using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefBoundaries : MonoBehaviour
{
    private float translateSpeed = 30f;
    public float freqRatio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = transform.position;
            if (position.y < 30) {
                transform.Translate(Vector3.up * Time.deltaTime * translateSpeed);
                
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = transform.position;
            if (position.y > -30)
            {
                transform.Translate(Vector3.down * Time.deltaTime * translateSpeed);
            }
        }

        freqRatio = Mathf.InverseLerp(-30, 30, transform.position.y);

    }
}
