using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TxtManager : MonoBehaviour {

    // Creem una variable pública i estàtica de tipus TextManager
    public static TxtManager Instance;

    // Creem una variable pública de tipus Text
    public Text endTxt;

    // Start is called before the first frame update
    void Start() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public IEnumerator ShowText(string scene) {
        
        // Establim el missatge del Text
        if (scene == "LoserScene") {
            endTxt.text = "Your spaceship has been destroyed!";
        } else if (SceneManager.GetActiveScene().name == "BossScene") {
            endTxt.text = "The Boss has been defeated!";
        } else {
            endTxt.text = "All the enemies have been anihilated!";
        }
        
        // Fem aparèixer i desaparèixer el Text durant 10 segons
        for (int i = 0; i < 30; i++) {
            endTxt.enabled = !endTxt.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        endTxt.enabled = false;

        // Canviem a l'Scene pertinent
        SceneManager.LoadScene(scene);
    }
}
