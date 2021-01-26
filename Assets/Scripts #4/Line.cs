using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    // mK + c
    public float m;
    public float c;
    // 0 for horizontal, 1 for vertical , 2 for slant
    public int type;

    public Line(float m,  float c, int type)
    {
        this.m = m;
        this.c = c;
        this.type = type;
    }

/*    public Line(float c, int type)
    {
        this.c = c;
        this.type = type;
    }*/

    public float getValue(float k)
    {
        return m * k + c;
    }
}

/*public class VerticalLine : MonoBehaviour
{
    // aX + b
    public float b;
    public float a;

    public VerticalLine(float a, float b)
    {
        this.a = a;
        this.b = b;
    }

    public float getValue(float x)
    {
        return a * x + b;
    }
}

public class HorizontalLine : MonoBehaviour
{
    // aY + b
    public float b;
    public float a;

    public HorizontalLine(float a, float b)
    {
        this.a = a;
        this.b = b;
    }

    public float getValue(float y)
    {
        return a * y + b;
    }
}*/