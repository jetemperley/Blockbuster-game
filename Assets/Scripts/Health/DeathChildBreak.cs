using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathChildBreak : DeathEffect 
{

    public override void effect(){
        for (int i = 0; i < transform.childCount; i++){
            GameObject g = transform.GetChild(i).gameObject;
            Rigidbody r = g.AddComponent<Rigidbody>();
            g.AddComponent<Health>();
            // r.AddExplosionForce(100, g.transform.position, 2);
            r.useGravity = false;
            g.transform.SetParent(null);
            
            Destroy(g, 2);
            
                
        }
    }
}
