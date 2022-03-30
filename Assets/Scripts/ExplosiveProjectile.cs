using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    public float projectileForce;
    public float projectileLifeSpan;

    private Rigidbody rb;
    private Vector3 pos;
    private Vector3 dir;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        rb.AddForce(dir *projectileForce);
    }

    public void SetDir(Vector3 direction){
        dir = direction + offset;
        //dir = Vector3.Normalize(dir);
    }
}
