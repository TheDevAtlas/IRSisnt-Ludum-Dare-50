using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OfficeController : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI range;
    public TextMeshProUGUI loot;
    public TextMeshProUGUI dmg;
    public TextMeshProUGUI coin;
    public TextMeshProUGUI BuyInstructions;

    public PlayerController pc;

    void Update()
    {
        time.text = (pc.timeAdd + 30f).ToString();
        speed.text = pc.speedAdd.ToString();
        range.text = pc.lengthAdd.ToString();
        loot.text = pc.lootMult.ToString();
        dmg.text = pc.damageMult.ToString();
        coin.text = pc.coin.ToString();
    }
}
