using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGen : MonoBehaviour
{
    public WeaponModel[][] weaponPool = new WeaponModel[4][];
    public WeaponModel[] lv1Pool;
    public WeaponModel[] lv2Pool;
    public WeaponModel[] lv3Pool;
    public WeaponModel[] lv4Pool;

    public Pickup[] choices;

    public string[] flavourText;
    public string[] pickupText;

    public Text displayText;

    private bool choiceMade = false;

    void Awake()
    {
        weaponPool[0] = lv1Pool;
        weaponPool[1] = lv2Pool;
        weaponPool[2] = lv3Pool;
        weaponPool[3] = lv4Pool;
    }

    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        int randomNumber;

        for (int i = 0; i < choices.Length; i++)
        {
            randomNumber = random.Next(0, weaponPool[0].Length);
            choices[i].pickupPrefab = weaponPool[0][randomNumber].gameObject;
            choices[i].pickupModel = weaponPool[0][randomNumber].weaponModel;
            choices[i].displayString = weaponPool[0][randomNumber].weaponName;
        }

        randomNumber = random.Next(0, flavourText.Length);
        displayText.text = flavourText[randomNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (!choiceMade)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (choices[i] == null)
                {
                    ChoiceMade();
                }
            }
        }
    }

    void ChoiceMade()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i] != null)
                Destroy(choices[i].gameObject.transform.parent.gameObject);
        }
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, pickupText.Length);
        displayText.text = pickupText[randomNumber];
        choiceMade = true;
    }
}
