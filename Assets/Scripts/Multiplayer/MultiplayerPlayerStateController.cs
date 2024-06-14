using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;


public class Mlt_PlayerStateController : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 startPlayer1Position = new Vector3(0, -4);
    private Vector3 startPlayer2Position = new Vector3(0, 4);


    public static float healthP1 = 3f;
    public static float healthP2 = 3f;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        RequestSetPlayerPositionServerRpc();

        GetNumConnectedPlayers();
    }

    public override void OnNetworkDespawn()
    {    
   
        Debug.Log("Health1:  " + healthP1 + "Health2:  " + healthP2);
        DestroyNetworkClientRPC();

        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        RequestUpdateHpServerRpc();

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Owner:" + IsOwner);
            Debug.Log("Server:" + IsServer);
            Debug.Log("Client:" + IsClient);
            Debug.Log("Host:" + IsHost);
        }



        if (!IsHost)
        {
            // Lógica de movimiento
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0f, 0f) * -speed * Time.deltaTime);

        }
        else if (IsHost)
        {
            // Lógica de movimiento
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime);
        }


        if (OwnerClientId == 0 && healthP1 > 0 && healthP2 <= 0)
        {
            SceneManager.LoadScene("MLT_WinnerScene");

        }
        else if (OwnerClientId == 0 && healthP1 <= 0 && healthP2 > 0)
        {
            SceneManager.LoadScene("MLT_LoserScene");
        }

        if (OwnerClientId == 1 && healthP2 > 0 && healthP1 <= 0)
        {
            SceneManager.LoadScene("MLT_WinnerScene");
        }
        else if (OwnerClientId == 1 && healthP2 <= 0 && healthP1 > 0)
        {
            SceneManager.LoadScene("MLT_LoserScene");
        }

        if (healthP1 <= 0 || healthP2 <= 0)
        {
            healthP1 = 3f;
            healthP2 = 3f;

        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (OwnerClientId == 0) healthP1 -= 1;

        else if (OwnerClientId == 1) healthP2 -= 1;

    }

    public int GetNumConnectedPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Plyr");
        return players.Length;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestSetPlayerPositionServerRpc()
    {
        setPlayerPositionClientRpc();
    }
    [ClientRpc]
    public void setPlayerPositionClientRpc()
    {
        if (OwnerClientId == 0)
        {
            Debug.Log("Host Position");
            // Si el jugador es el host, establece la posición inicial del jugador en startPlayer1Position
            transform.position = startPlayer1Position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }
        else if (OwnerClientId == 1)
        {
            Debug.Log("Client Position");
            // Si el jugador no es el host, establece la posición inicial del jugador en startPlayer2Position
            transform.position = startPlayer2Position;
            // Rotar el jugador para que mire hacia abajo
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
    }

    [ServerRpc]
    public void RequestUpdateHpServerRpc()
    {
        UpdateHpClientRpc();
    }
    [ClientRpc]
    public void UpdateHpClientRpc()
    {
        if (OwnerClientId == 0) 
        {
            Debug.Log("OwnerClientID:   " + OwnerClientId);
            Debug.Log("Health1  " + healthP1 + "    Healt2   " + healthP2);
            this.GetComponentInChildren<TextMesh>().text = healthP1.ToString();
        }
        else if (OwnerClientId == 1)
        { 
            Debug.Log("OwnerClientID:   " + OwnerClientId);
            Debug.Log("Health1  " + healthP1 + "    Healt2   " + healthP2);
            this.GetComponentInChildren<TextMesh>().text = healthP2.ToString();
        }

    }

    [ClientRpc]
    public void DestroyNetworkClientRPC()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
