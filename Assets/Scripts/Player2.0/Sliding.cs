using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour{

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
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement2>();

        startYScale = playerObj.localScale.y;
    }

    private void Update(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(slideKey) && (horizontalInput !=0 || verticalInput !=0)){
            StartSlide();
        }
        if(Input.GetKeyUp(slideKey) && sliding){
            StopSlide();
        }
    }

    private void FixedUpdate(){
        if(sliding){
            SlidingMovement();
        }
    }

    private void StartSlide(){
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
