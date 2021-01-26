using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseRenderer : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;

    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    float value;
    float maxValue;
    Ellipse ellipse;


    void Update()
    {
        updateMousePosition();
    }

    public void InitializeEllipse(Ellipse ellipse)
    {
        this.ellipse = ellipse;
        print("this eliipse: " + ellipse.h);
        PolyMesh(ellipse, 500);
    }


    void PolyMesh(Ellipse ellipse, int n)
    {
        float radius = ellipse.r;
        float a = ellipse.a;
        float b = ellipse.b;
        float h = ellipse.h;
        float k = ellipse.k;

        // Say equation is x^2 + Y^2 - r^2 = 0
        MeshFilter mf = GetComponent<MeshFilter>();
 
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //vertices
        List<Vector3> verticesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = a * radius * Mathf.Sin((2 * Mathf.PI * i) / n) + h;
            y = b * radius * Mathf.Cos((2 * Mathf.PI * i) / n) + k;
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

    public float updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 10f; //set as 10f since z position of camera is -10
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);
        origin = transform.position;
        value = ellipse.getValue(mouseWorld.x, mouseWorld.y);
        maxValue = ellipse.getMaxPositiveValue();
        //print("value: " + value);

        //float freqRatio = 1 - (value / maxValue);

        //audioManager.updateFrequency(value, maxValue);
        return value;
    }

}
