using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// original
public class EllipseRender : MonoBehaviour
{
    private SoundManager soundManager;
    

    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    Vector2 closestPerimeter;
    bool overlap;
    PolygonCollider2D polyCollider;

    Ellipse ellipse;

    void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        //polyCollider = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        
    }

    private void Update()
    {
        // Equation of ellipse and circle
        // (x/a)^2 + (y/b)^2 = r^2
        //MyFunction(0, 0);
        //PolyMesh(radius, 500, 2, 3);
        //updateMousePosition();
    }

    public void InitializeEllipse(Ellipse ellipse) 
    {
        this.ellipse = ellipse;
        PolyMesh(ellipse, 500);
    }

/*    float MyFunction(float x, float y)
    {
        int a = 2;
        int b = 3;
        int h = 0;
        int k = 0;
        int r = 2;
        this.radius = r;
        this.a = a;
        this.b = b;
        return (Mathf.Pow((x - h), 2) / (a * a)) + (Mathf.Pow((y - k), 2) / (b * b)) - (r * r);
    }*/

    void PolyMesh(Ellipse ellipse, int n)
    {
        float radius = ellipse.r;
        float a = ellipse.a;
        float b = ellipse.b;

        // Say equation is x^2 + Y^2 - r^2 = 0
        MeshFilter mf = GetComponent<MeshFilter>();
        polyCollider = GetComponent<PolygonCollider2D>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //vertices
        List<Vector3> verticesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = a * radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = b * radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticesList.Add(new Vector3(x, y, 0f));
        }
        Vector3[] vertices = verticesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (n - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < vertices.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);
    }

    void updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 15f; //set as 15f since z position of camera is -15
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
                print("pixel: " + mousePos);
                print("world: " + mouseWorld);
        mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);
        overlap = polyCollider.OverlapPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        closestPerimeter = polyCollider.ClosestPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        origin = polyCollider.bounds.center;

        //soundManager.updateFrequency(overlap, mouseWorld, closestPerimeter, origin);
    }
}
