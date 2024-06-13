using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void OnStart() {
        SceneManager.LoadScene("Lvl1");
    }

    public void OnVS()
    {
        SceneManager.LoadScene("Multiplayer");
    }
    public void OnExit() {
        Application.Quit();
    }
}
