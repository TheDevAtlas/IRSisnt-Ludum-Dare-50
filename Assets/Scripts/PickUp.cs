using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float coin;
    public float health;

    public GameObject bounceObject;
    public GameObject coinPack;
    public GameObject healthPack;

    public Shader coinShader;
    public Shader healthShader;

    public Vector3 rotateSpeed;

    public float moveAmmount;
    public float moveSpeed = 1.1f;

    Vector3 pos1, pos2;

    public GameController gc;

    void Start(){
        pos1 = new Vector3(bounceObject.transform.position.x,bounceObject.transform.position.y,bounceObject.transform.position.z);
        pos2 = new Vector3(bounceObject.transform.position.x,bounceObject.transform.position.y + moveAmmount,bounceObject.transform.position.z);
    }

    void Update(){
        bounceObject.transform.Rotate(rotateSpeed * Time.deltaTime);

        //bounceObject.transform.position = Vector3.Lerp(pos1, pos2, moveSpeed * Time.deltaTime);

        float time = Mathf.PingPong(Time.deltaTime * moveSpeed, 1);
        transform.position = Vector3.Slerp(pos1, pos2, time);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            p.coin += coin;
            gc.timeRemaining += health;
            Destroy(gameObject);
        }
    }
}
