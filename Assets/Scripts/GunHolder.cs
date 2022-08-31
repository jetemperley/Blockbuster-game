using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunHolder : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions; 

    public GameObject gunRoot;
    public GameObject[] slots;
    public int activeSlot;
    //public GameObject pistolRoot;
    public GameObject playerCamera;
    public bool canSwitch = true;
    public UIManager UI;
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerInput>();
        playerInputActions = new Input();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Weapon1.performed += Weap1Switch;
        playerInputActions.Player.Weapon2.performed += Weap2Switch;
        playerInputActions.Player.Weapon3.performed += Weap3Switch;
        Destroy(gunRoot);

        activeSlot = 0;
        gunRoot = slots[activeSlot];
        for (int i = 0; i < slots.Length; i++)
        {
            if (i != activeSlot && slots[i] != null)
                slots[i].SetActive(false);
        }
        gunRoot.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Switch") && gunRoot != null)
        {
            SwitchGun();
        }*/
        // if (Input.GetButtonDown("Weap1"))
        // {
        //     SwitchGun(0);
        // }
        // if (Input.GetButtonDown("Weap2"))
        // {
        //     SwitchGun(1);
        // }
        // if (Input.GetButtonDown("Weap3"))
        // {
        //     SwitchGun(2);
        // }
    }

    public void SetGun(GameObject gun){
        gun.transform.parent = playerCamera.transform;
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        Destroy(gunRoot);
        
        gunRoot = gun;
    }

    public void Weap1Switch(InputAction.CallbackContext ctx)
    {
        SwitchGun(0);
    }

    public void Weap2Switch(InputAction.CallbackContext ctx)
    {
        SwitchGun(1);
    }

    public void Weap3Switch(InputAction.CallbackContext ctx)
    {
        SwitchGun(2);
    }


    public void SwitchGun(int slot) 
    {

        if (slots[slot] != null)
        {
            activeSlot = slot;
            gunRoot.SetActive(false);
            gunRoot = slots[activeSlot];
            gunRoot.SetActive(true);
        }
    }

    public void AddGun(GameObject newGun, Pickup pickup)
    {
        
        newGun.transform.parent = playerCamera.transform;
        newGun.transform.localPosition = Vector3.zero;
        newGun.transform.localRotation = Quaternion.identity;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = newGun;
                SwitchGun(i);
                Destroy(pickup.gameObject.transform.parent.gameObject);
                UI.UpdateWeaponSlots(i, newGun.GetComponent<WeaponModel>());
                return;
            }
        }
        //Give the old gun to the pickup object
        pickup.SetGunPickup(gunRoot);

        //Replace the old gun with the new gun
        slots[activeSlot] = newGun;
        gunRoot = slots[activeSlot];
        slots[activeSlot].SetActive(true);
        UI.UpdateWeaponSlots(activeSlot, newGun.GetComponent<WeaponModel>());
    }
}
