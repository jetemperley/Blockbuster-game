using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover1 : MonoBehaviour
{
    public float dampener = 20;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (Vector3.up * Mathf.Cos(Time.time))/dampener;
    }
}
