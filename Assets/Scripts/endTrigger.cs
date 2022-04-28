using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTrigger : MonoBehaviour {
    
    public GameManager gameManager;

    void OnTriggerEnter (){
        GameObject.FindObjectOfType<GameManager>().CompleteLevel();;
    }
}
