using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRender : MonoBehaviour
{

    [SerializeField]
    private GameObject LineGeneratorPrefab;

    [SerializeField]
    private GameObject LinePointPrefab;
    PolygonCollider2D polyCollider;
    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    Vector2 closestPerimeter;
    bool overlap;
    private SoundManager soundManager;

    List<Lines> lines = new List<Lines>();

    void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        //polyCollider = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        //myPlaneFunction();
    }

    private void Update()
    {
        //updateMousePosition();
    }

    public void InitializePlaneLine(Lines line)
    {
        lines.Add(line);
    }

    public void GeneratePlane()
    {
        myPlaneFunction();
    }

    private void myPlaneFunction()
    {
        int numLines = lines.Count;
        Vector3[] vertices = new Vector3[numLines];

        //List<Line> lines = new List<Line>();

        /*        Line y1 = new Line(0);
                Line y2 = new Line(0, 2);
                Line y3 = new Line(0, 0);
                Line y4 = new Line(2);

                lines.Add(y1);
                lines.Add(y2);
                lines.Add(y4);
                lines.Add(y3);*/

        for (int i = 0; i< lines.Count; i++)
        {
            Vector3 intersection;
            if (i == lines.Count-1)
            {
                if (CheckParallelLines(lines[i], lines[0]))
                {
                    Debug.Log("Lines are parallel");
                }
                else
                {
                    intersection = FindIntersection(lines[i], lines[0]);
                    //print("INTERSECTION " + i + ": " + intersection);
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
                    //print("INTERSECTION " + i + ": " + intersection);
                    CreatePointMarker(intersection);
                    vertices[i] = intersection;
                }
            }
        }

/*        int[] xArray = new int[] { -1, -1, 1, 1 };
        int[] yArray = new int[] { 0, 5, 5, 0 };

        Vector3[] vertices = new Vector3[numLines];
        for (int i = 0; i < numLines; i++)
        {
            CreatePointMarker(new Vector3(xArray[i], yArray[i], 15f));
            vertices[i] = new Vector3(xArray[i], yArray[i], 15f);
        }*/

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
        if(LinePointPrefab == null)
        {
            print("OH NO ITS NULL");
        }
        Instantiate(LinePointPrefab, pointPosition, Quaternion.identity);
    }

    private void SpawnLineGenerator(Vector3[] linePoints)
    {

        for (int i = 0; i< linePoints.Length; i++)
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

        polyCollider = GetComponent<PolygonCollider2D>();

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < linePoints.Length; i++)
        {
            pathList.Add(new Vector2(linePoints[i].x, linePoints[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);
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

    private void ClearAllPoints()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("PointMarker");

        foreach (GameObject p in allPoints)
        {
            Destroy(p); 
        }
    }

    void updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 15f; //set as 15f since z position of camera is -15
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);
        overlap = polyCollider.OverlapPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        closestPerimeter = polyCollider.ClosestPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        origin = polyCollider.bounds.center;

        //soundManager.updateFrequency(overlap, mouseWorld, closestPerimeter, origin);
    }
}
