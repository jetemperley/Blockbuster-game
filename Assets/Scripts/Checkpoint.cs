using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){
            active = true;
        }
        CheckpointManager.GetInst().DeactivateOthers();
    }

    public bool IsActive(){
        return active;
    }

    public void Deactivate(){
        active = false;
    }
}
