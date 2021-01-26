using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{

    Vector3 mousePos;
    Vector3 mouseWorld;
    Vector3 origin;
    Vector2 mouseWorld2D;
    Vector2 closestPerimeter;
    bool overlap;
    PolygonCollider2D polyCollider;

    private SoundManager soundManager;

/*    [SerializeField]
    private GameObject LinePointPrefab;*/


    // Start is called before the first frame update
    void Start()
    {
        EllipseRenderer ellipse = GameObject.FindObjectOfType<EllipseRenderer>();
        ellipse.InitializeEllipse(new Ellipse(2, 3, 0, 0, 2));
        
/*        PlaneRenderer plane = GameObject.FindObjectOfType<PlaneRenderer>();

        plane.InitializePlaneLine(new Line(1, 3, 1));
        plane.InitializePlaneLine(new Line(-1, 4, 0));
        plane.InitializePlaneLine(new Line(-1, 7, 1));
        plane.InitializePlaneLine(new Line(1, 0, 0));

        plane.GeneratePlane();*/
        
/*        MeshCombiner meshCombiner = GameObject.FindObjectOfType<MeshCombiner>();
        meshCombiner.CombineMeshes();*/

/*        soundManager = GameObject.FindObjectOfType<SoundManager>();

        polyCollider = GetComponent<PolygonCollider2D>();*/
    }

    // Update is called once per frame
    void Update()
    {
        //updateMousePosition();
    }

/*    void updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 15f; //set as 15f since z position of camera is -15
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        //print("pixel: " + mousePos);
        print("world: " + mouseWorld);
        mouseWorld2D = new Vector2(mouseWorld.x, mouseWorld.y);
        overlap = polyCollider.OverlapPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        closestPerimeter = polyCollider.ClosestPoint(new Vector2(mouseWorld.x, mouseWorld.y));
        origin = polyCollider.bounds.center;
        //print("Origin: " + origin);

        soundManager.updateFrequency(overlap, mouseWorld, closestPerimeter, origin);
    }*/

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("COLLIDED");

*//*        foreach ( ContactPoint2D contact in collision.contacts)
        {
            print(contact.point);
        }*//*

        int contactCount = collision.contactCount;
        print("count: " + contactCount);
        ContactPoint2D[] contacts = new ContactPoint2D[contactCount];

        collision.GetContacts(contacts);

        foreach (ContactPoint2D contact in contacts)
        {
            print(contact.point);
            CreatePointMarker(contact.point);
        }
    }

    private void CreatePointMarker(Vector2 pointPosition)
    {
        // instantiate point prefab at point position with rotation 
        if (LinePointPrefab == null)
        {
            print("OH NO ITS NULL");
        }
        Instantiate(LinePointPrefab, pointPosition, Quaternion.identity);
    }*/
}
