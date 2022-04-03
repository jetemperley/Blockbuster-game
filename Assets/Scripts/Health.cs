using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth = 1;

    public Health shield;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void takeDamage(int dam){
        if (shield != null) {
            shield.takeDamage(dam);
            return;
        }
        currentHealth -= dam;

        if (currentHealth <= 0)
            Destroy(gameObject);

    }

    int getHealth(){
        return currentHealth;
    }

    public void Reset(){
        currentHealth = maxHealth;
    }

}
