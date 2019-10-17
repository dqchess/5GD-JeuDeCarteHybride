using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    public Image atkElementImage;
    public Image defElementImage;

    [Header("Stuff")]
    public GameObject[] monsters;
    public GameObject monsterPreview;

    [Header("Monster Values")]
    public int minAtk;
    public int maxAtk;
    public int def;
    public int loot;
    public float honor;
    public Element elementAtk;
    public Element elementDef;
    
    public FightManager fightManager;

    public void InstantiateMonster()
    {
        int randomMonster = Random.Range(0, monsters.Length);
        GameObject monster = monsterPreview.GetComponent<MonsterPreview>().model = Instantiate(monsters[randomMonster], monsterPreview.transform);

        DOTween.To(() => monster.transform.localScale, x => monster.transform.localScale = x, Vector3.zero, 0.5f).From();

        SetMonsterElements();
        SetMonsterStats(monster.GetComponent<MonsterStats>());
    }

    public void SetMonsterStats(MonsterStats monster)
    {
        if (monster == null)
            return;

        textMonsterName.text = monster.GetComponent<MonsterStats>().monsterName;

        minAtk = monster.monsterMinATK;
        maxAtk = monster.monsterMaxATK;
        def = monster.monsterHP;
        loot = monster.monsterLoot;
        honor = monster.monsterHonor;

        if (elementAtk != Element.NULL)
        {
            minAtk *= 2; maxAtk *= 2;
        }            
        if (elementDef != Element.NULL)
        {
            def *= 2;
        }
            
        textMinAtkMonsterStats.text = minAtk.ToString();
        textMaxAtkMonsterStats.text = maxAtk.ToString();
        textHpMonsterStats.text = def.ToString();
        textLootMonsterStats.text = loot.ToString();        
        textHonorMonsterStats.text = honor.ToString();
    }

    public void SetMonsterElements()
    {
        int randomElement;
        //ATK Random
        if (Random.Range(0, GameManager.Instance.randomElementMonster) == 2)
        {
            randomElement = Random.Range(0, 3);
            atkElementImage.color = new Color(255, 255, 255, 255);//alpha 1
            switch (randomElement)
            {
                case 0:
                    elementAtk = Element.FIRE;
                    atkElementImage.sprite = GameManager.Instance.fireSprite;
                    break;
                case 1:
                    elementAtk = Element.ICE;
                    atkElementImage.sprite = GameManager.Instance.iceSprite;
                    break;
                case 2:
                    elementAtk = Element.ELECTRIC;
                    atkElementImage.sprite = GameManager.Instance.electricSprite;
                    break;
            }
        }
        else
        {
            elementAtk = Element.NULL;
            atkElementImage.sprite = null;
            atkElementImage.color = new Color(255, 255, 255, 0);//alpha 0
        }
        //DEF Random
        if (Random.Range(0, GameManager.Instance.randomElementMonster) == 2)
        {
            randomElement = Random.Range(0, 3);
            defElementImage.color = new Color(255, 255, 255, 255);//alpha 1
            switch (randomElement)
            {
                case 0:
                    elementDef = Element.FIRE;
                    defElementImage.sprite = GameManager.Instance.fireSprite;
                    break;
                case 1:
                    elementDef = Element.ICE;
                    defElementImage.sprite = GameManager.Instance.iceSprite;
                    break;
                case 2:
                    elementDef = Element.ELECTRIC;
                    defElementImage.sprite = GameManager.Instance.electricSprite;
                    break;
            }
        }
        else
        {
            elementDef = Element.NULL;
            defElementImage.sprite = null;
            defElementImage.color = new Color(255, 255, 255, 0);//alpha 0
        }    
    }
}
