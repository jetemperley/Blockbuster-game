using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : DeathEffect
{
    public int damage = 10;
    public int explosionRadius = 10;
    public AudioClip clip;

    public override void effect(){

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        if(clip!= null){
            AudioSource.PlayClipAtPoint(clip, transform.position, 1f);
        }

        

        foreach(Collider nearbyObject in colliders){
                        
            Health h = nearbyObject.gameObject.GetComponent<Health>();
            if (h != null){
                h.takeDamage(damage);
            }
            
            }
    }

        private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
    }
}
