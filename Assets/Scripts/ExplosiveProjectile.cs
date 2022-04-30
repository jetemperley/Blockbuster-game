using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    public float projectileVelocity;
    public float explosiveRadius;
    public int damage;
    public float verticalOffset;    

    private Rigidbody rb;
    private Vector3 pos;
   
    
    private ParticleSystem explosion;
    

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
        rb.AddForce(new Vector3(0,1f,0) * verticalOffset);
    }

    void ExplosionDamage(Collision collision, Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider hc in hitColliders){

            Rigidbody rb = hc.GetComponent<Collider>().attachedRigidbody;
            if(rb != null){
                Health h = rb.gameObject.GetComponent<Health>();
                if (h != null){
                    h.takeDamage(damage); 
                } 
            }
                        
        }        
        Destroy(this.gameObject);       
    }

    void OnCollisionEnter(Collision collision)
    {        
        if(collision.gameObject.name != "Player" && collision.gameObject != this.gameObject){
            explosion.Stop();
            explosion.transform.position = transform.position;
            float size = explosiveRadius;
            explosion.transform.localScale = new Vector3(size, size, size);
            explosion.Play();
            AudioSource audioData = explosion.gameObject.GetComponent<AudioSource>();
            audioData.Stop();
            audioData.Play(0);
            ExplosionDamage(collision, transform.position, explosiveRadius);
        }
        
    }

    public void setExplosion(ParticleSystem s){
        explosion = s;        
    }



}
