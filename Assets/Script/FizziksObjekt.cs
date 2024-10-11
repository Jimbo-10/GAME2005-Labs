using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizziksObjekt : MonoBehaviour
{
    public float mass = 1;
    public float drag = 2.1f;
    public float radius = 1.0f;
    public Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        FizziksEnjun.Instance.objekts.Add(this); 
    }

    private void Update()
    {
        transform.localScale = new Vector3(radius, radius, radius) * 2f;
    }
}
