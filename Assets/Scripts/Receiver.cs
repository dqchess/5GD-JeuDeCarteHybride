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

    public bool playerOneReady = false;
    public bool playerTwoReady = false;

    private void Start()
    {
        ReceiverOSC.Bind("/player", ReceiveDebug);

        ReceiverOSC.Bind("/player1/cards", ReceivePlayer1CardsInfos);
        ReceiverOSC.Bind("/player2/cards", ReceivePlayer2CardsInfos);

        //ReceiverOSC.Bind("/player1/name", ReceivePlayer1NameInfos);
        //ReceiverOSC.Bind("/player2/name", ReceivePlayer2NameInfos);

    }

    public void Update()
    {
        if (playerOneReady == true && playerTwoReady == true)
        {
            GameManager.Instance.Fight();
            playerOneReady = false;
            playerTwoReady = false;
            SoundManager.instance.EndOfNegociation();
        }
    }

    public void ReceiveDebug(OSCMessage message)
    {
        print("Debug : GoodIP");
    }

    public void ReceivePlayer1CardsInfos(OSCMessage message)
    {
        Debug.Log("Player One Scan : " + message.Values[0].StringValue);


        string id = message.Values[0].StringValue;
        CardsInformations c = ExcelManager.Instance.GetInfosOfTheCard(id, 1);
        if (c != null)
        {
            if (id == "RDY")
            {
                //Met Le joueur pret/non pret
                playerOneReady = !playerOneReady;
                SoundManager.instance.ValidationStuff();
                return;
            }

            if (c.name.Contains("aventurer") == true)
            {
                //Recuperer les infos de l'aventurier et mettre le joueur 1 en PRET
                GameManager.Instance.player1.ScanAdventurer(c.id);
                print("Adventurer Scanned");
                return;
            }
            else
            {
                if (ExcelManager.Instance.IsMyCardScannedPlayerOne(id) == true)
                {
                    GameManager.Instance.player1.RemoveEquipment(c);
                    Debug.Log("Player One Scan Remove : " + message.Values[0].StringValue);
                }
                else
                {
                    GameManager.Instance.player1.AddEquipment(c);
                    Debug.Log("Player One Scan Add : " + message.Values[0].StringValue);
                }
            }
        }
    }

    public void ReceivePlayer2CardsInfos(OSCMessage message)
    {
        Debug.Log("Player Two Scan : " + message.Values[0].StringValue);


        string id = message.Values[0].StringValue;
        CardsInformations c = ExcelManager.Instance.GetInfosOfTheCard(id, 2);
        if (c != null)
        {
            if (id == "RDY")
            {
                //Met Le joueur pret/non pret
                SoundManager.instance.ValidationStuff();
                playerTwoReady = !playerTwoReady;
                return;
            }



            if (c.name.Contains("aventurer") == true)
            {
                //Recuperer les infos de l'aventurier et mettre le joueur 2 en PRET
                GameManager.Instance.player2.ScanAdventurer(c.id);
                print("Adventurer Scanned");

            }
            else
            {
                if (ExcelManager.Instance.IsMyCardScannedPlayerTwo(id) == true)
                {
                    GameManager.Instance.player2.RemoveEquipment(c);
                    Debug.Log("Player Two Scan Remove : " + message.Values[0].StringValue);
                }
                else
                {
                    GameManager.Instance.player2.AddEquipment(c);
                    Debug.Log("Player Two Scan Add : " + message.Values[0].StringValue);
                }
            }

        }
    }

    /*public void ReceivePlayer1NameInfos(OSCMessage message)
    {
        string nameOfThePlayer = message.Values[0].StringValue;
        print("Name Of Player One : " + nameOfThePlayer);
    }

    public void ReceivePlayer2NameInfos(OSCMessage message)
    {
        string nameOfThePlayer = message.Values[0].StringValue;
        print("Name Of Player Two : " + nameOfThePlayer);
    }*/
}
