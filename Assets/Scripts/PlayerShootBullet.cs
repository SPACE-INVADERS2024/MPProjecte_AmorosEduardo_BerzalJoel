using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBullet : MonoBehaviour {

    // Creem una variable pública de tipus AudioSource
    /*public AudioSource shot;*/
    
    // Creem una variable pública de tipus GameObject
    public GameObject bulletPrefab;

    // Creem una variable pública de tipus Transform
    public Transform bulletSpawnPoint;

    // Creem una variable pública de tipus Bool
    public static bool canShoot;

    // Creem una variable tipus Float
    float cooldown;

    // Start is called before the first frame update
    void Start() {
        canShoot = true;
        cooldown = 1.0f;
    }

    // Update is called once per frame
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space) && canShoot) {

            // Disparem una bala quan es prem la barra d'espai
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Deshabilitem la capacitat de disparar
            canShoot = false;

            // Iniciem el cooldown
            StartCoroutine(ShootCooldown());

            // Activem l'AudioSource del dispar quan es prem la barra d'espai
            /*shot.Play();*/
        }
    }

    IEnumerator ShootCooldown() {

        // Esperem a que passi el temps de cooldown
        yield return new WaitForSeconds(cooldown - PlayerManager.shot);

        // Un cop s'acabi el cooldown, permetem que el jugador torni a disparar
        canShoot = true;
    }
}
