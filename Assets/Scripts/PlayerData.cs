using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float healthAdd;
    public float speedAdd;
    public float lengthAdd;
    public float timeAdd;
    public float lootMult;
    public float damageMult;
    public float coin;

    public PlayerData (PlayerController data){
        healthAdd = data.healthAdd;
        speedAdd = data.speedAdd;
        lengthAdd = data.lengthAdd;
        timeAdd = data.timeAdd;
        lootMult = data.lootMult;
        damageMult = data.damageMult;
        coin = data.coin;
    }
}
