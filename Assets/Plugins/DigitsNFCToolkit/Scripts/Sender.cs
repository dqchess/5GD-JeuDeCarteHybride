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
        DigitsNFCToolkit.TextRecord textRecord;

        private void Start()
        {
            NativeNFCManager.Enable();
            //NativeNFCManager.AddNDEFReadFinishedListener();
        }

        private void Update()
        {
            //NativeNFCManager.AddNDEFPushFinishedListener(OnNDEFPushFinished)
        }
        public void SendValue(string textToSend)
        {
            iDReceiveText.text = textToSend;
            Send("/joueur1", OSCValue.String(textToSend));
        }

        private void Send(string address, OSCValue value)
        {
            if (Transmitter != null)
                Transmitter.Send(address, value);
        }
    }
}

