using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool active;
    private UIManager uim;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        uim = FindObjectOfType<UIManager>().GetComponent<UIManager>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == 9)
        {
            IsActive();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            Deactivate();
        }
    }

    public bool IsActive(){
        uim.InCheckpoint();
        return active;
    }

    public void Deactivate(){
        uim.OutCheckpoint();
        active = false;
    }

}
