using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(pos + Vector3.up*Mathf.Sin(Time.time)/2);
        rb.MoveRotation(rb.rotation*Quaternion.Euler(0, 180*Time.deltaTime, 0));
    }
}
