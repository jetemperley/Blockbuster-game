using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletLifeSpan;

    private Rigidbody rb;
    private Vector3 pos;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + dir*bulletSpeed*Time.deltaTime);
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
