using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour {
    // Creem una variable pública i estàtica de tipus EnemyCounter
    public static EnemyCounter Instance { get; private set; }

    // Creem un nombre enter que representa la quantitat d'enemics per pantalla
    public int enemyCount;

    // Start is called before the first frame update
    void Start() {
        if (Instance == null) {
            Instance = this;
        }
        enemyCount = 0;
    }

    public void Increment() {
        enemyCount++;
        Debug.Log("Enemies: " + enemyCount);
    }

    public void Decrement() {
        enemyCount--;
        Debug.Log("Enemies: " + enemyCount);
    }
}
