using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathChildBreak : DeathEffect 
{
    private float speedOnBreak = 10;
    public Transform childContainer;

    public override void effect(){
        while (childContainer.childCount != 0){
            GameObject g = childContainer.GetChild(0).gameObject;
            Rigidbody r = g.AddComponent<Rigidbody>();
            // g.AddComponent<Health>();
            // r.AddExplosionForce(100, g.transform.position, 2);
            r.useGravity = true;
            g.layer = 12;
            r.mass = 0.01f;
            g.transform.SetParent(null);
            Vector3 f = Random.onUnitSphere*speedOnBreak;
            r.AddForce(f, ForceMode.VelocityChange);
            g.AddComponent<Terrain>();
            Destroy(g, 15);
            
                
        }
    }
}
