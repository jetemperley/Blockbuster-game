using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAway : MonoBehaviour
{
    static Vector3 fallSpeed = new Vector3(0, -1, 0);

    public float backWall = -10;

    private bool fall = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (fall || transform.position.z < backWall){
            
            if (!fall){
                if (rb == null){
                    rb = gameObject.AddComponent<Rigidbody>();
                }
                rb.useGravity = false;
                rb.isKinematic = false;
                rb.mass = 1000;
                rb.drag = 1;
                // rb.angularDrag = 1;
                fall = true;
            }
            rb.MovePosition(rb.position + fallSpeed*Time.deltaTime);
            
        }
    }
}
