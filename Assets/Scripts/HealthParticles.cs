using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class HealthParticles : Health
{

    public ParticleSystem particlesOnDeath;
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
            particlesOnDeath.Play();
            
            Destroy(gameObject);
        }

    }
}
