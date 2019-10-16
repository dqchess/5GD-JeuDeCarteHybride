using DG.Tweening;
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
    public TMP_Text textLootMonsterStats;
    public TMP_Text textHonorMonsterStats;
    public TMP_Text textMonsterName;

    [Header("Monster Fight")]
    public TMP_Text textMonster1Name;
    public TMP_Text textMonster2Name;

    [Header("Stuff")]
    public GameObject[] monsters;
    public GameObject monsterPreview;

    private int randomMonster;
    [HideInInspector] public int minAtk;
    [HideInInspector] public int maxAtk;
    [HideInInspector] public int hp;
    [HideInInspector] public int loot;
    [HideInInspector] public float honor;

    public FightManager fightManager;

    public void InstantiateMonsterStats()
    {
        monsterPreview.SetActive(true);
        monsterPreview.transform.localScale = Vector3.one;
        randomMonster = Random.Range(0, monsters.Length);
        monsterPreview.GetComponent<MonsterPreview>().model = Instantiate(monsters[randomMonster], monsterPreview.transform);

        GameObject m = monsterPreview.GetComponent<MonsterPreview>().model;
        DOTween.To(() => m.transform.localScale, x => m.transform.localScale = x, Vector3.zero, 0.5f).From();

        textMonsterName.text = m.GetComponent<MonsterStats>().monsterName;

        minAtk = m.GetComponent<MonsterStats>().monsterMinATK;
        maxAtk = m.GetComponent<MonsterStats>().monsterMaxATK;
        hp = m.GetComponent<MonsterStats>().monsterHP;
        loot = m.GetComponent<MonsterStats>().monsterLoot;
        honor = m.GetComponent<MonsterStats>().monsterHonor;
        textMinAtkMonsterStats.text = minAtk.ToString();
        textMaxAtkMonsterStats.text = maxAtk.ToString();
        textHpMonsterStats.text = hp.ToString();
        textLootMonsterStats.text = loot.ToString();        
        textHonorMonsterStats.text = honor.ToString();        
    }

    public void StartFight()
    {
        fightManager.StartFight();
    }
}
