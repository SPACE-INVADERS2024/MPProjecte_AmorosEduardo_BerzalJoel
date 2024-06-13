using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;



public class NetWorkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private TestRelay relay;
    [SerializeField] private Text textJoinCode;

    private Mlt_PlayerStateController playerStateController;
    private int numberPlayers = 0;
    private string joinCode;


    private void Awake()
    {

        relay = new TestRelay();
        playerStateController = new Mlt_PlayerStateController();


        hostBtn.onClick.AddListener(() => {
            relay.CreateRelay();
        });


        clientBtn.onClick.AddListener(() => {

            relay.JoinRelay(joinCode);
        });
    }
    private void Update()
    {
        numberPlayers = playerStateController.GetNumConnectedPlayers();

        if (numberPlayers > 0)
        {
            joinCode = relay.GetJoinCode();

            if (joinCode != null)
            {

                textJoinCode.text = joinCode;
            }
        }
        if (numberPlayers == 2)
        {
            GameObject.FindGameObjectWithTag("NManagerUI").SetActive(false);

        }
    }

    public void GetText(string text)
    {
        joinCode = text;
        Debug.Log(joinCode);
    }
}