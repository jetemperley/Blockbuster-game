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

    public float pickupLockout = 1.0f;
    private float timer;

    public WeaponGen weaponGen;

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
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag(playerTag) && timer <= 0){
            other.gameObject.GetComponent<GunHolder>().AddGun(Instantiate(pickupPrefab), this);
            timer = pickupLockout;
            if (weaponGen != null)
                weaponGen.ChoiceMade(this);
            //Destroy(gameObject.transform.parent.gameObject);
        }
        
    }

    public void SetGunPickup(GameObject gun)
    {
        pickupPrefab = Instantiate(gun.GetComponent<WeaponModel>().weaponPrefab);
        pickupPrefab.transform.SetParent(transform);
        pickupPrefab.transform.localPosition = Vector3.zero;
        pickupPrefab.SetActive(false);

        Destroy(gun);

        Destroy(pickupModel);
        pickupModel = Instantiate(pickupPrefab.GetComponent<WeaponModel>().weaponModel);
        pickupModel.transform.SetParent(transform);
        pickupModel.transform.localPosition = Vector3.zero;
        pickupModel.transform.rotation = pickupModel.transform.rotation*transform.rotation;

        displayString = pickupPrefab.GetComponent<WeaponModel>().weaponName;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
