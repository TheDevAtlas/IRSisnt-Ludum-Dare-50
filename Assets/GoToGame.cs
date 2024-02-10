using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerController>().SavePlayer();
            SceneManager.LoadScene("Gameplay");
        }
    }
}
