using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool
{
    private static AudioPool single;
    private List<AudioSource> pool;

    AudioPool(){
        pool = new List<AudioSource>(100);
        for (int i  = 0; i < 100; i++){
            AddAudioSource();
        }

    }

    private AudioSource AddAudioSource(){
        AudioSource g = (new GameObject()).AddComponent<AudioSource>();
        Object.DontDestroyOnLoad(g.gameObject);
        pool.Add(g);
        return g;
    }

    public static AudioSource GetAudioSource(){
        if (single == null)
            single = new AudioPool();

        foreach (AudioSource g in single.pool){
            if(!g.isPlaying)
            {
                g.gameObject.SetActive(false);
            }
            if (!g.gameObject.activeSelf){
                g.gameObject.SetActive(true);
                return g;
            }
        }
        AudioSource newg = single.AddAudioSource();
        newg.gameObject.SetActive(true);
        return newg;
    }

    public static void Init(){
        if (single == null)
            single = new AudioPool();
    }

}
