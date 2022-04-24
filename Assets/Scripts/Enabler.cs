using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    
    public GameObject enableThis;

    void Start()
    {
        enableThis.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        
        // for (int i = 0; i < transform.childCount; i++){
        //     GameObject child = transform.GetChild(i).gameObject;
        //     child.SetActive(true);
        // }
        if (other.attachedRigidbody.gameObject.CompareTag("Player"))
        enableThis.SetActive(true);

    }



}
