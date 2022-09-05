using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sliding : MonoBehaviour{

    private PlayerInput controls;

    [Header("Referebces")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement2 pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Keycodes")]
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;
    private bool slideKey = false;

    private void Start(){
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement2>();

        startYScale = playerObj.localScale.y;
    }

    private void Update(){
        Vector2 moveInputVector = controls.actions["Move"].ReadValue<Vector2>();
        horizontalInput = moveInputVector.x;
        verticalInput = moveInputVector.y;

        if(controls.actions["slide"].triggered)
        {
            slideKey = !slideKey;
        }

        if(slideKey && (horizontalInput !=0 || verticalInput !=0)){
            StartSlide();
        }
        if(slideKey && sliding){
            StopSlide();
        }
    }

    private void FixedUpdate(){
        if(sliding){
            SlidingMovement();
        }
    }

    private void StartSlide(){
        Debug.Log("slide");
        sliding = true;

        playerObj.localScale= new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down *5f, ForceMode.Impulse);

        slideTimer=maxSlideTime;
    }

    private void SlidingMovement(){
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(!pm.OnSlope() || rb.velocity.y > -0.1f){
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }else{
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if(slideTimer <= 0){
            StopSlide();
        }
    }

    private void StopSlide(){
        sliding=false;
        playerObj.localScale= new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
