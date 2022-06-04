using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIflashes : MonoBehaviour
{
    public GameObject gotHit;
    public GameObject pickUpShield;
    public float StartOpacity = 0.7f;
    public float fadeAwaySpeed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        if(gotHit != null){
            if(gotHit.GetComponent<Image>().color.a > 0){
                var color = gotHit.GetComponent<Image>().color;
                color.a -= fadeAwaySpeed;
                gotHit.GetComponent<Image>().color=color;
            }
        }

        if(pickUpShield != null){
            if(pickUpShield.GetComponent<Image>().color.a > 0){
                var color = gotHit.GetComponent<Image>().color;
                color.a -= fadeAwaySpeed;
                pickUpShield.GetComponent<Image>().color=color;
            }
        }
    }


    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.layer==11){
                var color = gotHit.GetComponent<Image>().color;
                color.a = StartOpacity;
                gotHit.GetComponent<Image>().color=color;
        }
        if(collision.gameObject.tag=="ShieldPickup"){
                var color = pickUpShield.GetComponent<Image>().color;
                color.a = StartOpacity;
                pickUpShield.GetComponent<Image>().color=color;
        }
        }
    
}
