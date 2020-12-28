using UnityEngine;

public class Boundaries : MonoBehaviour
{

    /*    private Vector2 screenBounds;
        private float objectWidth;
        private float objectHeight;
        private float speed = 1.0f;*/

    // mousePos is in pixel coordinates
    // spherePos is in world coordinates
    Vector3 mousePos;
    Vector3 sphereWorldPos;
    Vector3 spherePixelPos;
    public float freqRatio;

    // Start is called before the first frame update
    void Start()
    {
/*        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<MeshRenderer>().bounds.extents.x / 2;
        objectHeight = transform.GetComponent<MeshRenderer>().bounds.extents.y / 2;*/
    }

    void Update()
    {
        //print("transform.position: " + transform.position);
        mousePos = Input.mousePosition;
        //print("mousePos: " + mousePos);
        mousePos.z = 100f;
        mousePos.x = Screen.width / 2;
        mousePos.y = Mathf.Clamp(mousePos.y, Screen.height / 5, Screen.height - (Screen.height / 5));
        //mousePos.x = Mathf.Clamp(mousePos.x, Screen.width/2 + objectWidth, Screen.width / 2 * -1 - objectWidth);
        //mousePos.y = Mathf.Clamp(mousePos.y, Screen.height/2 + objectHeight, Screen.height / 2 * -1 - objectHeight);
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        sphereWorldPos = transform.position;
        spherePixelPos = Camera.main.WorldToScreenPoint(transform.position);
        freqRatio = Mathf.InverseLerp(Screen.height / 5, Screen.height - (Screen.height / 5), spherePixelPos.y);
    }

    // Update is called once per frame

    /*    void LateUpdate()
        {
            var viewPos = Input.mousePosition;
            print(viewPos);
            var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(viewPos.x, viewPos.y, transform.position.z));
            //moveSphere(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            //Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
            transform.position = wantedPos;
        }*/
}
