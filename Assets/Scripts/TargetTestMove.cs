using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTestMove : MonoBehaviour
{
    public float moveSpeed = 2;
    private Rigidbody rb;
    private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vec = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        vec.Set(0, 0, moveSpeed*Time.fixedDeltaTime);
        rb.MovePosition(transform.position - vec);
    }
}
