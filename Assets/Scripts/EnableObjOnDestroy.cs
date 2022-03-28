using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjOnDestroy : MonoBehaviour
{

    public GameObject toEnable;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy(){
        toEnable.SetActive(true);
        Instantiate(obj);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
