using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeSpan;
    
    private Vector3 pos;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir*bulletSpeed*Time.deltaTime);
        if(bulletLifeSpan>0){
            bulletLifeSpan-=Time.deltaTime;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void SetDir(Vector3 direction){
        dir = direction;
    }
}
