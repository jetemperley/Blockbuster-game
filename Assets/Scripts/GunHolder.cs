using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    public GameObject gunRoot;
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
        
        
        gun.transform.parent = gunRoot.transform.parent;
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        Destroy(gunRoot);
        
        gunRoot = gun;
        

    }
}
