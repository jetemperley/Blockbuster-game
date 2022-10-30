using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject pistolCursor;
    public GameObject minigunCursor;
    public GameObject cannonCursor;
    public Image [] iconWeaponSlots;
    public HealthBarImage healthBar;
    public HealthBarImage shieldBar;
    public FillDashBar dashBar;
    public GameObject loadingAlert;

    public Text scoreText;
    public Text scoreAddText;
    public Text scoreMultiplierText;
    public Text scoreDisplayText;
    public Text highscoreDisplayText;
    public Text timerText;
    public Text endTimerText;
    public Text bestTimeText;
    public Text distanceText;
    public Text endDistText;
    public Text bestDistText;

    private GameManager gameManager;
    private GunHolder weapon;
    private Health playerHealth;
    private Health playerShield;
    private GameObject StatSliders;
    private GameObject WeaponSlots;
    private Dashing playerMove;
    private ScoreManager scoreManager;

    private float timer;
    private bool inCheckpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        weapon = FindObjectOfType<GunHolder>().GetComponent<GunHolder>();
        playerHealth =  FindObjectOfType<GunHolder>().GetComponent<Health>(); 
        playerShield = playerHealth.shield;   
        playerMove =  FindObjectOfType<GunHolder>().GetComponent<Dashing>();  
        scoreManager = ScoreManager.Inst;
        StatSliders = GameObject.Find("3DUI");
        //WeaponSlots = GameObject.Find("WeaponSlots");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != null && !PauseMenu.gameIsPaused){
            StatSliders.SetActive(true);
            //WeaponSlots.SetActive(true);
            
            if(playerHealth.currentHealth > 0)
            {
                UpdateHealthBar();
            }   
           
            if(playerShield != null){
                if(playerShield.currentHealth >= 0)
                {
                    UpdateShieldBar();
                }  
            }

            if(!inCheckpoint)
            {
                timer += Time.deltaTime;  
            }
            
            if(timer > PlayerPrefs.GetFloat("bestTime"))
            {
                PlayerPrefs.SetFloat("bestTime", timer);
                PlayerPrefs.Save();
            }
            
            timerText.text = DisplayTime(timer);

            

            // if(playerMove != null){
            //     if(playerMove.DashCDTimer < playerMove.dashCD)
            //     {
            //         UpdateDashBar();
            //     }  
            // }

            scoreText.text = "" + ScoreManager.currentScore;
            scoreAddText.text = "+ " + ScoreManager.scoreToAdd;
            scoreMultiplierText.text = "x " + scoreManager.scoreMultiplier;
            UpdateCursor();

        }else{
            //StatSliders.SetActive(false);
            pistolCursor.SetActive(false);
            minigunCursor.SetActive(false);
            cannonCursor.SetActive(false);
        }

        scoreDisplayText.text = "Final Score: " + ScoreManager.currentScore;
        highscoreDisplayText.text = "Personal Best: " + ScoreManager.highscore;
        endTimerText.text = "Time: " + DisplayTime(timer);
        bestTimeText.text = "Longest Time: " + DisplayTime(PlayerPrefs.GetFloat("bestTime", timer));
    }

    private void UpdateCursor(){
        if (weapon.gunRoot != null && weapon.gunRoot.activeSelf)
        {
            string weaponName = weapon.gunRoot.name;
            if(weaponName.Contains("Pistol") || weaponName.Contains("Revolver") || weaponName.Contains("Rifle") ){
                pistolCursor.SetActive(true);
                minigunCursor.SetActive(false);
                cannonCursor.SetActive(false);
            }else if(weaponName.Contains("Minigun")){
                pistolCursor.SetActive(false);
                minigunCursor.SetActive(true);
                cannonCursor.SetActive(false);
            }else{
                pistolCursor.SetActive(false);
                minigunCursor.SetActive(false);
                cannonCursor.SetActive(true);
            }
        } 
    }

    private void UpdateHealthBar()
    {
        healthBar.UpdateBar(playerHealth.currentHealth, playerHealth.maxHealth);
    }

    private void UpdateShieldBar()
    {
        shieldBar.UpdateBar(playerShield.currentHealth, playerShield.maxHealth);
    }

    private void UpdateDashBar()
    {
        dashBar.UpdateBar(playerMove.DashCDTimer, playerMove.dashCD);
    }

    public void UpdateWeaponSlots(int slot, WeaponModel wm)
    {
        iconWeaponSlots[slot].sprite = wm.icon;
    }

    public void UpdateActiveSlot(int slot)
    {
        for(int i = 0; i<iconWeaponSlots.Length; i++)
        {
            if(i == slot)
            {
                iconWeaponSlots[i].color = new Color32(0,255,255,255);
                iconWeaponSlots[i].transform.Find("Slot").GetComponent<Text>().color = new Color32(0,255,255,255);
            }else{
                iconWeaponSlots[i].color = new Color32(255,0,230,255);
                iconWeaponSlots[i].transform.Find("Slot").GetComponent<Text>().color = new Color32(255,0,230,255);
            }            
        }        
    }

    public void UpdateDistance(float distance)
    {
        distanceText.text = "" + distance + "M";
        endDistText.text = "Distance: " + distance + "M";
        if(distance > PlayerPrefs.GetFloat("bestDist"))
        {
            PlayerPrefs.SetFloat("bestDist", distance);
            PlayerPrefs.Save();
        }

        bestDistText.text = "Longest Distance: " + PlayerPrefs.GetFloat("bestDist") + "M";
    }

    public string DisplayTime(float timer_)
    {
        int hours = (int)(timer_/60)/60;
        int minutes = (int)(timer_/60) % 60;
        int seconds = (int)(timer_) % 60;
        int milliseconds = (int)(timer_*10) % 100;

        string hoursString = "" + hours;
        string minutesString = "" + minutes;
        string secondsString = "" + seconds;
        string millisecondsString = "" + milliseconds;

        if(hours < 10){
            hoursString = "0" + hours;
        }

        if(minutes < 10)
        {
            minutesString = "0" + minutes;
        }

        if(seconds < 10)
        {
            secondsString = "0" + seconds;
        }

        if(milliseconds < 10)
        {
            millisecondsString = "0" + milliseconds;
        }

        return hoursString + ":" + minutesString + ":" + secondsString + "." + millisecondsString;
    }

    public void InCheckpoint()
    {
        inCheckpoint = true;
    }

    public void OutCheckpoint()
    {
        inCheckpoint = false;
    }

}
