using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class EdgeGlow : MonoBehaviour
{

    public GameObject edge;
    private float edgeWidthScale = 4;
    // Start is called before the first frame update
    void Start()
    {
        Vertex[] arr = GetComponent<ProBuilderMesh>().GetVertices();
        // for (int i = 0; i < arr.Length; i++){
        //     GameObject g = Instantiate(sphere);
        //     g.name = "" + i;
        //     g.transform.SetParent(transform);
            

        //     g.transform.localPosition = arr[i].position;
        // }
        // 7 -> 6
        // 14 -> 15
        GameObject g = Instantiate(edge);
        g.transform.SetParent(transform);
        g.transform.localPosition = arr[7].position;
        Vector3 dir = arr[6].position - arr[7].position;
        g.transform.localRotation = Quaternion.LookRotation(dir);
        Vector3 scale = g.transform.localScale;
        scale.z =  dir.magnitude;
        scale.y = edgeWidthScale;
        scale.x = edgeWidthScale;
        g.transform.localScale = scale;

        g = Instantiate(edge);
        g.transform.SetParent(transform);
        g.transform.localPosition = arr[14].position;
        dir = arr[15].position - arr[14].position;
        g.transform.localRotation = Quaternion.LookRotation(dir);
        scale = g.transform.localScale;
        scale.z =  dir.magnitude;
        scale.y = edgeWidthScale;
        scale.x = edgeWidthScale;
        
        g.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
