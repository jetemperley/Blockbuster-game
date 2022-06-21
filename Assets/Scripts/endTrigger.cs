using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTrigger : MonoBehaviour {
    
    public GameManager gameManager;

    void OnTriggerEnter (Collider c){
        if (c.gameObject.CompareTag("Player")){
            GameObject.FindObjectOfType<GameManager>().CompleteLevel();
            c.attachedRigidbody.gameObject.GetComponent<Health>().takeDamage(100);
        }
        
    }
}
