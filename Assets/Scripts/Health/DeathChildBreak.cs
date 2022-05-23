using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DeathChildBreak : DeathEffect 
{
    public float randomSpeedOnBreak = 1;
    public Transform childContainer;

    public override void effect(){
        while (childContainer.childCount != 0){
            GameObject g = childContainer.GetChild(0).gameObject;
            Rigidbody r = g.AddComponent<Rigidbody>();
            // g.AddComponent<Health>();
            // r.AddExplosionForce(100, g.transform.position, 2);
            r.useGravity = false;
            r.mass = 0.01f;
            g.transform.SetParent(null);
            r.AddForce(Random.onUnitSphere*randomSpeedOnBreak, ForceMode.VelocityChange);
            g.AddComponent<Terrain>();
            Destroy(g, 2);
            
                
        }
    }
}
