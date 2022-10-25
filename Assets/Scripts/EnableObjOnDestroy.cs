using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnDestroy : MonoBehaviour
{

    public GameObject[] toEnable;

    void OnDestroy(){
        foreach (GameObject g in toEnable){
            g.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
