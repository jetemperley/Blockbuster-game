using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillDashBar : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateBar(float currentDashtimer, float maxDashtime){
        slider.value = currentDashtimer/maxDashtime;
    }
}
