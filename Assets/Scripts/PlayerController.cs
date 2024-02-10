using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    public GameController gc;
    TDActions controls;
    public GameObject mouse;
    public bool isShooting;
    private Camera main;
    public float speed = 10f;
    Rigidbody rb;
    public bool isJoypad = false;
    public VisualEffect shootingEffect;
    public float tween = 1f;
    public float tweenNow = 0f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float velPower = 1f;
    public GameObject topBoi;
    public GameObject bottomBoi;
    public GameObject gunCollide;
    public GameObject topPoint;
    public float angleOff = -90f;
    private float maxHealth;
    public GameObject lengthScaler;
    public float startLength = 0.1f;

    public float health = 50f;

    public float healthAdd = 0f;
    public float speedAdd = 0f;
    public float lengthAdd = 0f;
    public float timeAdd = 0f;
    public float lootMult = 0f;
    public float damageMult = 0f;
    public float coin = 0;

    private void Awake() {
        health += healthAdd;
        maxHealth = health;
        speed += speedAdd;
        LoadPlayer();
        controls = new TDActions();
        main = Camera.main;
        rb = GetComponent<Rigidbody>();
        lengthScaler.transform.localScale = new Vector3(startLength + lengthAdd,1f,1f);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }

    void Start(){
        controls.Player.Shoot.performed += ctx => StartShoot();
        controls.Player.Shoot.canceled += ctx => EndShoot();
        controls.Player.IsMouse.performed += ctx => isJoypad = false;
    }

    void StartShoot(){
        isShooting = true;
        gunCollide.SetActive(true);
    }

    void EndShoot(){
        isShooting = false;
        gunCollide.SetActive(false);
    }

    void Update(){
        if(!isJoypad){
            Vector2 mouseScreenPosition = controls.Player.MousePosition.ReadValue<Vector2>();
            Ray ray = main.ScreenPointToRay(mouseScreenPosition);
            if( Physics.Raycast(ray, out RaycastHit raycastHit)){
                mouse.transform.position = new Vector3(raycastHit.point.x, 0, raycastHit.point.z);
                //float angle = Mathf.Atan2(raycastHit.point.z, -raycastHit.point.x) * Mathf.Rad2Deg;
                //topBoi.transform.rotation = Quaternion.Euler(new Vector3(0f,angle+180,0f));
                topPoint.transform.position = new Vector3(mouse.transform.position.x,1.33f,mouse.transform.position.z);
                topBoi.transform.LookAt(topPoint.transform,Vector3.up);
                //topBoi.transform.rotation = Quaternion.Euler(new Vector3(0f, topBoi.transform.rotation.y * Mathf.Rad2Deg, 0f));
            }
        }

        Vector2 joystickPosition = controls.Player.JoyPosition.ReadValue<Vector2>();
        if(joystickPosition.x != 0 || joystickPosition.y != 0){
            isJoypad = true;
            float angle = Mathf.Atan2(joystickPosition.y, -joystickPosition.x) * Mathf.Rad2Deg;
            topBoi.transform.rotation = Quaternion.Euler(new Vector3(0f,angle+angleOff,0f));
        }

        if(isShooting){
            shootingEffect.SendEvent("OnPlay");
            if(tweenNow < tween){
                tweenNow += Time.deltaTime * 4f;
            }
        }else{
            tweenNow = 0;
        }

        //effectObj.transform.localScale = new Vector3(tweenNow+1f,tweenNow+1f,0f);
        shootingEffect.SetVector3("Scale", new Vector3(tweenNow,tweenNow,tweenNow));

        Vector2 movement = controls.Player.Movement.ReadValue<Vector2>();
        // rb.AddForce(new Vector3(movement.x, 0, movement.y));

        float targetSpeedX = movement.x * speed;
        float targetSpeedY = movement.y * speed;

        float speedDiffX = targetSpeedX - rb.velocity.x;
        float speedDiffY = targetSpeedY - rb.velocity.z;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? acceleration : deceleration;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? acceleration : deceleration;

        float moveX = Mathf.Pow(Mathf.Abs(speedDiffX) * accelRateX, velPower) * Mathf.Sign(speedDiffX);
        float moveY = Mathf.Pow(Mathf.Abs(speedDiffY) * accelRateY, velPower) * Mathf.Sign(speedDiffY);

        rb.AddForce(new Vector3(moveX, 0f, moveY));

        if(moveX >= 0.2f || moveY >= 0.2f){
            float angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg; 
            bottomBoi.transform.rotation = Quaternion.Euler(new Vector3 (0f,angle+angleOff,0f));
        } 
    } 
 
    public void SavePlayer(){ 
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null){
            healthAdd = data.healthAdd;
            speedAdd = data.speedAdd;
            lengthAdd = data.lengthAdd;
            timeAdd = data.timeAdd;
            lootMult = data.lootMult;
            damageMult = data.damageMult;
            coin = data.coin;
        }
    }

    
}
