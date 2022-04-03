using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnDestroy : MonoBehaviour
{

    public GameObject[] toEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        foreach (GameObject g in toEnable){
            g.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
