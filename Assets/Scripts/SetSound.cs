using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSound : MonoBehaviour
{
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    public Text masterText;
    public Text sfxText;
    public Text musicText;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterSound", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxSound", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("musicSound", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        masterText.text = "" + (int)(masterSlider.value*100);
        sfxText.text = "" + (int)(sfxSlider.value*100);
        musicText.text = "" + (int)(musicSlider.value*100);
    }

}
