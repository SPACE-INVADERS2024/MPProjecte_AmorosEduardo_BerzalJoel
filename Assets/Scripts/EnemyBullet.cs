using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : MonoBehaviour {

    // Creem un nombre decimal públic que representa l'angle de la bala
    public float bulletAngle;

    // Creem una variable pública de tipus Bool
    public bool isBoss;

    // Creem una variable tipus Rigidbody2D
    Rigidbody2D rb2D;

    // Creem una variable tipus Camera
    Camera mainCamera;

    // Creem una variable tipus Vector2
    Vector2 bulletDirection;

    // Creem un nombre decimal que representa la velocitat de la bala
    float bulletSpeed;

    // Creem un nombre decimal que representa la vitalitat actual del jugador
    float currentPlayerHP;

    // Start is called before the first frame update
    void Start() {

        currentPlayerHP = PlayerManager.playerHP;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        if (isBoss) {
            bulletSpeed = 10.0f;
        } else {
            bulletSpeed = Random.Range(5.0f, 10.0f);
        }

        bulletDirection = new Vector2(Mathf.Cos(bulletAngle * Mathf.Deg2Rad), Mathf.Sin(bulletAngle * Mathf.Deg2Rad)) * bulletSpeed;
    }

    // Update is called once per frame
    void Update() {
        
        // Desplacem la bala
        rb2D.velocity = bulletDirection;

        // Fem desaparèixer la bala quan surti dels límits de la pantalla
        /*
        if (Mathf.Abs(transform.position.y) > mainCamera.orthographicSize * mainCamera.aspect) {
            Destroy(gameObject);
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Fem desaparèixer al jugador si una bala impacta contra ell
        if (currentPlayerHP == PlayerManager.playerHP && other.CompareTag("Plyr")) {
            if (PlayerManager.playerHP == 1) {
                // Fem desaparèixer la bala d'un enemic quan impacti contra el jugador
                Destroy(other.gameObject);
                // Reproduïm l'animació del text de fi del joc
                StartCoroutine(TxtManager.Instance.ShowText("LoserScene"));
            } else {
                // Reduïm la vitalitat del jugador
                PlayerManager.playerHP --;
                Debug.Log("Player damaged! Current health: " + PlayerManager.playerHP);
                // Reproduïm l'animació del jugador
                StartCoroutine(DamagePlayer(other.gameObject));
            }
        }
    }

    // Fem que el jugador dugui a terme una rotació de 360º
    public IEnumerator DamagePlayer(GameObject playerObject) {

        // Impedim que el jugador es pugui moure mentre es reprodueixi l'animació
        PlayerStateController.canMove = false;

        // Impedim que el jugador pugui disparar mentre es reprodueixi l'animació
        PlayerShootBullet.canShoot = false;

        // Inicialitzem les variables
        float duration = 1.0f;
        float elapsed = 0.0f;

        // Canviem la visibilitat de la bala d'un enemic quan impacti contra el jugador
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Ens assegurem que la rotació s'inicia en la posició actual del jugador
        Quaternion initialRotation = playerObject.transform.rotation;
        Quaternion finalRotation = initialRotation * Quaternion.Euler(0, 0, 360);

        while (elapsed < duration) {
            float angle = Mathf.Lerp(0, 360, elapsed / duration);
            playerObject.transform.rotation = initialRotation * Quaternion.Euler(0, 0, angle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ens assegurem que la rotació finalitza exactament al final de la rotació
        playerObject.transform.rotation = finalRotation;

        // Tornem a permetre al jugador la possibilitat de moure's
        PlayerStateController.canMove = true;
        
        // Tornem a permetre al jugador la possibilitat de disparar
        PlayerShootBullet.canShoot = true;
        
        // Fem desaparèixer la bala d'un enemic quan impacti contra el jugador
        Destroy(gameObject);
    }
}
