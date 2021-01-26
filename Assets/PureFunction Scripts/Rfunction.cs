using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rfunction
{
    public float Union(float f1, float f2)
    {
        return (f1 + f2 + Mathf.Sqrt(Mathf.Pow(f1, 2) + Mathf.Pow(f2, 2))) * (Mathf.Pow(Mathf.Pow(f1, 2) + Mathf.Pow(f2, 2), 0.5f));
    }

    public float Intersection(float f1, float f2)
    {
        return (f1 + f2 - Mathf.Sqrt(Mathf.Pow(f1, 2) + Mathf.Pow(f2, 2))) * (Mathf.Pow(Mathf.Pow(f1, 2) + Mathf.Pow(f2, 2), 0.5f));
    }
}
