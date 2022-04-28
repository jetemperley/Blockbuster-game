using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOnDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this method for losing the game doesnt consider the players health
    // void OnCollisionEnter (Collision collisionInfo){
    //     if(collisionInfo.collider.tag == "Enemy"){

    //         Debug.Log("i've collided with an enemy!");
    //         FindObjectOfType<GameManager>().EndGame();  
    //     }
    // }

    // the health script destroys the object when its health reaches zero
    void OnDestroy() {
        FindObjectOfType<GameManager>().EndGame();  
    }

}
