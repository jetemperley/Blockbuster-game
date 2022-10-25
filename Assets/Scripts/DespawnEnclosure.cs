using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnEnclosure: MonoBehaviour
{

    public Vector3 allowedArea = new Vector3(10, 10, 10);
    public GameObject colliderCube;
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(1, allowedArea.y, allowedArea.z);
        g.transform.Translate(allowedArea.x/2, 0, 0);
        g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(1, allowedArea.y, allowedArea.z);
        g.transform.Translate(-allowedArea.x/2, 0, 0);

        g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(allowedArea.x, 1, allowedArea.z);
        g.transform.Translate(0, allowedArea.y/2, 0);
        g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(allowedArea.x, 1, allowedArea.z);
        g.transform.Translate(0, -allowedArea.y/2, 0);

        g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(allowedArea.x, allowedArea.y, 1);
        g.transform.Translate(0, 0, allowedArea.z/2);
        g = Instantiate(colliderCube);
        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localScale = new Vector3(allowedArea.x, allowedArea.y, 1);
        g.transform.Translate(0, 0, -allowedArea.z/2);
    }    

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, allowedArea);

    }
}
