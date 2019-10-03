using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public TMP_Text textMinAtkMonsterFight;
    public TMP_Text textMaxAtkMonsterFight;
    
    public TMP_Text textHpMonsterFight;
    public int monsterMinATK = 0;
    public int monsterMaxATK = 0;
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
        textMinAtkMonsterFight.text = monsterMinATK.ToString();
        textMaxAtkMonsterFight.text = monsterMaxATK.ToString();
        textHpMonsterFight.text = monsterHP.ToString();
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
