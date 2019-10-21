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
        ReceiverOSC.Bind("/player", ReceiveDebug);

        ReceiverOSC.Bind("/player1/cards", ReceivePlayer1CardsInfos);
        ReceiverOSC.Bind("/player2/cards", ReceivePlayer2CardsInfos);

        ReceiverOSC.Bind("/player1/name", ReceivePlayer1NameInfos);
        ReceiverOSC.Bind("/player2/name", ReceivePlayer2NameInfos);

    }

    public void ReceiveDebug(OSCMessage message)
    {
        print("Debug : GoodIP");
    }

    public void ReceivePlayer1CardsInfos(OSCMessage message)
    {
        Debug.Log("Player One Scan : " + message.Values[0].StringValue);
        string id = message.Values[0].StringValue;
        CardsInformations c = ExcelManager.Instance.GetInfosOfTheCard(id);
        GameManager.Instance.player1.AddStatsPlayer(int.Parse(c.damage), int.Parse(c.armor));
    }

    public void ReceivePlayer2CardsInfos(OSCMessage message)
    {
        string id = message.Values[0].StringValue;
        CardsInformations c = ExcelManager.Instance.GetInfosOfTheCard(id);
        GameManager.Instance.player2.AddStatsPlayer(int.Parse(c.damage), int.Parse(c.armor));
        Debug.Log("Player Two Scan : " + message.Values[0].StringValue);
    }

    public void ReceivePlayer1NameInfos(OSCMessage message)
    {
        string nameOfThePlayer = message.Values[0].StringValue;
        print("Name Of Player One : " + nameOfThePlayer);
    }

    public void ReceivePlayer2NameInfos(OSCMessage message)
    {
        string nameOfThePlayer = message.Values[0].StringValue;
        print("Name Of Player Two : " + nameOfThePlayer);
    }
}
