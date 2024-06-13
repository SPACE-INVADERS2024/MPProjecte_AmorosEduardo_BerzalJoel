using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public void OnReplay() {
        SceneManager.LoadScene("Lvl1");
    }

    public void OnReturn() {
        SceneManager.LoadScene("MainMenu");
    }
}
