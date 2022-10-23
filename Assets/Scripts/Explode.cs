using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : DeathEffect
{
    public int damageToEnemies = 10;
    public int damageToPlayer = 1;
    public int explosionRadius = 10;
    public AudioClip clip;

    public override void effect(){

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        if(clip!= null){
            AudioSource.PlayClipAtPoint(clip, transform.position, 
                                        PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f));
        }

        
        foreach(Collider nearbyObject in colliders){

                Rigidbody rb = nearbyObject.GetComponent<Collider>().attachedRigidbody;
                if (rb != null){
                    Health h = rb.gameObject.GetComponent<Health>();
                    if(h!=null){
                        if(rb.gameObject.layer == 11){
                            h.takeDamage(damageToEnemies);
                        }else{
                            h.takeDamage(damageToPlayer);
                        }
                    }
                }
            }            
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
    }
}
