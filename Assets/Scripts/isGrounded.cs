using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGrounded : MonoBehaviour
{
    
    public PlayerMove cc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        cc.canJump = true;
    }
    private void OnTriggerStay(Collider other) {
        cc.canJump = true;
    }

    private void OnTriggerExit(Collider other) {
        cc.canJump = false;
    }
}
