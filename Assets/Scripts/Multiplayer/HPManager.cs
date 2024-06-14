using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class HPManager : NetworkBehaviour
{
    [SerializeField] private TextMesh hpTxt;


    // Update is called once per frame
 
    public void UpdateHpPlayer(float hp)
    {
        hpTxt.text = hp.ToString();
    }
}
