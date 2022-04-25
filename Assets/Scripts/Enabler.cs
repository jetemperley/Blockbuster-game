using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Enabler : MonoBehaviour
{
    
    public GameObject enableThis;
    float toOffset = 0;
    int layers = 2;
    void Start()
    {
        //disable(layers, enableThis.transform);
        enableThis.SetActive(false);
    }

    void FixedUpdate() {
        // toOffset = Time.fixedDeltaTime * Conductor.conductor.getLevelSpeed();
    }

    void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody == null)
            return; 
        if (other.attachedRigidbody.gameObject.CompareTag("Player")) {
            // StartCoroutine(load(layers, enableThis.transform));
            enableThis.SetActive(true);
        }
            

    }
    

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
