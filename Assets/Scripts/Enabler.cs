using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enabler : MonoBehaviour
{
    
    public GameObject enableThis;
    float toOffset = 0;
    int layers = 2;
    public GameObject player;
    bool playerPassed = false;
    void Start()
    {
        //disable(layers, enableThis.transform);
        enableThis.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate() {
        // toOffset = Time.fixedDeltaTime * Conductor.conductor.getLevelSpeed();
        if (player != null)
        {
            if (player.transform.position.z > gameObject.transform.position.z && playerPassed == false)
            {
                playerPassed = true;
                enableThis.SetActive(true);
            }
        }
    }

    /*void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody == null)
            return; 
        if (other.attachedRigidbody.gameObject.CompareTag("Player")) {
            // StartCoroutine(load(layers, enableThis.transform));
            enableThis.SetActive(true);
        }
            

    }*/
    

    IEnumerator load(int depth, Transform t){

        for (int i = 0; i < t.transform.childCount; i++){
            if (depth > 1)
                yield return load(depth -1, t.transform.GetChild(i));
            GameObject child = t.transform.GetChild(i).gameObject;
            child.SetActive(true);
            yield return null;
        }

    }

    void disable(int depth, Transform t){
        for (int i = 0; i < t.transform.childCount; i++){
            if (depth > 1)
                load(depth -1, t.transform.GetChild(i));
            GameObject child = t.transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }



}
