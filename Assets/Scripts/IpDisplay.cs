using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IpDisplay : MonoBehaviour
{
    public TMP_Text text;
    public extOSC.OSCReceiver osc;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(DisplayIp());
        }  
    }

    private IEnumerator DisplayIp()
    {
        text.text = osc.ToString();
        yield return new WaitForSeconds(5f);
        text.text = "";
    }
}
