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
    public string url = "";

    public static bool resetToCheckpointOrNot = false;

    void Start()
    {
        CPManager = CheckpointManager.Inst;
        if (resetToCheckpointOrNot) 
        {
            CPManager.ResetToCheckpoint();
            resetToCheckpointOrNot = false;
        }
    }

    public void EndGame (){
        
        if(gameHasEnded == false){
            GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 0.15f;
            gameHasEnded = true;
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
        ScoreManager.ResetScores();
        CheckpointManager.ResetCheckpoints();
    }

    public void LoadCheckpoint(){
        Debug.Log("Attempted to reset to checkpoint");
        if(CPManager != null && CheckpointManager.activeCheckpoint != null){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            resetToCheckpointOrNot = true;
        }
        gameHasEnded = false;
    }

    public void CompleteLevel(){
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 0.15f;
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

    public void OpenUrl()
    {
        Application.OpenURL(url);
    }

}
