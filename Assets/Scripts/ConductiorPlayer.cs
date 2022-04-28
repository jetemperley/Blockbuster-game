using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductiorPlayer : MonoBehaviour
{

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Conductor.conductor != null){
            characterController.Move(Vector3.back*Conductor.conductor.getLevelSpeed()*Time.deltaTime);
        }
    }

    void FixedUpdate() {
        
    }
}
