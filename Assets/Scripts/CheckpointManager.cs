using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint [] checkpoints;

    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //player = FindObjectOfType<PlayerMove>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <checkpoints.Length; i++){
            Debug.Log(checkpoints[i].IsActive());
            if(checkpoints[i].IsActive()){
                for(int k = 0; k<i; k++){
                    checkpoints[k].Deactivate();
                }
            }
        }
    }
}
