using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float launchAngle = 20f;
    public float launchSpeed = 20f;
    public float startHeight = 1.0f;

    public GameObject projectileToCopy;

    void Update()
    {
        Vector3 launchVelocity = new Vector3(Mathf.Cos(launchAngle * Mathf.PI / 180) * launchSpeed, Mathf.Sin(launchAngle * Mathf.PI / 180) * launchSpeed);
        Vector3 startPosition = new Vector3(0.0f, startHeight, 0.0f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Launch");
            GameObject newObject = Instantiate(projectileToCopy);
            FizziksObjekt fizziksObjekt = newObject.GetComponent<FizziksObjekt>();

            fizziksObjekt.velocity = launchVelocity;

            fizziksObjekt.transform.position = startPosition;
        }

        Debug.DrawLine(startPosition, startPosition + launchVelocity, Color.red);

    }
}
