using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject helpUI;
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        helpUI = GameObject.FindGameObjectWithTag("UI_Help");
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "MainMenu") helpUI.SetActive(false);
    }

    void Update(){
        if(Input.GetButtonDown("Cancel") && currentScene.name != "MainMenu"){
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("MainMenu");
        }
        
        if(currentScene.name == "MainMenu"){
            Cursor.lockState = CursorLockMode.None;
            if(Input.GetButtonDown("Cancel")) helpUI.SetActive(false);
        }
    }

    public void LoadTestingRoom() {
        SceneManager.LoadScene("TestingRoom");
    }

    public void LoadRoom() {
        SceneManager.LoadScene("Room");
    }

    public void Help() {
        helpUI.SetActive(true);
    }
}
