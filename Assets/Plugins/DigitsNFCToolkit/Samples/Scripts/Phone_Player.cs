using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone_Player : MonoBehaviour
{
    public Toggle toggleJoueur1;
    public Toggle toggleJoueur2;
    public int player = 1;
    float timer;
    bool waitOneFrame = false;
    public InputField userNameInputField;
    public InputField iPInputField;
    public Button validationButton;
    public extOSC.OSCTransmitter oscManager;
    public Text errorLogText;

    public DigitsNFCToolkit.Samples.Sender sender;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*if (Input.GetKeyDown(KeyCode.A))
        {
            print("1");
            oscManager.RemoteHost = "192.168.43.170";
            print("2");
        }*/
    }

    private void LateUpdate()
    {
        waitOneFrame = false;
    }

    public void ValidateAllStats()
    {
        if (userNameInputField.text == "" || iPInputField.text == "")
        {
            errorLogText.text += " Error 01 : TextEmpty";
            return;
        }

        oscManager.RemoteHost = iPInputField.text;

        sender.SendPlayerName(userNameInputField.text);

    }

    public void ChangePlayer(Toggle otherToggle)
    {
        if (waitOneFrame == false)
        {
            if (player == 1)
                player = 2;
            else
                player = 1;

            waitOneFrame = true;
            otherToggle.isOn = !otherToggle.isOn;            
        }
    }
}
