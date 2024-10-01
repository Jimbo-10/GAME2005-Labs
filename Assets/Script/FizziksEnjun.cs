using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizziksEnjun : MonoBehaviour
{
    static FizziksEnjun instance = null;
    public static FizziksEnjun Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<FizziksEnjun>();
            }
            return instance;
        }
    }

    public List<FizziksObjekt> objekts = new List<FizziksObjekt>();
    public float dt = 0.02f;
    public Vector3 gravityAcceleration = new Vector3(0, -10, 0);
    Vector3 dragF;
    void FixedUpdate()
    {
        foreach (FizziksObjekt objektA in objekts)
        {
            Vector3 prevPos = objektA.transform.position;
            Vector3 newPos = objektA.transform.position + objektA.velocity * dt;
           

            objektA.transform.position = newPos;

            objektA.velocity += gravityAcceleration * dt;

            // drag
            dragF = -objektA.drag * objektA.velocity;
            objektA.velocity += dragF * dt;

            // Debug draw
            Debug.DrawLine(prevPos, newPos, Color.green, 10);
            Debug.DrawLine(objektA.transform.position, objektA.transform.position + objektA.velocity, Color.red);
        }
        
    }
}
