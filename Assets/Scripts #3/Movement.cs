using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 mouseWorld;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);
        mousePos.z = 15f; //set as 15f since z position of camera is -15
        mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
