using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Mlt_PlayerBullet : NetworkBehaviour
{
    public float speed = 10f;
    // Creem una variable tipus Camera
    Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Bullet Id: " + OwnerClientId);
        Debug.Log("Owner:" + IsOwner);
        Debug.Log("Server:" + IsServer);
        Debug.Log("Client:" + IsClient);
        Debug.Log("Host:" + IsHost);

    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
        
        if (Mathf.Abs(transform.position.y) > mainCamera.orthographicSize * mainCamera.aspect)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}

