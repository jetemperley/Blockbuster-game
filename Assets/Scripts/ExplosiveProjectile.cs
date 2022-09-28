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

    public float timeToLive = 2;
    private float time = 0;
   
    
    private ParticleSystem explosion;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToLive){
            explode();
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        rb.AddForce(new Vector3(0,1f,0) * verticalOffset);
    }

    public void SetProperties (float projVel, float expRad, int dmg)
    {
        SetProperties(projVel, expRad, dmg, 6, 2);
    }

    public void SetProperties (float projVel, float expRad, int dmg, float vOffset, float timeTL)
    {
        projectileVelocity = projVel;
        explosiveRadius = expRad;
        damage = dmg;
        verticalOffset = vOffset;
        timeToLive = timeTL;
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider hc in hitColliders){

            Rigidbody rb = hc.GetComponent<Collider>().attachedRigidbody;
            if(rb != null){
                if (rb.gameObject.layer != 9)
                {
                    Health h = rb.gameObject.GetComponent<Health>();
                    if (h != null){
                        if (h.takeDamage(damage) <= 0){
                            PlayerStats.getInst().addStat(gameObject.name);
                        } 
                    } 
                }
            }
                        
        }        
        Destroy(this.gameObject);       
    }

    void OnCollisionEnter(Collision collision)
    {        
        if(collision.gameObject.name != "Player" && collision.gameObject != this.gameObject){
            explode();
        }
        
    }

    void explode(){
        explosion.Stop();
        explosion.transform.position = transform.position;
        float size = explosiveRadius;
        explosion.transform.localScale = new Vector3(size, size, size);
        explosion.Play();
        AudioSource audioData = explosion.gameObject.GetComponent<AudioSource>();
        audioData.Stop();
        audioData.Play(0);
        ExplosionDamage(transform.position, explosiveRadius);
    }

    public void setExplosion(ParticleSystem s){
        explosion = s;        
    }



}
