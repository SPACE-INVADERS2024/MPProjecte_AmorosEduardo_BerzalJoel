using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BtnManager : MonoBehaviour {
    
    // Creem una variable pública i estàtica de tipus BtnManager
    public static BtnManager Instance;
    
    // Creem tres variables públiques de tipus TextMeshProUGUI
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI shotText;

    // Start is called before the first frame update
    void Start() {
        if (Instance == null) {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update() {
        hpText.text = PlayerManager.hpNum.ToString();
        speedText.text = PlayerManager.speedNum.ToString();
        shotText.text = PlayerManager.shotNum.ToString();
    }
    
    public static IEnumerator AnimateBtn(TextMeshProUGUI btnText) {
        // Canviem el color del text del botó durant 5 segons
        for (int i = 0; i < 5; i++) {
            btnText.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            btnText.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
