using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour {

    // Creem una variable pública i estàtica de tipus Store
    public static StoreManager Instance;

    // Creem una cadena de text a mode de nom d'una Scene
    string lvl;

    // Start is called before the first frame update
    void Start() {

        // Instanciem el StoreManager
        if (Instance == null) {
            Instance = this;
        }

        // Inicialitzem les variables
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
        lvl = PlayerPrefs.GetString("Scene", "");
        Debug.Log("Next Scene: " + lvl);
    }

    public void SetLvl(string lvl) {
        PlayerPrefs.SetString("Scene", lvl);
    }

    public void OnUpgraded() {
        SceneManager.LoadScene(lvl);
    }
}
