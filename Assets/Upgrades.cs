using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public bool isStanding;
    public PlayerController pc;
    public OfficeController oc;
    public float cost;
    
    public bool time;
    public bool speed;
    public bool range;
    public bool loot;
    public bool dmg;

    TDActions controls;

    void Awake(){
        controls = new TDActions();
    }
    
    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    void Start(){
        controls.Player.Action.performed += ctx => TryBuy();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            isStanding = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            isStanding = false;
        }
    }

    public void TryBuy(){
        if(pc.coin >= cost){
            if(isStanding){
                pc.coin -= cost;
                if(time){
                    pc.timeAdd += 5f;
                }
                if(speed){
                    pc.speedAdd += 2.5f;
                }
                if(range){
                    pc.lengthAdd += 0.25f;
                }
                if(loot){
                    pc.lootMult += 1f;
                }
                if(dmg){
                    pc.damageMult += 0.1f;
                }
            }
        }else{
            oc.BuyInstructions.text = "You Don't Have The Money! Ha! Just Make More LOL";
        }
    }
}
