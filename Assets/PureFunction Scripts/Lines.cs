using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
    // horizontal is mY + c
    // vertical is mX + c
    // slant is aX + bY + c
    public float m;
    public float c;
    public float a;
    public float b;
    // 0 for horizontal, 1 for vertical , 2 for slant
    public int type;

    public Lines(float m, float c, int type)
    {
        this.m = m;
        this.c = c;
        this.type = type;
    }

    // for slant
    public Lines(float a, float b, float c, int type)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.type = 2;
    }

    public float getYValue(float k)
    {
        return m * k + c;
    }

    public float getXValue(float k)
    {
        return (k - c) / m;
    }

    public float getPoint()
    {
        if (type == 1 || type == 0)
        {
            if (m >= 0)
            {
                return -c;
            }
            else if (m < 0)
            {
                return c;
            }
            Debug.Log("Does not fulfil function structure");
        }
        return 0;
    }

    public float getDistanceFromLine(float x1, float y1)
    {
        //float distance;
        if (type == 0)
        {
            if (m >= 0)
            {
                return y1 - getPoint();
            }
            else
            {
                return getPoint() - y1;
            }
        }
        else if (type == 1)
        {
            if (m >= 0)
            {
                return x1 - getPoint();
            }
            else
            {
                return getPoint() - x1;
            }
        }
        else
        {
            float x2, y2, c2, c1, m1, m2;
            m2 = m;
            c2 = c;
            m1 = 1 / m2;
            c1 = y1 - (m1 * x1);
            

            x2 = ((m1 * x1) + c1 - c2) / m2;
            y2 = m2 * x2 + c2;

            return Mathf.Sqrt((Mathf.Pow((x2 - x1), 2)) - (Mathf.Pow(y2 - y1, 2)));
        }
    }

    public float GetValue(float x, float y)
    {
        float value;
        if (type == 0)
        {
            value = m * y + c;
        }
        else if (type == 1)
        {
            value = m * x + c;
        }
        else
        {
            value = a * x + b * y + c;
        }
        return value;
    }
}
