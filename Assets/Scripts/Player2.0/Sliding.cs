using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sliding : MonoBehaviour{

    private PlayerInput controls;

    public Camera cam;

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
    private bool slideNeedsReset = false;
    private bool slideKeyNeedsRelease;

    private Vector3 inputDirection;

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

        if(!slideKey)
        {
            slideKeyNeedsRelease = false;
        }


        if(slideKey && (horizontalInput !=0 || verticalInput !=0) && pm.grounded==true && !sliding && !slideNeedsReset && !slideKeyNeedsRelease){
            StartSlide();
        }
        if(!slideKey && sliding){
            StopSlide();
        }
    }

    private void FixedUpdate(){
        if(sliding){
            SlidingMovement();
        }
    }

    private void StartSlide(){
        slideKeyNeedsRelease = true;
        sliding = true;
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        playerObj.localScale= new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        cam.transform.localScale = new Vector3(1,1/slideYScale,1);
        rb.AddForce(Vector3.down *5f, ForceMode.Impulse);

        slideTimer=maxSlideTime;
    }

    private void SlidingMovement(){
        

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
        slideNeedsReset = true;
        sliding=false;
        playerObj.localScale= new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        cam.transform.localScale = new Vector3(1,1,1);
        StartCoroutine(ResetSlide());
    }

    IEnumerator ResetSlide()
    {
        yield return new WaitForSeconds(0.25f);
        slideNeedsReset = false;
    }
}
