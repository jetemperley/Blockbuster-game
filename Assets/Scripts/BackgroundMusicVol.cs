using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicVol : MonoBehaviour
{
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioData.volume = PlayerPrefs.GetFloat("musicSound",1f)*PlayerPrefs.GetFloat("masterSound",1f);
    }
}
