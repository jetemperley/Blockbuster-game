using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGrounded : MonoBehaviour
{
    
    Collider jump;
    public PlayerMove characterController;
    // Start is called before the first frame update
    void Start()
    {
        jump = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
//         Debug.Log("Trigger enter " + other.gameObject.name);
        characterController.canJump = true;
    }
    private void OnTriggerStay(Collider other) {
//        Debug.Log("Trigger stay " + other.gameObject.name);
        characterController.canJump = true;
    }

    // private void OnTriggerExit(Collider other) {
    //     Debug.Log("Trigger exit " + other.gameObject.name);
    //     cc.canJump = false;
    // }
}
