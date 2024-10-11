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
        
        foreach(FizziksObjekt objekt in objekts)
        {
            objekt.GetComponent<Renderer>().material.color = Color.white;
        }

        for(int iA = 0; iA < objekts.Count; iA++)
        {
            FizziksObjekt objektA = objekts[iA];

            for(int iB = iA + 1; iB < objekts.Count; iB++)
            {
                FizziksObjekt objektB = objekts[iB];

                if (objektA == objektB) continue;

                if (IsOverlappingSpheres(objektA, objektB))
                {
                    Debug.DrawLine(objektA.transform.position, objektB.transform.position, Color.red);

                    objektA.GetComponent<Renderer>().material.color = Color.red;
                    objektB.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    // no collision
                }
            }
        }
    }
    public bool IsOverlappingSpheres(FizziksObjekt objektA, FizziksObjekt objektB)
    {
        Debug.Log("Checking collision between: " + objektA.name + " and " + objektB.name);
        Vector3 Displacement = objektA.transform.position - objektB.transform.position;
        float distance = Displacement.magnitude;

        return distance < objektA.radius + objektB.radius;
    }

}
