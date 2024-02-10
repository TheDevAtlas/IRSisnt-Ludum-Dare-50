using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5f;
    float currentTime = 0f;
    float offset = 0f;

    public GameObject[] badBuys;

    void Start(){
        offset = Random.Range(0f, 8f);
        spawnTime = Random.Range(spawnTime*offset, spawnTime-offset);
    }

    void Update(){
        currentTime += Time.deltaTime;

        if(currentTime >= spawnTime + offset){
            currentTime = 0f;
            GameObject g = Instantiate(badBuys[0], transform.position, transform.rotation);
            g.transform.SetParent(transform,true);
        }
    }
}
