using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    // Creem una variable pública i estàtica de tipus ScoreManager
    public static ScoreManager Instance;

    // Creem una variable pública de tipus Text
    public Text scoreText;

    // Creem un nombre enter que representa la puntuació d'un nivell
    public int score;
    
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
            scoreText.text = "POINTS: " + score;
        } else {
            scoreText.text = "";
        }
    }

    // Retornem l'Score al seu valor inicial
    public void Reset() {
        score = 0;
    }

    // Incrementem el valor de l'Score
    public void Increment() {
        score ++;
    }

    // Reduïm el valor de l'Score
    public void Decrement(int n) {
        if (score >= n) {
            score -= n;
        }
    }
}
