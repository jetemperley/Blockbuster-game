using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float smooth;
    public float swayMultiplier;
    public float maxAmount;

    private Vector3 initialPos;

    // Update is called once per frame
    void Start(){
        initialPos = transform.localPosition;
    }

    void Update()
    {
        // RotateSway();
        MoveSway();
    }

    private void RotateSway(){
        float mouseX = Input.GetAxisRaw("Mouse X")*swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y")*swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);
    }

    private void MoveSway(){
        float movementX = -Input.GetAxis("Mouse X")*swayMultiplier;
        float movementY = -Input.GetAxis("Mouse Y")*swayMultiplier;
        // movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        // movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition+initialPos, Time.deltaTime*smooth);

    }
}
