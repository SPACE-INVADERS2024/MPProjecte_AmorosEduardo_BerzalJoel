using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour {

    // Creem una variable pública de tipus Bool
    public bool isSupport;

    // Creem un nombre decimal a mode de límit esquerre
    float leftBound;

    // Creem un nombre decimal a mode de límit dret
    float rightBound;

    // Creem un nombre decimal que determina la distància que descendirà l'enemic
    float descendDistance = 0f;

    // Creem dues variables tipus Bool per regular el moviment de l'enemic
    bool movingDown = false;
    bool movingRight = true;

    // Start is called before the first frame update
    void Start() {
        // Establim els valors dels límits dret i esquerre en funció de la posició d'inici
        leftBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        rightBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 0.5f;
    }

    // Update is called once per frame
    void Update() {

        if (!isSupport) {

            if (movingDown) {

                // Desplaçem l'enemic cap a sota
                transform.Translate(Vector2.down * 2.5f * Time.deltaTime);
                descendDistance += 2.5f * Time.deltaTime;

                // Si l'enemic arriba fins la distància desitjada, aquest deixa d'anar cap a sota
                if (descendDistance >= 1.0f) {
                    movingDown = false;
                    descendDistance = 0f;
                }

                // Sortim del mètode Update per prevenir altres accions de moviment de l'enemic
                return;

            } else if (movingRight) {

                // Desplaçem l'enemic cap a l'equerra
                transform.Translate(Vector2.right * 2.5f * Time.deltaTime);

                // Si l'enemic arriba fins el límit dret, canvia de direcció
                if (transform.position.x >= rightBound) {
                    movingRight = false;
                    movingDown = true;
                }

            } else {

                // Desplaçem l'enemic cap a la dreta
                transform.Translate(Vector2.left * 2.5f * Time.deltaTime);

                // Si l'enemic arriba fins el límit esquerre, canvia de direcció
                if (transform.position.x <= leftBound) {
                    movingRight = true;
                    movingDown = true;
                }
            }
        }
    }

    // Fem desaparèixer al jugador si un enemic impacta contra ell
    void OnTriggerEnter2D(Collider2D other) {
        if (!isSupport && other.CompareTag("Plyr")) {
            Destroy(other.gameObject);
            // Reproduïm l'animació del text de fi del nivell
            StartCoroutine(TxtManager.Instance.ShowText("LoserScene"));
        }
    }
}
