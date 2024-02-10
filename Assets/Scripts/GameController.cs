using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timeRemaining = 10;
    public TextMeshProUGUI timerText;
    public GameObject EndScreen;
    public Image image;
    float change = 0;
    public PlayerController pc;
    public TextMeshProUGUI coinText;
    public float damage = 5f;
    public float loot = 1f;
    public GameObject lootObj;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    public GameObject s6;


    void Start(){
        timeRemaining += pc.timeAdd;
        damage *= 1+pc.damageMult;
        loot *= 1+pc.lootMult;
    }

    void Update()
    {
        coinText.text = pc.coin.ToString();

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int dispTime = (int)timeRemaining;
            timerText.text = dispTime.ToString();
        } else {
            timerText.text = "0";
        }

        if(timeRemaining <= 0){
            pc.SavePlayer();
            change += Time.deltaTime;
            EndScreen.SetActive(true);
            Color c = image.color;
            c.a = change;
            image.color = c;
            Destroy(s2);
            Destroy(s3);
            Destroy(s4);
            Destroy(s5);
            Destroy(s6);
            Destroy(s1);
            Destroy(pc);
        }
    }

    public void SpawnLoot(Vector3 location, int diff, PlayerController pc){
        GameObject g = Instantiate(lootObj, location, Quaternion.identity);
        PickUp p = g.GetComponent<PickUp>();
        p.gc = this;

        float l = Random.Range(0f,2f);
        if(l >= 1){
            p.coinPack.SetActive(true);
            p.healthPack.SetActive(false);
            p.coin = loot * (pc.lootMult + 1);
        }else{
            p.healthPack.SetActive(true);
            p.coinPack.SetActive(false);
            p.health = 10f + pc.timeAdd;
        }
    }

    public void AddTime(float add){

    }
}
