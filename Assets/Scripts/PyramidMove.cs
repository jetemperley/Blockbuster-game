using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PyramidMove : MonoBehaviour
{
    Conductor conductor;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        conductor = Conductor.getConductor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the terrain towards the player at speed determined by the Conductor
        if(rigidbody.position.z > conductor.startBoundary)
            rigidbody.MovePosition(rigidbody.position + Vector3.back * Time.deltaTime * conductor.getLevelSpeed());

        if (rigidbody.position.z <= conductor.getBoundary())
        {
            Destroy(this.gameObject);
        }
    }
}