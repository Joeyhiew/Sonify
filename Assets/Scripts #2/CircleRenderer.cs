using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CircleRenderer : MonoBehaviour
{
    private SoundManager soundManager;
    CircleCollider2D circleCollider;
    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    Vector2 closestPerimeter;
    bool overlap;
    float radius = 5f;

    void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = radius;
        origin = circleCollider.bounds.center;
    }

    private void Update()
    {
        PolyMesh(radius, 500);
        updateMousePosition();       
    }

    void PolyMesh(float radius, int n)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //vertices
        List<Vector3> verticesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
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

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        Vector2[] path = pathList.ToArray();
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
        overlap = circleCollider.OverlapPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        closestPerimeter = circleCollider.ClosestPoint(new Vector2(mouseWorld.x, mouseWorld.y));

        //soundManager.updateFrequency(overlap, mouseWorld2D, closestPerimeter, radius, origin);
    }
}
