using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEngine.Networking;

using System.IO;
using System;

public class PlayerStats : MonoBehaviour
{

    private static PlayerStats inst;
    public int killCube = 0, killShield = 0, killTri = 0, killDiam = 0;
    public int miniKill = 0, pistolKill = 0, cannonKill = 0;
    public int dCube = 0, dShield = 0, dTri = 0, dDiam = 0, dSpike = 0, dFall = 0, dPlayer = 0;
    public int jumps = 0, dashs = 0, falls = 0;

    public float time = 0;
    // Start is called before the first frame update

    private void Awake() {
        if (inst == null)
            inst = this;
        else if (inst != this){
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
    }

    private void Update() {
        time += Time.deltaTime;
    }

    public static PlayerStats getInst(){
        if (inst == null){
            GameObject g = new GameObject();
            g.isStatic = true;
            inst = g.AddComponent<PlayerStats>();

        }
        return inst;
    }

    public void reset(){
        killShield = 0;
        killCube= 0;
        killTri= 0;
        killTri= 0;
        dPlayer= 0;
        jumps= 0;
        dashs= 0;
        cannonKill= 0;
        miniKill= 0;
        miniKill= 0;
    }

    public void log(){
        Debug.Log("------------------------------");
        Debug.Log("cube kills " + killCube);
        Debug.Log("shield kills " + killShield);
        Debug.Log("tri kills " + killTri);
        Debug.Log("diam kills " + killDiam);
        Debug.Log("player deaths " + dPlayer);
        Debug.Log("jumps " + jumps);
        Debug.Log("dashs " + dashs);
        Debug.Log("cannon kills " + cannonKill);
        Debug.Log("mini kills " + miniKill);
        Debug.Log("pistol kills " + pistolKill);
        Debug.Log("time " + time);
        Debug.Log("------------------------------");

    }

    public void deathLog(){

        

        AnalyticsResult result = Analytics.CustomEvent("death event", 
            new Dictionary<string, object> 
            {
                {"cubesKilled", killCube},
                {"shieldsKilled", killShield},
                {"trianglesKilled", killTri},
                {"diamondKilled", killDiam},

            }
        );
        Debug.Log("analytics result " + result);
    }

    public void addStat(string name){
        switch (name) {
            case "kill CubeShield":
                killShield++;
            break;

            case "kill NormCube":
                killCube++;
            break;

            case "kill PyramidEnemy":
                killTri++;
            break;

            case "kill DiamondEnemy":
                killDiam++;
            break;

            case "kill Player":
                dPlayer++;
                deathLog();

            break;

            case "jump":
                jumps++;
            break;

            case "dash":
                dashs++;
            break;

            case "ExplosiveProjectile(Clone)":
                cannonKill++;
            break;

            case "Minigun":
                miniKill++;
            break;

            case "Pistol":
                pistolKill++;
            break;

            case "fall":
                falls++;
            break;

            
        }


    }
    public void addStatAnalytic(string name, GameObject thingy){

        //StartCoroutine(Upload(thingy.transform.position));

        Analytics.CustomEvent(
            "Player Died",
            new Dictionary<string, object>{
                {"Position", thingy.transform.position},
            }
        );
        // addPlayerDeathToCSV(thingy.transform.position);
    }

    private void addPlayerDeathToCSV(Vector3 pos){
        
        string fname = getPosFilename();
        if (!File.Exists(fname)){
            File.Create(fname);
        }
        try {
            File.AppendAllText(fname, pos.x + " " + pos.y + " " + (pos.z+Conductor.getConductor().getDistTraveled()) + ",\n");
        } catch (Exception e) {
            Debug.Log("Could not write death position to file: " + e.ToString());
            
        }
    }

    public static string getPosFilename(){
        return "./" + SceneManager.GetActiveScene().name + "Deaths.txt";
    }

    IEnumerator Upload(Vector3 loc)
    {
        Debug.Log("setting up url");
        UnityWebRequest www = UnityWebRequest.Get("http://193.119.107.178:91/log.php?" + 
            "level="+SceneManager.GetActiveScene().name+"&"+
            "x="+loc.x+"&"+
            "y="+loc.y+"&"+
            "z="+(loc.z+Conductor.getConductor().getDistTraveled())
            );
        Debug.Log(www.url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
