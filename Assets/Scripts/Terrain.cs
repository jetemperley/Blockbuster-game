using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Terrain : MonoBehaviour
{
    Rigidbody rigidbody;
    Conductor conductor;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        conductor = Conductor.conductor;
    }

     void Update() {
        //Move the terrain towards the player at speed determined by the Conductor
        
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (conductor != null){
            rigidbody.MovePosition(
                rigidbody.position + Vector3.back * Time.fixedDeltaTime * conductor.getLevelSpeed());
        }
    }
}
