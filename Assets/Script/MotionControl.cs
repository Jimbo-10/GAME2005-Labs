using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControl : MonoBehaviour
{
    public float X,Y = 0;
    public float a = 1;
    public float b = 3;
    public float t = 0;
    
    
    void FixedUpdate()
    {
        float dt = 1.0f / 60.0f;

        X = (-Mathf.Sin(t * a) * a * b * dt);
        X = (-Mathf.Cos(t * a) * a * b * dt);

        transform.position = new Vector3(X, Y, transform.position.z);

        t += dt;
    }
}
