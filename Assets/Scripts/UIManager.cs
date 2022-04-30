using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pistolCursor;
    public GameObject minigunCursor;
    public GameObject cannonCursor;

    private GameManager gameManager;
    private GunHolder weapon;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        weapon = FindObjectOfType<GunHolder>().GetComponent<GunHolder>();      
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(weapon.gunRoot.name);
        UpdateCursor();
    }

    private void UpdateCursor(){
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
}
