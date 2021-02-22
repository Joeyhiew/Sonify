using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRenderer : MonoBehaviour
{
    [SerializeField]
    private GameObject LineGeneratorPrefab;

    [SerializeField]
    private GameObject LinePointPrefab;

    [SerializeField]
    private AudioManager audioManager;

    public Rfunction rFunctions;

    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    float value;
    float maxValue;
    bool withinBoundary = true;

    List<Lines> lines = new List<Lines>();
    List<float> distances = new List<float>();

    public void InitializePlaneLine(Lines line)
    {
        lines.Add(line);
        distances.Add(0);
    }

    public void GeneratePlane()
    {
        myPlaneFunction();
    }
    void Update()
    {
        updateMousePosition();
    }

    private void myPlaneFunction()
    {
        int numLines = lines.Count;
        Vector3[] vertices = new Vector3[numLines];

        rFunctions = new Rfunction();


        for (int i = 0; i < lines.Count; i++)
        {
            Vector3 intersection;
            if (i == lines.Count - 1)
            {
                if (CheckParallelLines(lines[i], lines[0]))
                {
                    Debug.Log("Lines are parallel");
                }
                else
                {
                    intersection = FindIntersection(lines[i], lines[0]);
                    CreatePointMarker(intersection);
                    vertices[i] = intersection;
                }
            }
            else
            {
                if (CheckParallelLines(lines[i], lines[i + 1]))
                {
                    Debug.Log("Lines are parallel");
                }
                else
                {
                    intersection = FindIntersection(lines[i], lines[i + 1]);
                    CreatePointMarker(intersection);
                    vertices[i] = intersection;
                }
            }
        }

        GenerateNewLine();
    }

    private bool CheckParallelLines(Lines A, Lines B)
    {
        if ((A.type == 0 && B.type == 0) || (A.type == 1 && B.type == 1))
        {
            return true;
        }
        else if (A.type == 2 && B.type == 2)
        {
            // gradient value same
            if (A.m == B.m)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }

    }

    private Vector3 FindIntersection(Lines A, Lines B)
    {
        float intersectingX;
        float intersectingY;





        if (A.type == 1 && B.type == 0)
        {
            intersectingX = A.getPoint();
            intersectingY = B.getPoint();
        }
        else if (B.type == 1 && A.type == 0)
        {
            intersectingX = B.getPoint();
            intersectingY = A.getPoint();
        }
        else if (A.type == 2 && B.type == 1)
        {
            intersectingX = B.getPoint();
            intersectingY = A.getYValue(intersectingX);
        }
        else if (A.type == 1 && B.type == 2)
        {
            intersectingX = A.getPoint();
            intersectingY = B.getYValue(intersectingX);
        }
        else if (A.type == 2 && B.type == 0)
        {
            intersectingY = B.getPoint();
            intersectingX = A.getXValue(intersectingY);
        }
        else if (A.type == 0 && B.type == 2)
        {
            intersectingY = A.getPoint();
            intersectingX = A.getXValue(intersectingY);
        }
        else
        {
            intersectingX = (B.c = A.c) / (A.m - B.m);
            intersectingY = A.getYValue(intersectingX);
        }

        return new Vector3(intersectingX, intersectingY, 0f);
    }

    private void CreatePointMarker(Vector3 pointPosition)
    {
        // instantiate point prefab at point position with rotation 
        if (LinePointPrefab == null)
        {
            print("OH NO ITS NULL");
        }
        Instantiate(LinePointPrefab, pointPosition, Quaternion.identity);
    }

    private void SpawnLineGenerator(Vector3[] linePoints)
    {

        for (int i = 0; i < linePoints.Length; i++)
        {
            print("I is : " + linePoints[i]);
        }

        /*        GameObject newLineGen = Instantiate(LineGeneratorPrefab);
                LineRenderer lineRenderer = newLineGen.GetComponent<LineRenderer>();

                lineRenderer.positionCount = linePoints.Length;
                lineRenderer.SetPositions(linePoints);

                Mesh lineMesh = new Mesh();
                lineRenderer.BakeMesh(lineMesh);

                Vector3[] positions = new Vector3[linePoints.Length];
                lineRenderer.GetPositions(positions);
                for (int i = 0; i < positions.Length; i++){
                    print(positions[i]);
                }
                print(positions);

                lineRenderer.loop = true;*/

        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (linePoints.Length - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < linePoints.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = linePoints;
        mesh.triangles = triangles;
        mesh.normals = normals;

        for (int i = 0; i < linePoints.Length; i++)
        {
            print("MESH vertices: " + mesh.vertices[i]);
        }


        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < linePoints.Length; i++)
        {
            pathList.Add(new Vector2(linePoints[i].x, linePoints[i].y));
        }
        Vector2[] path = pathList.ToArray();
    }

    private void GenerateNewLine()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");
        Vector3[] allPointsPosition = new Vector3[allPoints.Length];

        if (allPoints.Length >= 2)
        {
            for (int i = 0; i < allPoints.Length; i++)
            {
                allPointsPosition[i] = allPoints[i].transform.position;
            }
            SpawnLineGenerator(allPointsPosition);
        }
        else
        {
            Debug.Log("Need 2 or more points to form line!");
        }
    }

    private void GetDistances(float x, float y)
    {
        int i = 0;
        foreach (Lines line in lines)
        {
            float distance = line.getDistanceFromLine(x, y);
            distances[i] = distance;
            //print("distance " + i + " : : " + distance);
            i++;
        }    
    }

    private float CheckValue()
    {
        float determiningValue;
        float smallestPositive = distances[0];
        float smallestNegative = distances[0];
        bool inside = true;
        foreach (float dist in distances)
        {
            print("dist: " + dist);
            if (dist > 0)
            {
                if (dist < smallestPositive)
                {
                    smallestPositive = dist;
                }
            }
            else
            {

                if ((Mathf.Abs(dist) < Mathf.Abs(smallestNegative)) || (smallestNegative >= 0))
                {
                    smallestNegative = dist;
                    inside = false;
                }
            }
        }
        if (inside == false)
        {
            withinBoundary = false;
            determiningValue = smallestNegative;
        }
        else
        {
            withinBoundary = true;
            determiningValue = smallestPositive;
        }
        return determiningValue;
    }

    private float GetDistanceValue(float x, float y)
    {
        int lineCount = lines.Count;
        float value = lines[0].GetValue(x, y);
        for (int i = 0; i < lineCount - 1; i++)
        {
            //value = rFunctions.Intersection(value, lines[i + 1].GetValue(x, y));
            value = Mathf.Min(value, lines[i + 1].GetValue(x, y));
        }
        return value;
    }

    public float updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 10f; //set as 10f since z position of camera is -10
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        //print("world: " + mouseWorld);
        mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);
        origin = transform.position;
        //print("Origin: " + origin);
        //GetDistances(mouseWorld.x, mouseWorld.y);

        value = GetDistanceValue(mouseWorld.x, mouseWorld.y);
        //print("Value: " + value);
        //float freqRatio = 1 - (value / 10);

        return value;
    }
}
