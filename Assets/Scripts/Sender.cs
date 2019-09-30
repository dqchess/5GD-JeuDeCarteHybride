using extOSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    [Header("OSC Settings")]
    public OSCTransmitter Transmitter;

    public void SendValue()
    {
        Send("/oscdude", OSCValue.String("FAK NOOB SHIT BITCH"));
    }

    private void Send(string address, OSCValue value)
    {
        if (Transmitter != null)
            Transmitter.Send(address, value);
    }
}
