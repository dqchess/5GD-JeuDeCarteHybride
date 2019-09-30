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
        ReceiverOSC.Bind("/oscdude", ReceiveOSCDude);
    }

    public void ReceiveOSCDude(OSCMessage message)
    {
        Debug.Log("message : " + message.ToString());
    }
}
