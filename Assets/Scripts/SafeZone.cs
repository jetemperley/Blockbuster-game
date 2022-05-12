using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Conductor.getConductor().getPosition() >= transform.position.z){
            Conductor.getConductor().gameSpeed = 0;
            // Debug.Log("automaticly paused");
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("trigger enter");
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.gameObject.layer == 9){
            Conductor.getConductor().gameSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("trigger exit");
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.gameObject.layer == 9){
            Conductor.getConductor().gameSpeed = 1;
            
            Destroy(this);
        }
    
    }
}
