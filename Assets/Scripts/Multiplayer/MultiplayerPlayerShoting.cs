using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Mlt_PlayerShoting : NetworkBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private Quaternion bulletRotation;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Debug.Log("Bullet Owner: " + OwnerClientId);


        bulletRotation = bulletPrefab.transform.rotation;
        
        if (OwnerClientId == 1)
        {
            bulletRotation = Quaternion.Inverse(bulletRotation);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;    

        if (Input.GetKeyDown(KeyCode.Space))
        {
            RequestSpawnBulletServerRpc();
        }

    }

    // Método llamado para instanciar una bala
    [ServerRpc]
    public void RequestSpawnBulletServerRpc()
    {
        SpawnBulletClientRpc(transform.position);
    }
    [ClientRpc]
    public void SpawnBulletClientRpc(Vector3 position)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not found at the specified path.");
            return;
        }

        if (OwnerClientId == 0)
        {
            var bulletInstance = Instantiate(bulletPrefab, position + Vector3.up, bulletRotation);
        }
        else if (OwnerClientId == 1)
        {
            var bulletInstance = Instantiate(bulletPrefab, position + Vector3.down, bulletRotation);
        }
    }


}

