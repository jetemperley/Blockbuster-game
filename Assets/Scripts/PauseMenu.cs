using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions;

    public GameObject pauseMenuUI;

    public static bool gameIsPaused = false;
    // Update is called once per frame
    void Awake()
    {
        controls = GetComponent<PlayerInput>();
        playerInputActions = new Input();        
    }

    void OnEnable()
    {
        playerInputActions.UI.Enable();
        playerInputActions.UI.Pause.performed += PauseGame;
    }

    private void OnDisable()
    {
      playerInputActions.UI.Pause.performed -= PauseGame;  
      playerInputActions.UI.Disable();
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        Debug.Log(GameManager.gameHasEnded);
        if(!GameManager.gameHasEnded)
        {
            if(gameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Restart (){
        ScoreManager.ResetScores();
        GameManager.gameHasEnded = false; 
        Debug.Log("Restarting " + GameManager.gameHasEnded);
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
    }

    
}
