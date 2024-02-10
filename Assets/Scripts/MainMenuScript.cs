using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject OptionsMenuUI;

    // Load Homebase Scene \\
    public void StartGame(){
        SceneManager.LoadScene("Office");
    }

    // Switch To Options Menu \\
    public void GoToOptionsMenu(){
        MainMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }

    // Switch To Main Menu \\
    public void GoToMainMenu(){
        MainMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
    }

    // End The Program Standalone \\
    public void QuitGame(){
        Application.Quit();
    }
}
