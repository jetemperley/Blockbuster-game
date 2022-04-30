using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaserLength : MonoBehaviour
{
    public Transform laser;
    public float laserLength = 1;
    // Start is called before the first frame update
    void Start()
    {
        laser.localScale = new Vector3(laserLength, laser.localScale.y, laser.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // private void OnGUI() {
    //     laser.localScale = new Vector3(laser.localScale.x*laserLength, laser.localScale.y, laser.localScale.z);
    // }
    private void OnValidate() {
        laser.localScale = new Vector3(laserLength, laser.localScale.y, laser.localScale.z);
    }


}
