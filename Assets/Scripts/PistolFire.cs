using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFire : MonoBehaviour
{
    public Camera cam;
    public Bullet bulletPrefab;
    public float fireCooldown; //seconds
    AudioSource audioData;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
        {
            audioData.Play(0);
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = transform;
            bullet.transform.localPosition = new Vector3(0,0,1f);
            bullet.transform.parent = null;
            bullet.SetDir(cam.transform.forward);
            fireTimer = fireCooldown;
        }
        fireTimer -= Time.deltaTime;
    }
}
