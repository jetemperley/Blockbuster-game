using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    
    public GameObject completeLevelUI;
    public GameObject gameOverUI;

    public void EndGame (){
        
        if(gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("GAME OVER");
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

}
