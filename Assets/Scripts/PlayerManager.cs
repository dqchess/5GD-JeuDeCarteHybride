using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player 1")]
    public TMP_InputField ATKInputPlayer1;
    public TMP_InputField DEFInputPlayer1;
    public TMP_Text ATKSummaryPlayer1;
    public TMP_Text DEFSummaryPlayer1;
    private float ATKPlayer1 = 0f;
    private float DEFPlayer1 = 0f;

    [Header("Player 2")]
    public TMP_InputField ATKInputPlayer2;
    public TMP_InputField DEFInputPlayer2;
    public TMP_Text ATKSummaryPlayer2;
    public TMP_Text DEFSummaryPlayer2;
    private float ATKPlayer2 = 0f;
    private float DEFPlayer2 = 0f;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public void AddStatsPlayer1()
    {
        bool b = false;
        int tmpInt;
        b = int.TryParse(ATKInputPlayer1.text, out tmpInt);
        if (b)
        {
            ATKPlayer1 += tmpInt;
            ATKSummaryPlayer1.text = ATKPlayer1.ToString();
            ATKInputPlayer1.text = "";
        }
        b = false;
        b = int.TryParse(DEFInputPlayer1.text, out tmpInt);
        if (b)
        {
            DEFPlayer1 += tmpInt;
            DEFSummaryPlayer1.text = DEFPlayer1.ToString();
            DEFInputPlayer1.text = "";
        }
    }

    public void AddStatsPlayer1(int atk, int def)
    {
        ATKPlayer1 += atk;
        ATKSummaryPlayer1.text = ATKPlayer1.ToString();

        DEFPlayer1 += def;
        DEFSummaryPlayer1.text = DEFPlayer1.ToString();
    }

    public void AddStatsPlayer2()
    {
        bool b = false;
        int tmpInt;
        b = int.TryParse(ATKInputPlayer2.text, out tmpInt);
        if (b)
        {
            ATKPlayer2 += tmpInt;
            ATKSummaryPlayer2.text = ATKPlayer2.ToString();
            ATKInputPlayer2.text = "";
        }
        b = false;
        b = int.TryParse(DEFInputPlayer2.text, out tmpInt);
        if (b)
        {
            DEFPlayer2 += tmpInt;
            DEFSummaryPlayer2.text = DEFPlayer2.ToString();
            DEFInputPlayer2.text = "";
        }
    }

    public void AddStatsPlayer2(int atk, int def)
    {
        ATKPlayer2 += atk;
        ATKSummaryPlayer2.text = ATKPlayer2.ToString();

        DEFPlayer2 += def;
        DEFSummaryPlayer2.text = DEFPlayer2.ToString();
    }
}
