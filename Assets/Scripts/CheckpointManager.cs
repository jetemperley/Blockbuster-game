using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint [] checkpoints;

    private static CheckpointManager inst;
    public static CheckpointManager Inst 
    {
        get 
        {
            return inst;
        }
    }

    public static Checkpoint activeCheckpoint = null;
    public GameObject level;
    public GameObject player;
    public static int savedScore = 0;
    public bool isActive = false;

    private void Awake() {
        DontDestroyOnLoad(this);

        if (inst == null)
            inst = this;
        else 
            Destroy(gameObject);
    }

    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerMove>().gameObject;
        player = GameObject.FindWithTag("Player");
        level = GameObject.FindWithTag("Level");
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCheckpoint != null)
            isActive = true;
    }

    public void DeactivateAll(){     
        for(int i = 0; i <checkpoints.Length; i++){            
            checkpoints[i].Deactivate();              
        }
    }

    public void DeactivateOthers(){
        for(int i = 0; i <checkpoints.Length; i++){
            if(checkpoints[i].IsActive())
            {
                for(int k = 0; k<i; k++)
                {
                    checkpoints[k].Deactivate();
                }
            }            
                          
        }
    }

    public static CheckpointManager GetInst(){
        return inst;
    }

    public void ResetToCheckpoint()
    {
        player = GameObject.FindWithTag("Player");
        level = GameObject.FindWithTag("Level");

        if (activeCheckpoint != null && level != null && player != null)
        {
            Vector3 levelPosition;
            Vector3 playerPosition;

            levelPosition = level.transform.position;
            levelPosition.z = -activeCheckpoint.startPosition.z;
            level.transform.position = levelPosition;

            playerPosition = player.transform.position;
            playerPosition.y = activeCheckpoint.startPosition.y;
            player.transform.position = playerPosition;

            ScoreManager.currentScore = savedScore;
            ScoreManager.scoreToAdd = 0;

            MoveCheckpoints(activeCheckpoint.startPosition.z);
        }
    }

    public void MoveCheckpoints(float zMovement)
    {
        Vector3 newPosition;

        for(int i = 0; i <checkpoints.Length; i++){     
            newPosition = checkpoints[i].gameObject.transform.position;
            newPosition.z -= zMovement;
            checkpoints[i].gameObject.transform.position = newPosition;
        }
    }

    public static void ResetCheckpoints()
    {
        savedScore = 0;
        activeCheckpoint = null;
    }
}
