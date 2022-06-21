using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public Health healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ShieldPickup") {
            /*if (healthComponent.shield == null)
            {
                GameObject instance = new GameObject("Shield");
                Health shieldComponent = instance.AddComponent<Health>();
                shieldComponent.maxHealth = 2;
                healthComponent.shield = shieldComponent;
            }*/
            
            if (healthComponent.shield != null)
            {
                healthComponent.shield.currentHealth = healthComponent.shield.maxHealth;
                Destroy(other.gameObject);
            }
        }
            
        if (other.gameObject.tag == "HealthPickup") {
            healthComponent.currentHealth = healthComponent.maxHealth;
            Destroy(other.gameObject);
        }
    }
}
