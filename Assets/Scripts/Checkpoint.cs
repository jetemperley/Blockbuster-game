using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private bool active;
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        active = false;
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9){
            active = true;
            CheckpointManager.activeCheckpoint = this;
            CheckpointManager.savedScore = ScoreManager.SetScore();
        }
        CheckpointManager.GetInst().DeactivateOthers();
    }

    public bool IsActive(){
        return active;
    }

    public void Deactivate(){
        active = false;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = startPosition;
    }
}
