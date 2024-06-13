using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBullet : MonoBehaviour {

    // Creem un nombre decimal que representa la vitalitat actual del Boss
    float currentBossHP;

    // Creem una variable tipus Rigidbody2D
    Rigidbody2D rb2D;

    // Creem una variable tipus Camera
    Camera mainCamera;

    // Creem una variable tipus SpawnEnemy
    SpawnEnemy spawnEnemy;
    
    // Creem una variable tipus GameObject
    GameObject bossObject;

    // Creem una variable pública i estàtica de tipus SpriteRenderer
    SpriteRenderer bossRenderer;

    // Start is called before the first frame update
    void Start() {

        currentBossHP = PlayerManager.bossHP;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        spawnEnemy = FindObjectOfType<SpawnEnemy>();

        if (SceneManager.GetActiveScene().name == "BossScene") {
            bossObject = GameObject.FindGameObjectWithTag("Boss");
            bossRenderer = bossObject.GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update() {
        
        // Desplacem la bala
        rb2D.velocity = Vector2.up * 5f;

        // Establim la transparència del Boss
        if (SceneManager.GetActiveScene().name == "BossScene") {
            if (currentBossHP == PlayerManager.bossHP && EnemyCounter.Instance.enemyCount == 0) {
                SetBossTransparency(1.0f);
            } else {
                SetBossTransparency(0.5f);
            }
        }
    }

    void SetBossTransparency(float transparency) {
        if (bossRenderer != null) {
            Color currentColor = bossRenderer.color;
            currentColor.a = transparency;
            bossRenderer.color = currentColor;
        }
    }

    string SetLvlName() {
        string lvlName = "";
        if (SceneManager.GetActiveScene().name == "Lvl1") {
            lvlName = "Lvl2";
        } else if (SceneManager.GetActiveScene().name == "Lvl2") {
            lvlName = "Lvl3";
        } else if (SceneManager.GetActiveScene().name == "Lvl3") {
            lvlName = "SampleScene";
        } else if (SceneManager.GetActiveScene().name == "SampleScene") {
            lvlName = "BossScene";
        }
        return lvlName;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Enemy")) {
            
            // Cridem a la funció Increment d'ScoreManager per disminuir el valor d'Score
            ScoreManager.Instance.Increment();

            // Fem desaparèixer a un enemic si una bala impacta contra ell
            Destroy(other.gameObject);
            
            // Cridem a la funció Increment d'EnemyCounter per disminuir el valor d'enemyCount
            EnemyCounter.Instance.Decrement();

            if (EnemyCounter.Instance.enemyCount == 0 && SceneManager.GetActiveScene().name != "BossScene") {
                // Reproduïm l'animació del text de fi del nivell
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                StoreManager.Instance.SetLvl(SetLvlName());
                StartCoroutine(TxtManager.Instance.ShowText("Store"));
            } else {
                // Fem desaparèixer la bala del jugador quan impacti contra un enemic
                Destroy(gameObject);
            }
        }

        if (currentBossHP == PlayerManager.bossHP && EnemyCounter.Instance.enemyCount == 0 && other.CompareTag("Boss")) {
            if (PlayerManager.bossHP == 1) {
                // Fem desaparèixer al Boss si una bala impacta contra ell i perd tota la vitalitat
                Destroy(other.gameObject);
                // Fem desaparèixer la bala del Boss quan impacti contra el Boss
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                // Reproduïm l'animació del text de fi del nivell
                StartCoroutine(TxtManager.Instance.ShowText("WinnerScene"));
            } else {
                // Reproduïm l'animació del Boss
                StartCoroutine(DamageBoss(bossRenderer));
            }
        }
    }

    public IEnumerator DamageBoss(SpriteRenderer bossRenderer) {

        // Reduïm la vitalitat del Boss
        PlayerManager.bossHP --;
        Debug.Log("Boss damaged! Current boss health: " + PlayerManager.bossHP);

        // Impedim que el Boss pugui disparar mentre es reprodueixi l'animació
        BossShootBullet.canShoot = false;

        // Fem aparèixer i desaparèixer l'sprite del Boss durant 10 segons
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < 20; i++) {
            bossRenderer.enabled = !bossRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        
        // Tornem a carregar l'Scene per a que tornin a aparèixer enemics de manera aleatòria
        SceneManager.LoadScene("BossScene");
    }
}
