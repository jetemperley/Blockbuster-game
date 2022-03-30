
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    

    public void EndGame (){
        
        if(gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            //display gameover UI
            //click Restart button on UI to restart game
            Restart();
        }

    }

    void Restart (){
        //reload scene. gives me an error idk how to fix
        //SceneManager.LoadScene(SceneManagement.GetActiveScene().name);
    }

    public void CompleteLevel(){
        Debug.Log("Level Complete!");
        //display UI
        //click next on UI
        //Restart();
    }
}
