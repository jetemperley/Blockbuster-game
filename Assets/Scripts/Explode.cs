using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public int damage = 10;
    public int explosionRadius = 10;
    
    public void explode(){

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider nearbyObject in colliders){
                        
            Health h = nearbyObject.gameObject.GetComponent<Health>();
            if (h != null){
                Debug.Log("i've dealt Damage");
                h.takeDamage(damage);
            }
            
            }
    }

        private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
    }
}
