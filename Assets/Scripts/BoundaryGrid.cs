using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryGrid : MonoBehaviour
{
    GameObject player;
    public MeshRenderer renderer;
    Color materialColor;
    public float distanceToAppear;
    public float distanceToFade;
    private float originalAlpha;

    // Start is called before the first frame update
    void Start()
    {
        materialColor = renderer.material.color;
        originalAlpha = materialColor.a;
        materialColor.a = 0;
        player = GameObject.FindWithTag("Player");
        renderer.material.color = materialColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) 
        {
            if (player.transform.position.z < distanceToFade)
                materialColor.a = 0;

            if (player.transform.position.z >= distanceToFade && player.transform.position.z < distanceToAppear){
                
                float val = ((player.transform.position.z - distanceToFade)/(distanceToAppear - distanceToFade))*originalAlpha;

                materialColor.a = val;
            
            }

            if (player.transform.position.z >= distanceToAppear)
                materialColor.a = originalAlpha;

            renderer.material.color = materialColor;
        }
    }
}
