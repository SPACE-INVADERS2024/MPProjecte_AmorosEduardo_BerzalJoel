using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBullet : MonoBehaviour {
    
    // Creem una variable pública de tipus AudioSource
    /*public AudioSource shot;*/
    
    // Creem una variable pública de tipus GameObject
    public GameObject bulletPrefab;

    // Creem dues variables públiques de tipus Transform
    public Transform bulletSpawnPoint;

    // Creem un nombre decimal a mode de temporitzador
    float timer;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        
        timer += Time.deltaTime;

        if (timer > 2) {

            // Retornem el temporitzador al seu valor inicial
            timer = 0;

            // Fem que l'enemic dispari quan el temporitzador arribi a 2 segons
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            
            // Activem l'AudioSource del dispar quan es prem la barra d'espai
            /*shot.Play();*/
        }
    }
}
