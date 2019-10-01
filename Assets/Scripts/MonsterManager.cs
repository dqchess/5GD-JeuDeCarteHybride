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
    public TMP_Text monster1Name;
    public TMP_Text monster2Name;

    public GameObject[] monsters;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monsterPreview;

    private int randomMonster;
    private int atk;
    private int hp;

    [Header("Stuff")]
    public FightManager fightManager;


    public void InstantiateMonsterStats()
    {
        ResetMonsters();
        randomMonster = Random.Range(0, monsters.Length);
        GameObject m = Instantiate(monsters[randomMonster], monsterPreview.transform);

        monsterName.text = m.GetComponent<MonsterStats>().monsterName;
        monster1Name.text = m.GetComponent<MonsterStats>().monsterName;
        monster2Name.text = m.GetComponent<MonsterStats>().monsterName;

        atk = m.GetComponent<MonsterStats>().monsterATK;
        hp = m.GetComponent<MonsterStats>().monsterHP;
        ATKSummaryMonster.text = atk.ToString();
        DEFSummaryMonster.text = hp.ToString();
    }

    public void InstantiateMonsters()
    {
        ResetMonsters();
        Destroy(monsterPreview.transform.GetChild(0).gameObject);
        monster1.GetComponent<Monster>().model = Instantiate(monsters[randomMonster], monster1.transform);
        monster2.GetComponent<Monster>().model = Instantiate(monsters[randomMonster], monster2.transform);

        monster1.GetComponent<Monster>().monsterATK = atk;
        monster1.GetComponent<Monster>().monsterHP = hp;
        monster2.GetComponent<Monster>().monsterATK = atk;
        monster2.GetComponent<Monster>().monsterHP = hp;
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
        InstantiateMonsters();
        monster1.GetComponent<Monster>().StartFight();
        monster2.GetComponent<Monster>().StartFight();
        fightManager.monster1 = monster1;
        fightManager.monster2 = monster2;
        fightManager.StartFight();
    }
}
