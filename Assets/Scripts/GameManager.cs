using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public CheckpointManager CPManager;

    //Main Menu objects
    public GameObject controlPanel;
    public GameObject mainMenuPanel;

    public static bool resetToCheckpointOrNot = false;

    public static bool gameHasEnded = false;

    void Start()
    {
        PlayerPrefs.GetFloat("MouseSens", 0.5f);       

        CPManager = CheckpointManager.Inst;
        
        if (resetToCheckpointOrNot) 
        {
            CPManager.ResetToCheckpoint();
            resetToCheckpointOrNot = false;
        }
    }

    public void EndGame (){
        if(gameHasEnded == false){
            Debug.Log("End Game");
            // GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().volume = 0.15f;
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
        // CheckpointManager.ResetCheckpoints();
        gameHasEnded = false;
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

    public void ActivateControlPanel()
    {
        controlPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ActivateMenuPanel()
    {
        controlPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void OpenWindow()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeImuncEjWfrFVEE-joCW1IxG83RbtGpwTC4PQKUXh8vaY7aA/viewform?usp=sf_link");
    }

}
