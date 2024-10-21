using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizziksObjekt : MonoBehaviour
{
    public FizziksShape shape = null;
    public float mass = 1;
    public float drag = 2.1f;
    public float gravityScale = 1;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<FizziksShape>();
        FizziksEnjun.Instance.objekts.Add(this); 
    }
}
