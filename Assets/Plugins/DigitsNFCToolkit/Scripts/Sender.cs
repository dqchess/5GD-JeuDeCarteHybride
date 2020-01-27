using extOSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitsNFCToolkit.Samples
{
    public class Sender : MonoBehaviour
    {
        [Header("OSC Settings")]
        public OSCTransmitter Transmitter;
        public Text iDReceiveText;
        public Phone_Player phonePlayer;
        //DigitsNFCToolkit.TextRecord textRecord;

        private void Start()
        {

        }

        private void Update()
        {
            SendDebug("YOLO");
        }
        public void SendDebug(string name)
        {
            Send("/player", OSCValue.String(name));
        }

        public void SendValue(string textToSend)
        {

            if (phonePlayer.player == 1)
            {
                iDReceiveText.text = textToSend;
                Send("/player1/cards", OSCValue.String(textToSend));
            }
            else
            {
                iDReceiveText.text = textToSend;
                Send("/player2/cards", OSCValue.String(textToSend));
            }

        }

        public void SendPlayerName(string nameToSend)
        {
            if (phonePlayer.player == 1)
            {
                Send("/player1/name", OSCValue.String(nameToSend));
            }
            else
            {
                Send("/player2/name", OSCValue.String(nameToSend));
            }
        }

        private void Send(string address, OSCValue value)
        {
            if (Transmitter != null)
            {
                Transmitter.Send(address, value);
            }
        }
    }
}

