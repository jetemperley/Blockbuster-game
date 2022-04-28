using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float startup;
    public float activeTime;
    public float recovery;
    public float damage;

    public Transform hitbox;

    public bool attacking;
    public bool canAttack;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = startup;
        attacking = false;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
