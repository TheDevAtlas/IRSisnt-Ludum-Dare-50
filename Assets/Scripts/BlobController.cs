using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject target;
    public float health = 100f;
    float maxH;
    public Shader toonShader;
    public Material newMat;
    public Color color;
    public bool isHit;
    public GameController GC;
    public SkinnedMeshRenderer GFX;
    public float timeSubIfHit = 5f;
    public int hardness;
    public float speedIncrease = 1f;
 
    void Start(){
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        target = GameObject.Find("Player");

        newMat = new Material(toonShader);
        newMat.color = color;
        GFX.material = newMat;
        maxH = health;
    }

    void Update(){
        transform.LookAt(target.transform.position);
        transform.Translate(Vector3.forward * speed);
        GFX.sharedMaterial.SetFloat("_Outline",health/maxH);
        if(target.GetComponent<PlayerController>().gunCollide.activeInHierarchy == false){
            isHit = false;
        }
    }

    private void FixedUpdate() {
        if(isHit){
            health -= Time.deltaTime * GC.damage;
        }

        if(health <= 0){
            GC.SpawnLoot(transform.position, hardness,target.GetComponent<PlayerController>());
            Destroy(gameObject);
        }

        speed += speedIncrease * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            GC.timeRemaining -= timeSubIfHit;
            // Create Bad Explosion + Camera Shake Big \\
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Beam"){
            isHit = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Beam"){
            isHit = false;
        }
    }
}
