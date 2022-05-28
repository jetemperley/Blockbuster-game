using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerCantact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        Health h = GetComponent<Health>();
        if (h != null)
            h.kill();
        if(other.gameObject.layer == 9){
            Destroy(gameObject);
        }        
    }
}
