using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour{
    private PlayerInput controls;

    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement2 pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCD;
    private float dashCDTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    private void Start(){
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement2>();

    }

    private void Update(){
        if(controls.actions["Dash"].triggered){
            Dash();
        }
        if(dashCDTimer>0){
            dashCDTimer -= Time.deltaTime;
        }
    }
    private void Dash(){

        if(dashCDTimer >0){
            return;
        }else{
            dashCDTimer=dashCD; 
        }
        pm.dashing = true;
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }
    private Vector3 delayedForceToApply;

    private void DelayedDashForce(){
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void ResetDash(){
        pm.dashing = false;
    }
}
