using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public CheckpointManager CPManager;

    public static bool resetToCheckpointOrNot = false;

    void Start()
    {
        if (resetToCheckpointOrNot) 
        {
            CPManager = CheckpointManager.Inst;
            CPManager.ResetToCheckpoint();
            resetToCheckpointOrNot = false;
        }
    }

    public void EndGame (){
        
        if(gameHasEnded == false){
            gameHasEnded = true;
            // Debug.Log("GAME OVER");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            gameOverUI.SetActive(true);            
        }

    }
    public void Quit(){
        Application.Quit();
    }
    
    public void Restart (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCheckpoint(){
        if(CPManager != null){
            /*gameOverUI.SetActive(false);
            GameObject player = Instantiate(CPManager.playerPrefab);
            foreach(Checkpoint cp in CPManager.checkpoints){
                if(cp.IsActive())
                {
                    Debug.Log(cp.transform.position);
                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.position = cp.transform.position;
                    player.GetComponent<CharacterController>().enabled = true;

                    player.transform.localRotation = Quaternion.Euler(0,180,0);

                }        
            }*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            resetToCheckpointOrNot = true;
        }
        gameHasEnded = false;
    }

    public void CompleteLevel(){
        if (gameHasEnded)
            return;
        Debug.Log("Level Complete!");
        gameHasEnded = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        completeLevelUI.SetActive(true);

        PlayerStats.getInst().log();
        PlayerStats.getInst().reset();
    }

    public void Load1(){
        SceneManager.LoadScene(1);
    }

}
