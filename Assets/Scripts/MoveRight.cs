using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 5f;
    public float maxPos = 1000f;
    
    // Update is called once per frame
    void Update()
    {
      transform.position = transform.position + new Vector3(speed*Time.deltaTime,0,0);
      if(transform.position.x > maxPos)
      {
          transform.position -= new Vector3(maxPos*2,0,0);
      }  
    }
}