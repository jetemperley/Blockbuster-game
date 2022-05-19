using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float smooth;
    public float swayMultiplier;

    // Update is called once per frame
    void Update()
    {
        MoveSway();
    }

    private void MoveSway(){
        float mouseX = Input.GetAxisRaw("Mouse X")*swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y")*swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth*Time.deltaTime);
    }
}
