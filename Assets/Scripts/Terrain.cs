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
        rigidbody.isKinematic = true;
        conductor = Conductor.conductor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move the terrain towards the player at speed determined by the Conductor
        rigidbody.MovePosition(rigidbody.position + Vector3.back * Time.deltaTime * conductor.getLevelSpeed());

        if (rigidbody.position.z <= conductor.getBoundary())
        {
            Destroy(this.gameObject);
        }
    }
}
