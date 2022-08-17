using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{

    public GameObject pickupPrefab;
    public GameObject pickupModel;
    string playerTag = "Player";

    public Text displayText;
    public string displayString;

    // Start is called before the first frame update
    void Start()
    {
        pickupModel = Instantiate(pickupModel);
        pickupModel.transform.SetParent(transform);
        pickupModel.transform.localPosition = Vector3.zero;
        pickupModel.transform.rotation = pickupModel.transform.rotation*transform.rotation;
        // pickupModel.transform.localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        displayText.text = displayString;
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(playerTag)){
            other.gameObject.GetComponent<GunHolder>().SetGun(Instantiate(pickupPrefab));
            Destroy(gameObject.transform.parent.gameObject);
        }
        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
