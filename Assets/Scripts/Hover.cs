using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;

    public float rotPerSecond = 180;
    public float verticalMovement = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float off = Mathf.Sin(Time.time);
//         Debug.Log(off);
        rb.position = rb.position + (Vector3.up*off/(1/verticalMovement))*Time.deltaTime;
        rb.MoveRotation(rb.rotation*Quaternion.Euler(0, Time.deltaTime*rotPerSecond, 0));
    }
}
