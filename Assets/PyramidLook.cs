using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class PyramidLook : MonoBehaviour
{
    private FieldOfView fov;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
       if(fov.visibleTargets.Count > 0)
       {
            Vector3 direction = FindObjectOfType<PlayerMovement2>().transform.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
       } 
    }
}
