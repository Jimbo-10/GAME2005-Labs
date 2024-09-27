using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float launchAngle = 20f;
    public float launchSpeed = 20f;
    public float startHeight = 1.0f;

    public float deltaTime = 0.02f;

    public Vector3 velocity;
    public Vector3 gravityAcceleration;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Launch");
            velocity = new Vector3(Mathf.Cos(launchAngle * Mathf.PI / 180) * launchSpeed, Mathf.Sin(launchAngle * Mathf.PI / 180) * launchSpeed);
            transform.position = new Vector3(0.0f, startHeight, 0.0f);
            gravityAcceleration = new Vector3(0.0f, -10.0f, 0.0f);
            Debug.DrawLine(transform.position, velocity, Color.red, 2);
        }

    }

    private void FixedUpdate()
    {

        transform.position = transform.position + velocity * deltaTime;

        velocity = velocity + gravityAcceleration * deltaTime;

        Debug.DrawLine(transform.position, transform.position + velocity, Color.green);

    }

}
