using System;
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

            Vector3 accelerationThisFrame = gravityAcceleration * objektA.gravityScale;
            Vector3 vSquared = objektA.velocity.normalized * objektA.velocity.sqrMagnitude;
            Vector3 dragAcceleration = -objektA.drag * vSquared;

            accelerationThisFrame += dragAcceleration;

            objektA.velocity += accelerationThisFrame * dt;

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

                bool isOverlapping = false;

                if (objektA.shape.GetShape() == FizziksShape.Shape.Sphere &&
                   objektB.shape.GetShape() == FizziksShape.Shape.Sphere)
                {
                    isOverlapping = IsCollideSpheres(objektA, objektB);
                   
                }
                else if (objektA.shape.GetShape() == FizziksShape.Shape.Sphere &&
                        objektB.shape.GetShape() == FizziksShape.Shape.Plane)
                {
                    isOverlapping = IsOverlappingSpherePlane((FizziksShapeSphere)objektA.shape, (FizziksShapePlane)objektB.shape);
                }
                else if (objektA.shape.GetShape() == FizziksShape.Shape.Plane &&
                        objektB.shape.GetShape() == FizziksShape.Shape.Sphere)
                {
                    isOverlapping = IsOverlappingSpherePlane((FizziksShapeSphere)objektB.shape, (FizziksShapePlane)objektA.shape);
                }

                else if (objektA.shape.GetShape() == FizziksShape.Shape.Sphere &&
                        objektB.shape.GetShape() == FizziksShape.Shape.Halfspace)
                {
                    isOverlapping = IsOverlappingSphereHalfspace((FizziksShapeSphere)objektA.shape, (FizziksShapeHalfSpace)objektB.shape);
                }
                else if (objektA.shape.GetShape() == FizziksShape.Shape.Halfspace &&
                        objektB.shape.GetShape() == FizziksShape.Shape.Sphere)
                {
                    isOverlapping = IsOverlappingSphereHalfspace((FizziksShapeSphere)objektB.shape, (FizziksShapeHalfSpace)objektA.shape);
                }

                if (isOverlapping)
                {
                    Debug.DrawLine(objektA.transform.position, objektB.transform.position, Color.red);

                    objektA.GetComponent<Renderer>().material.color = Color.red;
                    objektB.GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
    public bool IsCollideSpheres(FizziksObjekt sphereA, FizziksObjekt sphereB)
    {
       
        Vector3 Displacement = sphereA.transform.position - sphereB.transform.position;
        float distance = Displacement.magnitude;

        float radiusA = ((FizziksShapeSphere)sphereA.shape).radius;
        float radiusB = ((FizziksShapeSphere)sphereB.shape).radius;

        float overlap = (radiusA + radiusB) - distance;

        if (overlap > 0.0f)
        {
            Vector3 collisionNormal_BtoA = (Displacement / distance);
            Vector3 mtv = collisionNormal_BtoA * overlap;

            sphereA.transform.position += mtv * 0.5f;
            sphereB.transform.position -= mtv * 0.5f;
            return true;
        }
        else
            return false;
    }

    public bool IsOverlappingSpheres(FizziksObjekt objektA, FizziksObjekt objektB)
    {
        Debug.Log("Checking collision between: " + objektA.name + " and " + objektB.name);
        Vector3 Displacement = objektA.transform.position - objektB.transform.position;
        float distance = Displacement.magnitude;

        float radiusA = ((FizziksShapeSphere)objektA.shape).radius;
        float radiusB = ((FizziksShapeSphere)objektA.shape).radius;


        return distance < radiusA + radiusB;
    }

    public bool IsOverlappingSpherePlane(FizziksShapeSphere sphere, FizziksShapePlane plane)
    {
        Vector3 planeToSphere = sphere.transform.position - plane.transform.position;
        float positionAlongNormal = Vector3.Dot(planeToSphere, plane.Normal());
        float distanceToPlane = Mathf.Abs(positionAlongNormal);
        float overlap = sphere.radius - positionAlongNormal;

        if(overlap > 0)
        {
           // Vector3 collisionNormal = planeToSphere / positionAlongNormal;
            Vector3 mtv = plane.Normal() * overlap;
            sphere.transform.position += mtv * 0.5f;
            plane.transform.position -= mtv * 0.5f;

            return true;
        }
        else

        return false;
    }

    public bool IsOverlappingSphereHalfspace(FizziksShapeSphere sphere, FizziksShapeHalfSpace halfspace)
    {
        Vector3 halfSpaceToSphere = sphere.transform.position - halfspace.transform.position;
        float positionAlongNormal = Vector3.Dot(halfSpaceToSphere, halfspace.Normal());
        float distanceToHalfSpace = Mathf.Abs(positionAlongNormal);

        float overlap = sphere.radius - distanceToHalfSpace;

        if(overlap > 0)
        {
           // Vector3 collisionNormal = planeToSphere/distanceToPlane;
            Vector3 mtv = halfspace.Normal() * overlap;
            sphere.transform.position += mtv * 0.5f;
            halfspace.transform.position -= mtv * 0.5f;

            return true;
        }
        else

        return false;

    }

}
