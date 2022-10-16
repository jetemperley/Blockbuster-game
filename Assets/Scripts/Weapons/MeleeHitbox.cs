using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    public float activeTime;
    public int damage;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = activeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= (Time.deltaTime);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.attachedRigidbody.gameObject.tag != "Player")
        {
            try{
                Health health = collision.attachedRigidbody.gameObject.GetComponent<Health>();
                if (health != null){
                    health.takeDamage(damage);
                }
            } catch{}
        }
    }
}
