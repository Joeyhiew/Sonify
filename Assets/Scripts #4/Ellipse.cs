using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse : MonoBehaviour
{
    public float a;
    public float b;
    public float h;
    public float k;
    public float r;

    // if its a circle, a and b are 1
    public Ellipse(float a, float b, float h, float k, float r)
    {
        this.a = a;
        this.b = b;
        this.h = h;
        this.k = k;
        this.r = r;
    }

    public float getValue(float x, float y)
    {
        //return (Mathf.Pow((x - h), 2) / (a * a)) + (Mathf.Pow((y - k), 2) / (b * b)) - (r * r);
        return (r * r) - (Mathf.Pow((x - h), 2) / (a * a)) - (Mathf.Pow((y - k), 2) / (b * b));
    }

    public float getMaxPositiveValue()
    {
        return (r * r);
    }
}
