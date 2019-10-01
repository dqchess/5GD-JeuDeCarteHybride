using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public TMP_Text ATKSummaryMonsterFight;
    public TMP_Text HPSummaryMonsterFight;
    public int monsterATK = 0;
    public int monsterHP = 0;

    [HideInInspector] public GameObject model;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void StartFight()
    {
        UpdateStatsUIMonster();
    }

    private void UpdateStatsUIMonster()
    {
        ATKSummaryMonsterFight.text = monsterATK.ToString();
        HPSummaryMonsterFight.text = monsterHP.ToString();
    }

    public void TakeDamageMonster(int damage)
    {
        monsterHP -= damage;
        UpdateStatsUIMonster();
    }

    public void Die()
    {
        //particle 
        Destroy(model);
    }
}
