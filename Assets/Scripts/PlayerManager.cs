using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    // Creem tres nombres enters que representen el preu de les millores
    public static int hpNum = 5;
    public static int speedNum = 3;
    public static int shotNum = 2;

    // Creem quatre nombres decimals a mode d'estadístiques
    public static float bossHP = 5.0f;
    public static float playerHP = 10.0f;
    public static float speed = 5.0f;
    public static float shot = 0.0f;

    // Augmentem la salut del personatge
    public void IncrementHealth() {
        if (ScoreManager.Instance.score >= hpNum) {
            playerHP += 1.0f;
            ScoreManager.Instance.Decrement(hpNum);
            hpNum ++;
            Debug.Log("Health upgraded! Current health: " + playerHP);
        } else {
            StartCoroutine(BtnManager.AnimateBtn(BtnManager.Instance.hpText));
        }
    }

    // Augmentem la velocitat del personatge
    public void IncrementSpeed() {
        if (ScoreManager.Instance.score >= speedNum) {
            speed += 2.5f;
            ScoreManager.Instance.Decrement(speedNum);
            speedNum ++;
            Debug.Log("Speed upgraded! Current speed: " + speed);
        } else {
            StartCoroutine(BtnManager.AnimateBtn(BtnManager.Instance.speedText));
        }
    }

    // Augmentem la velocitat de la bala
    public void IncrementShot() {
        if (ScoreManager.Instance.score >= shotNum) {
            shot += 0.1f;
            ScoreManager.Instance.Decrement(shotNum);
            shotNum ++;
            Debug.Log("Shot upgraded! Current shot: " + shot);
        } else {
            StartCoroutine(BtnManager.AnimateBtn(BtnManager.Instance.shotText));
        }
    }
}
