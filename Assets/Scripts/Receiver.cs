using extOSC;
using extOSC.Core;
using extOSC.Core.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    [Header("OSC Settings")]
    public OSCReceiver ReceiverOSC;

    private void Start()
    {
        ReceiverOSC.Bind("/player1", ReceivePlayer1);
        ReceiverOSC.Bind("/player2", ReceivePlayer2);
    }

    public void ReceivePlayer1(OSCMessage message)
    {
        //Debug.Log("message : " + message.ToString());
        CardsInformations c = ExcelManager.Instance.GetInfosOfTheCard(message.ToString());
        GameManager.Instance.player1.AddStatsPlayer(int.Parse(c.damage), int.Parse(c.armor));
    }

    public void ReceivePlayer2(OSCMessage message)
    {
        Debug.Log("message : " + message.ToString());
    }
}
