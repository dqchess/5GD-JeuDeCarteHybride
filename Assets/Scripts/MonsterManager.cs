using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    [Header("Monster")]
    public TMP_Text ATKSummaryMonster;
    public TMP_Text DEFSummaryMonster;
    public TMP_Text monsterName;
    private int ATKMonster = 0;
    private int DEFMonster = 0;

    public GameObject[] monsters;
    public GameObject monster1;
    public GameObject monster2;

    [Header("Stuff")]
    public FightManager fightManager;


    public void InstantiateMonsters()
    {
        ResetMonsters();
        int r = Random.Range(0, monsters.Length);
        monster1.GetComponent<Monster>().model = Instantiate(monsters[r], monster1.transform);
        monster2.GetComponent<Monster>().model = Instantiate(monsters[r], monster2.transform);

        GameObject m = monster1.GetComponent<Monster>().model;
        monsterName.text = m.GetComponent<MonsterStats>().monsterName;
        int atk = m.GetComponent<MonsterStats>().monsterATK;
        int def = m.GetComponent<MonsterStats>().monsterHP;

        ATKSummaryMonster.text = atk.ToString();
        DEFSummaryMonster.text = def.ToString();

        monster1.GetComponent<Monster>().monsterATK = atk;
        monster1.GetComponent<Monster>().monsterHP = def;
        monster2.GetComponent<Monster>().monsterATK = atk;
        monster2.GetComponent<Monster>().monsterHP = def;  
    }

    public void ResetMonsters()
    {
        if (monster1.GetComponent<Monster>().model != null)
        {
            Destroy(monster1.GetComponent<Monster>().model);            
        }
        if (monster2.GetComponent<Monster>().model != null)
        {
            Destroy(monster2.GetComponent<Monster>().model);
        }
        monster1.GetComponent<Monster>().monsterATK = 0;
        monster1.GetComponent<Monster>().monsterHP = 0;
        monster2.GetComponent<Monster>().monsterATK = 0;
        monster2.GetComponent<Monster>().monsterHP = 0;
    }


    public void StartFight()
    {
        monster1.GetComponent<Monster>().StartFight();
        monster2.GetComponent<Monster>().StartFight();
        fightManager.monster1 = monster1;
        fightManager.monster2 = monster2;
        fightManager.StartFight();
    }
}
