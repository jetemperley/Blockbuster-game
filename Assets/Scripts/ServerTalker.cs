using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerTalker : MonoBehaviour
{
    [SerializeField]
    private string url = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeImuncEjWfrFVEE-joCW1IxG83RbtGpwTC4PQKUXh8vaY7aA/formResponse";
    
    public void SendData()
    {
        StartCoroutine(Post());   
    }


    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.34897578", (Random.Range(0,1000000).ToString()));

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
    }
}
