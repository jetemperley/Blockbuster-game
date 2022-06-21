using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public GameObject gunRoot;
    public GameObject pistolRoot;
    public GameObject playerCamera;
    public bool canSwitch = true;
    // Start is called before the first frame update
    void Start()
    {
        // gunLocation = transform.GetChild(0).GetChild(0).gameObject;
        pistolRoot.SetActive(true);
        Destroy(gunRoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch") && gunRoot != null)
        {
            SwitchGun();
        }
    }

    public void SetGun(GameObject gun){
        
        
        //gun.transform.parent = gunRoot.transform.parent;
        gun.transform.parent = playerCamera.transform;
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        Destroy(gunRoot);
        
        gunRoot = gun;
        
        if(pistolRoot.activeSelf)
            SwitchGun();

    }

    public void SwitchGun() 
    {
        if (gunRoot != null && canSwitch)
        {
            if (pistolRoot.activeSelf)
            {
                pistolRoot.SetActive(false);
                gunRoot.SetActive(true);
            }
            else if (gunRoot.activeSelf)
            {
                pistolRoot.SetActive(true);
                gunRoot.SetActive(false);
            }
        }
    }
}
