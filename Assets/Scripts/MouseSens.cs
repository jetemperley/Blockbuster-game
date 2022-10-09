using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSens : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        if(PlayerPrefs.GetFloat("MouseSens", 0.5f) == null)
        {
            PlayerPrefs.SetFloat("MouseSens", 0.5f);
            PlayerPrefs.Save();
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("MouseSens", 0.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "x" + 2f * Mathf.Round(slider.value * 100f) / 100f;
    }

    public void OnEndDrag()
    {
        PlayerPrefs.SetFloat("MouseSens", Mathf.Round(slider.value * 100f) / 100f);
        PlayerPrefs.Save();
    }
}
