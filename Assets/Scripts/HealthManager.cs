using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    // Creem una variable pública i estàtica de tipus HealthManager
    public static HealthManager Instance;

    // Creem una variable pública de tipus Text
    public Text hpText;

    // Creem un nombre enter que representa la vitalitat del jugador
    public int hp;
    
    // Start is called before the first frame update
    void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name != "MainMenu" &&
            SceneManager.GetActiveScene().name != "LoserScene" &&
            SceneManager.GetActiveScene().name != "WinnerScene") {
            hpText.text = "HEALTH: " + PlayerManager.playerHP;
        } else {
            hpText.text = "";
        }
    }
}
