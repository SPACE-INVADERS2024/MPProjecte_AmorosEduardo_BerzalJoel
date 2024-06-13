using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    // Creem una variable pública de tipus GameObject
    public GameObject enemyPrefab;

    // Creem una variable pública de tipus Transform
    public Transform enemySpawnPoint;

    // Creem dues variables públiques de tipus Bool
    public bool isSpawnable;
    public bool isRandom;

    // Creem un nombre enter a mode de comptador
    int spawnCounter;

    // Start is called before the first frame update
    void Start() {
        spawnCounter = 0;
    }

    // Update is called once per frame
    void Update() {
        // Fem aparèixer l'enemic mentre aquest encara no hagi aparegut al mapa
        if (spawnCounter < 1 && enemyPrefab != null && enemySpawnPoint != null && isSpawnable) {
            if (isRandom) {
                if (Random.Range(0, 2) == 1) {
                    // Fem aparèixer l'enemic
                    Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
                    // Cridem a la funció Increment d'EnemyCounter per augmentar el valor d'enemyCount
                    EnemyCounter.Instance.Increment();
                }
            } else {
                // Fem aparèixer l'enemic
                Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
                // Cridem a la funció Increment d'EnemyCounter per augmentar el valor d'enemyCount
                EnemyCounter.Instance.Increment();
            }
            spawnCounter ++;
        }
    }
}
