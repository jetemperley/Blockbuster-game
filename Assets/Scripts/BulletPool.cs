using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool
{


    private static LaserPool single;
    private List<Laser> pool;

    LaserPool(){
        pool = new List<Laser>(100);
        for (int i  = 0; i < 100; i++){
            AddLaser();
        }

    }

    private Laser AddLaser(){
        Laser g = (new GameObject()).AddComponent<Laser>();
        pool.Add(g);
        return g;
    }

    public static Laser GetLaser(){
        if (single == null)
            single = new LaserPool();
        foreach (Laser g in single.pool){
            if (!g.gameObject.activeSelf){
                g.gameObject.SetActive(true);
                return g;
            }
        }
        Laser newg = single.AddLaser();
        newg.gameObject.SetActive(true);
        return newg;
    }

    public static void Init(){
        if (single == null)
            single = new LaserPool();
    }

}
