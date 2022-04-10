using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public GameObject gunLocation;
    // Start is called before the first frame update
    void Start()
    {
        // gunLocation = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGun(GameObject gun){
        gun.transform.position = gunLocation.transform.position;
        gun.transform.rotation = gunLocation.transform.rotation;
        
        gun.transform.SetParent(gunLocation.transform.parent);
        Destroy(gunLocation);
        gunLocation = gun;
        

    }
}
