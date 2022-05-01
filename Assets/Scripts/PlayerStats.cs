using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            break;

            case "jump":
                jumps++;
            break;

            case "dash":
                dashs++;
            break;

            case "ExplosiveProjectile":
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
}
