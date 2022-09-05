using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Basic Movement and Camera controls Taken from https://sharpcoderblog.com/blog/unity-3d-fps-controller 
[RequireComponent(typeof(CharacterController))]

public class PlayerMove : MonoBehaviour
{
    private PlayerInput controls;
    // private Input playerInputActions; 

    public float walkingSpeed = 7.5f;
    //public float runningSpeed = 11.5f;
    public float dashSpeed = 50.0f;
    public float dashTime = 0.2f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public AudioSource dashSFX;
    public AudioSource jumpSFX;
    public ParticleSystem speedParticles;

    CharacterController characterController;
    private Rigidbody playerRB;
    Vector3 moveDirection = Vector3.zero;
    Vector3 dashDirection = Vector3.zero;
    float rotationX = 0;

    public float dashCooldown = 1.0f;
    public bool canDash;
    private float dashTimer;
    private float movementDirectionY;
    private bool jumpPressed;
    private bool dashPressed;

    private int nbJumps;

    public float DashTimer
    {
        get {
            return dashTimer;
        }
    }

    [HideInInspector]
    public bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        dashSFX.Stop();
        jumpSFX.Stop();

        jumpPressed = false;
        dashPressed = false;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        /*dashTimer = 0;
        canDash = true;*/
    }

    // Update is called once per frame
    void Update()
    {

        if(PauseMenu.gameIsPaused)
        {
            characterController.enabled = false;
        }else{
            characterController.enabled = true;
            // Debug.Log(controls.actions["Move"].ReadValue<Vector2>());
            Vector2 moveInputVector = controls.actions["Move"].ReadValue<Vector2>();
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? (/*isRunning ? runningSpeed : */walkingSpeed) * moveInputVector.y : 0;
            float curSpeedY = canMove ? (/*isRunning ? runningSpeed : */walkingSpeed) * moveInputVector.x : 0;
            movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (controls.actions["Jump"].triggered && canMove && nbJumps < 2)
            {
                jumpSFX.Play(0);
                PlayerStats.getInst().addStat("jump");   
                moveDirection.y = jumpSpeed;
                nbJumps++;
            }
            else if (characterController.isGrounded){
                moveDirection.y = 0;
                nbJumps = 0;
            } else 
            {
                moveDirection.y = movementDirectionY;
            }
            //apply gravity
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

                //Dashing
            if(controls.actions["Dash"].triggered && canDash)
            {
                dashSFX.Play(0);
                dashTimer = dashCooldown;
                canDash = false;
                StartCoroutine(Dash());
            }

            if (dashTimer >= 0 && !canDash)
            {
                dashTimer -= Time.deltaTime;
            }

            if (dashTimer <= 0)
            {
                canDash = true;
            }
            characterController.Move(moveDirection*Time.deltaTime);

            Vector2 lookInputVector = controls.actions["Look"].ReadValue<Vector2>();
            rotationX += -lookInputVector.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookInputVector.x * lookSpeed, 0);
        }

        

        // // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // // as an acceleration (ms^-2)
       
    }
    private void JumpPressed(InputAction.CallbackContext ctx)
    {
        jumpPressed = true;
    }

    private void JumpReleased(InputAction.CallbackContext ctx)
    {
       jumpPressed = false;
    }

    private void DashPressed(InputAction.CallbackContext ctx)
    {
        dashPressed = true;
    }

    private void DashReleased(InputAction.CallbackContext ctx)
    {
        dashPressed = false;
    }

    IEnumerator Dash()
    {
        PlayerStats.getInst().addStat("dash");
        float startTime = Time.time;
        
        //Set the direction of the dash
        Vector2 moveInputVector = controls.actions["Move"].ReadValue<Vector2>();
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float dashSpeedX = canMove ? dashSpeed * moveInputVector.y : 0;
        float dashSpeedY = canMove ? dashSpeed * moveInputVector.x : 0;
        dashDirection = (forward * dashSpeedX) + (right * dashSpeedY);

        canMove = false;
        while(Time.time < startTime + dashTime)
        {            
            characterController.Move(dashDirection * Time.deltaTime);
            yield return null;
        }

        canMove = true;
     }


}
