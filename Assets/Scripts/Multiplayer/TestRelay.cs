using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class TestRelay : MonoBehaviour
{
    string joincode;
    async void Start()
    {
        InitializationOptions hostOptions = new InitializationOptions().SetProfile("host");
        InitializationOptions clientOptions = new InitializationOptions().SetProfile("client");

        await UnityServices.InitializeAsync(hostOptions);

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
        };

        if (AuthenticationService.Instance.IsAuthorized)
        {
            Debug.Log("Authorized");
            AuthenticationService.Instance.SignOut();
            await UnityServices.InitializeAsync(clientOptions);
        }
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            joincode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            
            Debug.Log(joincode);

            RelayServerData relayServerData = new RelayServerData(allocation,"dtls");
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public async void JoinRelay(string joincode)
    {
        try
        {
            Debug.Log("Join Relay with joincode " + joincode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joincode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch(RelayServiceException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public string GetJoinCode()
    {
        return joincode;
    }
}
 