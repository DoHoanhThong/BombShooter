using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Triangle
{
    public Vector2 A;
    public Vector2 B;
    public Vector2 C;

    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        A = a;
        B = b;
        C = c;
    }

    public float Area
    {
        get
        {
            return 0.5f * Mathf.Abs((B.x - A.x) * (C.y - A.y) - (C.x - A.x) * (B.y - A.y));
        }
    }
}
[System.Serializable]
public class Bound
{
    public float topBound;
    public float bottomBound;
    public float leftBound;
    public float rightBound;
}