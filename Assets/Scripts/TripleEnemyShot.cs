using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleEnemyShot : MonoBehaviour {

    // Creem una variable pública de tipus AudioSource
    /*public AudioSource shot;*/
    
    // Creem tres variables públiques de tipus GameObject
    public GameObject bulletPrefabLeft;
    public GameObject bulletPrefabDown;
    public GameObject bulletPrefabRight;

    // Creem tres variables públiques de tipus Transform
    public Transform bulletSpawnPointLeft;
    public Transform bulletSpawnPointDown;
    public Transform bulletSpawnPointRight;

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
            Instantiate(bulletPrefabLeft, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
            Instantiate(bulletPrefabDown, bulletSpawnPointDown.position, bulletSpawnPointDown.rotation);
            Instantiate(bulletPrefabRight, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
            
            // Activem l'AudioSource del dispar quan es prem la barra d'espai
            /*shot.Play();*/
        }
    }
}
