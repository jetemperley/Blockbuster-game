using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class LookAtPlayer : MonoBehaviour
{
    private FieldOfView fov;
    private PlayerMovement2 player;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldOfView>();
        player = FindObjectOfType<PlayerMovement2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fov.visibleTargets.Count>0)
        {
            Vector3 direction = player.transform.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
        
    }
}
