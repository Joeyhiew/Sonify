using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Explorer : MonoBehaviour
{
    [SerializeField]
    private GameObject LinePointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 screenPosDepth = Input.mousePosition;
                screenPosDepth.z = 10f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPosDepth);
                CreatePointMarker(mousePos);
                print("CREATED!");
            }

        }
    }

    private void CreatePointMarker(Vector3 pointPosition)
    {
        // instantiate point prefab at point position with rotation 
        Instantiate(LinePointPrefab, pointPosition, Quaternion.identity);
    }
}
