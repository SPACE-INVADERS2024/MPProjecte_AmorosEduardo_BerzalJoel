using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalEnemyShot : MonoBehaviour {
    
    // Creem una variable pública de tipus AudioSource
    /*public AudioSource shot;*/
    
    // Creem dues variables públiques de tipus GameObject
    public GameObject bulletPrefabLeft;
    public GameObject bulletPrefabRight;

    // Creem dues variables públiques de tipus Transform
    public Transform bulletSpawnPointLeft;
    public Transform bulletSpawnPointRight;

    // Creem un nombre decimal a mode de temporitzador
    float timer;

    // Update is called once per frame
    void Update() {
        
        timer += Time.deltaTime;

        if (timer > 2) {

            // Retornem el temporitzador al seu valor inicial
            timer = 0;

            // Fem que l'enemic dispari quan el temporitzador arribi a 2 segons
            Instantiate(bulletPrefabLeft, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
            Instantiate(bulletPrefabRight, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
            
            // Activem l'AudioSource del dispar quan es prem la barra d'espai
            /*shot.Play();*/
        }
    }
}
