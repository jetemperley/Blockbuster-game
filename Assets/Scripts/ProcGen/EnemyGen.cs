using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public GameObject[] groups; //Array of the different types of enemy groups to spawn

    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, groups.Length);
        Vector3 groupPosition = groups[randomNumber].transform.position;
        Vector3 objectPosition = gameObject.transform.position;
        Instantiate(groups[randomNumber], new Vector3(groupPosition.x, objectPosition.y, objectPosition.z), 
            groups[randomNumber].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
