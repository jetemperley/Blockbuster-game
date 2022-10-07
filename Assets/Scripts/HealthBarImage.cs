using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarImage : MonoBehaviour
{
    public Image healthFill;
    public Text healthTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(int currentHealth, int maxHealth){
        healthFill.fillAmount = (float)currentHealth/(float)maxHealth;
        healthTxt.text = "" + (int)((float)currentHealth/(float)maxHealth*100);
    }
}
