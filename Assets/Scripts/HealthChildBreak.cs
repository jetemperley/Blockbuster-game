using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class HealthChildBreak : Health
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started special break");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public override void takeDamage( int dam){
        if (shield != null) {
            shield.takeDamage(dam);
            return;
        }
        currentHealth -= dam;

        if (currentHealth <= 0) {
            for (int i = 0; i < transform.childCount; i++){
                GameObject g = transform.GetChild(i).gameObject;
                Rigidbody r = g.AddComponent<Rigidbody>();
                g.AddComponent<Health>();
                // r.AddExplosionForce(100, g.transform.position, 2);
                r.useGravity = false;
                g.transform.SetParent(null);
                
                Destroy(g, 2);
                
                    
            }
            
            Destroy(gameObject);
        }

    }
}
