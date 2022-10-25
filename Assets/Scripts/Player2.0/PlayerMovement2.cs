using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    private PlayerInput controls;
    private UIManager uim;

    public GameObject speedParticles;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    float rotationX = 0;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public int totalJumps;
    private int numOfJumps;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    
    private Vector3 startPos;
    private float distance;
    public float Distance
    {
        get
        {
            return distance;
        }
    }

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Sound")]
    public AudioClip jumpSFX;

    private MovementState state;
    private enum MovementState
    {
        walking,
        sprinting,
        crouching,
        dashing,
        air
    }
    
    public bool dashing;

    private void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        uim = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        rb.freezeRotation = true;
        startPos = transform.position;

        if(speedParticles != null)
        {
            speedParticles.GetComponent<ParticleSystem>().Stop();
        }
        readyToJump = true;

        startYScale = transform.localScale.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);

        if(!PauseMenu.gameIsPaused)
        {
            MyInput();
            SpeedControl();
            StateHandler();
            // handle drag
            if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching)
                rb.drag = groundDrag;
            else
                rb.drag = 0; 
        }

        //distance
        distance = transform.position.z-startPos.z;
        uim.UpdateDistance((int)distance);
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    

    private void MyInput()
    {
        Vector2 moveInputVector = controls.actions["Move"].ReadValue<Vector2>();
        horizontalInput = moveInputVector.x;
        verticalInput = moveInputVector.y;
        if(rb.velocity.magnitude >= 15 && speedParticles!= null)
        {
            speedParticles.GetComponent<ParticleSystem>().Play();
        }else if(speedParticles != null)
        {
            speedParticles.GetComponent<ParticleSystem>().Stop();
        }
        // Look
        Look();

        // when to jump
        if(controls.actions["Jump"].triggered && readyToJump==true && numOfJumps<totalJumps)
        {
            
            Jump();
            numOfJumps++;
            if(numOfJumps==totalJumps){
                readyToJump = false;
            }
            

        }else if(grounded){
            readyToJump = true;
            numOfJumps=0;
            exitingSlope = false;
        }

        // start crouch
        if (controls.actions["Crouch"].triggered)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (controls.actions["Crouch"].triggered)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void Look()
    {
        Vector2 lookInputVector = controls.actions["Look"].ReadValue<Vector2>();
        rotationX += -lookInputVector.y * (lookSpeed* 2 * PlayerPrefs.GetFloat("MouseSens", 0.5f));
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, lookInputVector.x * (lookSpeed* 2 * PlayerPrefs.GetFloat("MouseSens", 0.5f)), 0);
    }

    private void StateHandler()
    {
        // mode - Dashing
        if(dashing){
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
        }
        // Mode - Crouching
        else if (controls.actions["Crouch"].triggered)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - sprinting
        else if (grounded)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        // on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(this.gameObject.GetComponent<Sliding>().IsSliding)
        {
            rb.AddForce(transform.up * jumpForce * 2f, ForceMode.Impulse);
        }else{
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        AudioSource audio = AudioPool.GetAudioSource();
        audio.clip = jumpSFX;
        audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
        audio.Play(0);
       
    }
    // private void ResetJump()
    // {

    // }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}

