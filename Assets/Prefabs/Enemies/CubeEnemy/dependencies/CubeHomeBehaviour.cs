using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class CubeHomeBehaviour : MonoBehaviour
{
    FieldOfView fov;
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 3;
    public float maxLookDist = 10;
    public string targetTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        if (target == null || fov.visibleTargets.Count < 1)
            return;

        Vector3 direction = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }
}
