using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone_Player : MonoBehaviour
{
    public Toggle toggleJoueur1;
    public Toggle toggleJoueur2;
    public GameObject canvasSettings;
    public int player = 1;
    float timer;
    bool waitOneFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void LateUpdate()
    {
        waitOneFrame = false;
    }

    public void OpenSettings()
    {
        if (canvasSettings.activeSelf == true)
        {
            canvasSettings.SetActive(false);
        }
        else
        {
            canvasSettings.SetActive(true);
        }
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
