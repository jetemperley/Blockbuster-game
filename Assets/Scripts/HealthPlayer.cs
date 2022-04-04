using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Health
{

    public GameObject playerRootObject;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

        if (currentHealth <= 0)
            Destroy(playerRootObject);

    }


}
