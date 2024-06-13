using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class Mlt_PlayerStateController : NetworkBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 startPlayer1Position = new Vector3(0, -4);
    private Vector3 startPlayer2Position = new Vector3(0, 4);


    public static float health = 3f;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        RequestSetPlayerPositionServerRpc();

        Debug.Log("Id: " + OwnerClientId);
        Debug.Log("Health: " + health);

        GetNumConnectedPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Health: " + health);
        health -= 1;
        if (health == 0)
        {
            Debug.Log("Dead");
        }
    }

    public int GetNumConnectedPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Plyr");
        return players.Length;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestSetPlayerPositionServerRpc(){
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

}
