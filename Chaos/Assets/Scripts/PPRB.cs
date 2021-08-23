using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPRB : MonoBehaviour
{
    // Allows PPPhysics to enumerate
    void OnEnable() =>  PPPhysics.pprbs.Add(this); 
    void OnDisable() => PPPhysics.pprbs.Remove(this);

    public Vector2 velocity = Vector2.zero;
}
