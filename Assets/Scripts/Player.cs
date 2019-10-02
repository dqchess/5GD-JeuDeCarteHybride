using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public TMP_InputField ATKInputPlayer;
    public TMP_InputField DEFInputPlayer;
    public TMP_Text textHpPlayerStats;
    public TMP_Text textAtkPlayerStats;
    public TMP_Text textDefPlayerStats;

    [Header("Fight")]
    public TMP_Text textHpPlayerFight;
    public TMP_Text textAtkPlayerFight;
    public TMP_Text textDefPlayerFight;

    [Header("Other")]
    public GameObject model;

    [HideInInspector] public int playerATK = 0;
    [HideInInspector] public int playerDEF = 0;
    [HideInInspector] public int playerHP = 20;

    private void Start()
    {
        UpdateStatsUIPlayer();
    }

    public void AddStatsPlayer()
    {
        bool b = false;
        int tmpInt;
        b = int.TryParse(ATKInputPlayer.text, out tmpInt);
        if (b)
        {
            playerATK += tmpInt;           
            ATKInputPlayer.text = "";
        }
        b = false;
        b = int.TryParse(DEFInputPlayer.text, out tmpInt);
        if (b)
        {
            playerDEF += tmpInt;            
            DEFInputPlayer.text = "";
        }
        UpdateStatsUIPlayer();
    }

    public void AddStatsPlayer(int atk, int def)
    {
        playerATK += atk;
        textAtkPlayerStats.text = playerATK.ToString();

        playerDEF += def;
        textDefPlayerStats.text = playerDEF.ToString();
    }

    public void StartFight()
    {
        //UpdateStatsUIPlayer();
    }

    public void Die()
    {
        Destroy(model);
    }

    public void UpdateStatsUIPlayer()
    {
        //STATS
        textAtkPlayerStats.text = playerATK.ToString();
        textDefPlayerStats.text = playerDEF.ToString();
        textHpPlayerStats.text = playerHP.ToString();

        //FIGHT
        textAtkPlayerFight.text = playerATK.ToString();
        textDefPlayerFight.text = playerDEF.ToString();
        textHpPlayerFight.text = playerHP.ToString();
    }

    public void TakeDamagePlayer(int damage)
    {
        Debug.Log(damage);
        if (damage <= playerDEF)
        {
            playerDEF -= damage;
        }
        else if (damage > playerDEF)
        {
            int damageTmp = damage - playerDEF;
            playerDEF = 0;
            playerHP -= damageTmp;
        }
        UpdateStatsUIPlayer();
    }

    public void ResetStats()
    {
        playerATK = 0;
        playerDEF = 0;
        UpdateStatsUIPlayer();
    }
}
