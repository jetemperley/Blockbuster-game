using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputLoader : MonoBehaviour
{
    static private PlayerInputLoader instance;

    static public PlayerInputLoader Instance{
        get{
            if(instance == null)
            {
                Debug.LogError("no player input");
            }
            return instance;
        }
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
