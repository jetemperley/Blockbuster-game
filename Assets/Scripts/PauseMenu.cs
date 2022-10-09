using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions;

    public GameObject pausePanel;
    public GameObject pauseMenuUI;
    public GameObject controlMenuUI;
    public GameObject soundMenuUI;
    public GameObject settingsMenuUI;

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
        if(!GameManager.gameHasEnded)
        {
            if(gameIsPaused && (!settingsMenuUI.activeSelf || !controlMenuUI.activeSelf || !soundMenuUI.activeSelf))
            {
                Resume();
            } 
            else
            {
                OpenPauseMenu();
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        OpenPauseMenu();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume()
    {
        gameIsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OpenSettings()
    {
        settingsMenuUI.SetActive(true);
        soundMenuUI.SetActive(false);
        controlMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void OpenSound()
    {
        settingsMenuUI.SetActive(false);
        soundMenuUI.SetActive(true);
        controlMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void OpenControls()
    {
        settingsMenuUI.SetActive(false);
        soundMenuUI.SetActive(false);
        controlMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void OpenPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        soundMenuUI.SetActive(false);
        controlMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
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
