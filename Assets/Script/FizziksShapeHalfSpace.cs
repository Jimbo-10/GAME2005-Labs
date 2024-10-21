using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FizziksShape;

public class FizziksShapeHalfSpace : FizziksShape
{
    public override Shape GetShape()
    {
        return Shape.Halfspace;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 Normal()
    {
        return transform.up;
    }
}
