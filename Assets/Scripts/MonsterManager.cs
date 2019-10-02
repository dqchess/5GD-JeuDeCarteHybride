using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : MonoBehaviour
{
    [Header("Monster Stats")]
    public TMP_Text textMinAtkMonsterStats;
    public TMP_Text textMaxAtkMonsterStats;
    public TMP_Text textHpMonsterStats;
    public TMP_Text textMonsterName;

    [Header("Monster Fight")]
    public TMP_Text textMonster1Name;
    public TMP_Text textMonster2Name;

    [Header("Stuff")]
    public GameObject[] monsters;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monsterPreview;

    private int randomMonster;
    private int minAtk;
    private int maxAtk;
    private int hp;

    public FightManager fightManager;


    public void InstantiateMonsterStats()
    {
        ResetMonsters();
        randomMonster = Random.Range(0, monsters.Length);
        GameObject m = Instantiate(monsters[randomMonster], monsterPreview.transform);

        textMonsterName.text = m.GetComponent<MonsterStats>().monsterName;
        textMonster1Name.text = m.GetComponent<MonsterStats>().monsterName;
        textMonster2Name.text = m.GetComponent<MonsterStats>().monsterName;

        minAtk = m.GetComponent<MonsterStats>().monsterMinATK;
        maxAtk = m.GetComponent<MonsterStats>().monsterMaxATK;
        hp = m.GetComponent<MonsterStats>().monsterHP;
        textMinAtkMonsterStats.text = minAtk.ToString();
        textMaxAtkMonsterStats.text = maxAtk.ToString();
        textHpMonsterStats.text = hp.ToString();
    }

    public void InstantiateMonsters()
    {
        ResetMonsters();
        Destroy(monsterPreview.transform.GetChild(0).gameObject);
        monster1.GetComponent<Monster>().model = Instantiate(monsters[randomMonster], monster1.transform);
        monster2.GetComponent<Monster>().model = Instantiate(monsters[randomMonster], monster2.transform);

        monster1.GetComponent<Monster>().monsterMinATK = minAtk;
        monster1.GetComponent<Monster>().monsterMaxATK = maxAtk;
        monster1.GetComponent<Monster>().monsterHP = hp;

        monster2.GetComponent<Monster>().monsterMinATK = minAtk;
        monster2.GetComponent<Monster>().monsterMaxATK = maxAtk;
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
        monster1.GetComponent<Monster>().monsterMinATK = 0;
        monster1.GetComponent<Monster>().monsterMaxATK = 0;
        monster1.GetComponent<Monster>().monsterHP = 0;
        monster2.GetComponent<Monster>().monsterMinATK = 0;
        monster2.GetComponent<Monster>().monsterMaxATK = 0;
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
