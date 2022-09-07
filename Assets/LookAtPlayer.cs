using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class LookAtPlayer : MonoBehaviour
{
    public TankAttack ta;
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
        if(fov.visibleTargets.Count>0 && !ta.IsAttacking)
        {
            Vector3 direction = player.transform.position-transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.parent.transform.rotation = rotation;
            transform.rotation = rotation;
        }
        
    }
}
