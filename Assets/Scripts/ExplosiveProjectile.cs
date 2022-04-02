using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    public float projectileVelocity;
    public float explosiveRadius;
    public Vector3 offset;

    private Rigidbody rb;
    private Vector3 pos;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider hc in hitColliders){
            if(hc.gameObject.name != "Plane"){
               Destroy(hc.gameObject); 
            }
            
        }
        
        Destroy(this.gameObject);
       
    }

    void OnCollisionEnter(Collision collision)
    {        
        if(collision.gameObject.name != "Player" && collision.gameObject != this.gameObject){
            Debug.Log(collision.gameObject);
            ExplosionDamage(transform.position, explosiveRadius);
        }
        
    }

}
