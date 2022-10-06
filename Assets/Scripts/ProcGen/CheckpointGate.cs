using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointGate : MonoBehaviour
{
    public Transform teleportLoc;
    public string targetTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.tag == targetTag)
        {
            Transform player = other.attachedRigidbody.gameObject.transform;
            player.position = teleportLoc.position;
            other.attachedRigidbody.velocity = Vector3.zero;
            TerrainGen.instance.ProgressCheckpoint();
            Health playerHealth = other.attachedRigidbody.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                if (playerHealth.currentHealth < playerHealth.maxHealth)
                    playerHealth.currentHealth = playerHealth.maxHealth;
                else
                    playerHealth.shield.currentHealth = playerHealth.shield.maxHealth;
            }
            Destroy(this.gameObject);
        }
    }
}
