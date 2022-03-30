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
        dir = direction + new Vector3(-0.05f,0.5f,0);
        //dir = Vector3.Normalize(dir);
    }
}
