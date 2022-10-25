using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunHolder : MonoBehaviour
{
    private PlayerInput controls; 

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
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        Destroy(gunRoot);
        UI = FindObjectOfType<UIManager>();
        activeSlot = 0;
        gunRoot = slots[activeSlot];
        UI.UpdateActiveSlot(activeSlot);
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i] != null)
            {
                UI.UpdateWeaponSlots(i, slots[i].GetComponent<WeaponModel>());
   
                if (i != activeSlot)
                {
                    slots[i].SetActive(false);
                }                    
            }           
                
        }
        gunRoot.SetActive(true);        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controls.actions["Weapon 1"].triggered)
        {
            SwitchGun(0);
        }
        if (controls.actions["Weapon 2"].triggered)
        {
            SwitchGun(1);
        }
        if (controls.actions["Weapon 3"].triggered)
        {
            SwitchGun(2);
        }
        if(controls.actions["NextWeap"].ReadValue<float>() > 0)
        {
            SwitchGun(activeSlot - 1);
        }
         if(controls.actions["NextWeap"].ReadValue<float>() < 0)
        {
            SwitchGun(activeSlot + 1);
        }
    }

    public void SetGun(GameObject gun){
        gun.transform.parent = playerCamera.transform;
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        Destroy(gunRoot);
        
        gunRoot = gun;
    }


    public void SwitchGun(int slot) 
    {
        if(slot < 0)
        {
            slot = 2;
        }
        if(slot > 2)
        {
            slot = 0;
        }

        if (slots[slot] != null)
        {
            activeSlot = slot;
            
            gunRoot.SetActive(false);
            gunRoot = slots[activeSlot];
            gunRoot.SetActive(true);
            UI.UpdateActiveSlot(slot);
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
                UI.UpdateActiveSlot(i);
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
