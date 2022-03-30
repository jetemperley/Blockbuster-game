using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    public override void takeDamage(int dam){
        if (shield != null) {
            shield.takeDamage(dam);
            return;
        }
        currentHealth -= dam;

        if (currentHealth <= 0) {
            Debug.Log("doing special break");
            for (int i = 0; i < transform.childCount; i++){
                Rigidbody r = transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
                r.useGravity = false;
            }
            transform.DetachChildren();
            Destroy(gameObject);
        }

    }
}
