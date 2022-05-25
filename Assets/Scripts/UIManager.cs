using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pistolCursor;
    public GameObject minigunCursor;
    public GameObject cannonCursor;
    public FillHealthBar healthBar;
    public FillHealthBar shieldBar;
    public FillDashBar dashBar;

    private GameManager gameManager;
    private GunHolder weapon;
    private Health playerHealth;
    private Health playerShield;
    private PlayerMove playerMove;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        weapon = FindObjectOfType<GunHolder>().GetComponent<GunHolder>();
        playerHealth =  FindObjectOfType<GunHolder>().GetComponent<Health>(); 
        playerShield = playerHealth.shield;   
        playerMove =  FindObjectOfType<GunHolder>().GetComponent<PlayerMove>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth != null){
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

            if(playerMove != null){
                if(playerMove.DashTimer < playerMove.dashCooldown)
                {
                    UpdateDashBar();
                }  
            }
        UpdateCursor();
        }else{
            pistolCursor.SetActive(false);
            minigunCursor.SetActive(false);
            cannonCursor.SetActive(false);
            healthBar.gameObject.SetActive(false);
            shieldBar.gameObject.SetActive(false);
            dashBar.gameObject.SetActive(false);
        }
    }

    private void UpdateCursor(){
        if (weapon.gunRoot != null && weapon.gunRoot.activeSelf)
        {
            string weaponName = weapon.gunRoot.name;
            if(weaponName.Contains("Pistol")){
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
        else if (weapon.pistolRoot.activeSelf)
        {
            pistolCursor.SetActive(true);
            minigunCursor.SetActive(false);
            cannonCursor.SetActive(false);
        }
    }

    private void UpdateHealthBar(){
        healthBar.UpdateBar(playerHealth.currentHealth, playerHealth.maxHealth);
    }

    private void UpdateShieldBar(){
        shieldBar.UpdateBar(playerShield.currentHealth, playerShield.maxHealth);
    }

    private void UpdateDashBar(){
        dashBar.UpdateBar(playerMove.DashTimer, playerMove.dashCooldown);
    }
}
