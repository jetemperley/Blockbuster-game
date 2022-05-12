using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint [] checkpoints;

    private static CheckpointManager inst;

    private void Awake() {
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
