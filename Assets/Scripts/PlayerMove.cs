using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Movement and Camera controls Taken from https://sharpcoderblog.com/blog/unity-3d-fps-controller 
[RequireComponent(typeof(CharacterController))]

public class PlayerMove : MonoBehaviour
{

    public float walkingSpeed = 7.5f;
    //public float runningSpeed = 11.5f;
    public float dashSpeed = 50.0f;
    public float dashTime = 0.2f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector3 dashDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        //bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (/*isRunning ? runningSpeed : */walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (/*isRunning ? runningSpeed : */walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Dashing
        if(Input.GetKeyDown("left shift"))
        {
            StartCoroutine(Dash());
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        
        //Set the direction of the dash
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float dashSpeedX = canMove ? dashSpeed * Input.GetAxis("Vertical") : 0;
        float dashSpeedY = canMove ? dashSpeed * Input.GetAxis("Horizontal") : 0;
        dashDirection = (forward * dashSpeedX) + (right * dashSpeedY);

        while(Time.time < startTime + dashTime)
        {
            canMove = false;
            characterController.Move(dashDirection * Time.deltaTime);

            yield return null;
        }

        canMove = true;
    }

    void OnCollisionEnter (Collision collisionInfo){
        if(collisionInfo.collider.tag == "CompleteLevel"){
            FindObjectOfType<GameManager>().CompleteLevel();
            
        }

    }
}
