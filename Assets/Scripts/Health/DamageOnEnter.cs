using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnEnter : MonoBehaviour
{
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
//         Debug.Log(gameObject.name + " and " + other.gameObject.name + " collided");
        if (other.gameObject.layer == gameObject.layer) {
            
            return;
        }
            
        Health h = other.gameObject.GetComponent<Health>();
        if (h != null)
            h.takeDamage(damage);
    }

    private void OnTriggerEnter(Collider other) {
        // Debug.Log(gameObject.name + " and " + other.gameObject.name + " collided");
        if (other.gameObject.layer == gameObject.layer) {
            
            return;
        }
            
        Health h = other.gameObject.GetComponent<Health>();
        if (h != null)
            h.takeDamage(damage);
    }
    
}
