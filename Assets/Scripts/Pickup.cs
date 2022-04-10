using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject pickupPrefab;
    public GameObject pickupModel;
    string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        pickupModel = Instantiate(pickupModel);
        pickupModel.transform.SetParent(transform);
        pickupModel.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(playerTag)){
            other.gameObject.GetComponent<GunHolder>().SetGun(Instantiate(pickupPrefab));
            Destroy(gameObject);
        }
        
    }
}
