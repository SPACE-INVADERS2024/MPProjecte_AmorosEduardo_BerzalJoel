using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootBullet : MonoBehaviour {
    
    // Creem una variable pública de tipus AudioSource
    /*public AudioSource shot;*/
    
    // Creem cinc variables públiques de tipus GameObject
    public GameObject bulletPrefabLeft_1;
    public GameObject bulletPrefabLeft_2;
    public GameObject bulletPrefabDown;
    public GameObject bulletPrefabRight_1;
    public GameObject bulletPrefabRight_2;

    // Creem cinc variables públiques de tipus Transform
    public Transform bulletSpawnPointLeft_1;
    public Transform bulletSpawnPointLeft_2;
    public Transform bulletSpawnPointDown;
    public Transform bulletSpawnPointRight_1;
    public Transform bulletSpawnPointRight_2;

    // Creem una variable pública de tipus Bool
    public static bool canShoot;

    // Creem un nombre decimal a mode de temporitzador
    float timer;

    // Start is called before the first frame update
    void Start() {
        canShoot = true;
    }

    // Update is called once per frame
    void Update() {

        if (canShoot) {

            timer += Time.deltaTime;

            if (timer > 2) {

                // Retornem el temporitzador al seu valor inicial
                timer = 0;

                // Fem que l'enemic dispari quan el temporitzador arribi a 2 segons
                Instantiate(bulletPrefabLeft_1, bulletSpawnPointLeft_1.position, bulletSpawnPointLeft_1.rotation);
                Instantiate(bulletPrefabLeft_2, bulletSpawnPointLeft_2.position, bulletSpawnPointLeft_2.rotation);
                Instantiate(bulletPrefabDown, bulletSpawnPointDown.position, bulletSpawnPointDown.rotation);
                Instantiate(bulletPrefabRight_1, bulletSpawnPointRight_1.position, bulletSpawnPointRight_1.rotation);
                Instantiate(bulletPrefabRight_2, bulletSpawnPointRight_2.position, bulletSpawnPointRight_2.rotation);
                
                // Activem l'AudioSource del dispar quan es prem la barra d'espai
                /*shot.Play();*/
            }
        }
    }
}
