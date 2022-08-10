using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url = "";
    // Start is called before the first frame update
    void OpenUrl()
    {
        Application.OpenURL(url);
    }
}
