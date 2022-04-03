using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHomeBehaviour : MonoBehaviour
{
    public Transform target;
    Rigidbody rb;
    public float moveSpeed = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        if (target == null)
            return;

        
        rb.MovePosition( rb.position +
            (target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }
}
