using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerCantact : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == 9){
            Destroy(gameObject);
        }        
    }
}
