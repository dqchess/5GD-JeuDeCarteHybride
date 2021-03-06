﻿using DG.Tweening;
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
    public TMP_Text textAtkMonsterStats;
    public TMP_Text textDefMonsterStat;
    public TMP_Text textLootMonsterStats;
    public TMP_Text textHonorMonsterStats;
    public TMP_Text textMonsterName;
    public Image atkElementImage;
    public Image defElementImage;

    [Header("Stuff")]
    public GameObject[] monsters;
    public bool randomizeMonsters = false;
    public GameObject monsterPreview;
    public GameObject atkUI;
    public GameObject defUI;

    [Header("Monster Values")]
    public int minAtk;
    public int maxAtk;
    public int atk;
    public int def;
    public int loot;
    public float honor;
    public Element elementAtk;
    public Element elementDef;

    private int randomAtk = 0;
    private int monsterId = 0;

    public MonsterStats monsterStats;

    public void InstantiateMonster()
    {
        SoundManager.instance.MonsterSpawn();
        

        int monsterSelected = monsterId++;

        if (randomizeMonsters)
            monsterSelected = Random.Range(0, monsters.Length);

        randomAtk = 0;
        monsterPreview.GetComponent<MonsterPreview>().model = Instantiate(monsters[monsterSelected], monsterPreview.transform);
        GameObject model = monsterPreview.GetComponent<MonsterPreview>().model;

        DOTween.To(() => model.transform.localScale, x => model.transform.localScale = x, Vector3.zero, 0.5f).From();

        monsterStats = model.GetComponent<MonsterStats>();

        //SetMonsterElements(monsterStats);

        SetMonsterStats(monsterStats);
    }

    public void DestroyMonster()
    {
        monsterPreview.GetComponent<MonsterPreview>().model.transform.DOScale(Vector3.zero, 1.5f).SetEase(Ease.InSine).OnComplete(() => {
            Destroy(monsterPreview.GetComponent<MonsterPreview>().model);
            SoundManager.instance.MonsterScream();
            SoundManager.instance.StopIdleMonster();
        });       
    }

    public void SetMonsterElements(MonsterStats monsterStats)
    {
        int randomElement;
        //atkElementImage.color = new Color(255, 255, 255, 0);//alpha 0
        
        
        //ATK Random
        
        if (monsterStats.forceAttackElement == Element.NULL) //random element
        {
            if (Random.Range(0, GameManager.Instance.randomElementMonster) == 0)
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
                atkElementImage.color = new Color(255, 255, 255, 0);//alpha 0
            }
        }
        
        else if (monsterStats.forceAttackElement != Element.NONE) // defined element
        {
            atkElementImage.color = new Color(255, 255, 255, 255);//alpha 1
            switch (monsterStats.forceAttackElement)
            {
                case Element.FIRE:
                    elementAtk = Element.FIRE;
                    atkElementImage.sprite = GameManager.Instance.fireSprite;
                    break;
                case Element.ICE:
                    elementAtk = Element.ICE;
                    atkElementImage.sprite = GameManager.Instance.iceSprite;
                    break;
                case Element.ELECTRIC:
                    elementAtk = Element.ELECTRIC;
                    atkElementImage.sprite = GameManager.Instance.electricSprite;
                    break;
            }
        }
        
        else // no element
        {
            elementAtk = Element.NULL;
            atkElementImage.sprite = null;
            atkElementImage.color = new Color(255, 255, 255, 0);//alpha 0
        }
        
        
      
        
        //DEF Random
        
        //defElementImage.color = new Color(255, 255, 255, 0);//alpha 0
        if (monsterStats.forceDefenseElement == Element.NULL)
        {
            if (Random.Range(0, GameManager.Instance.randomElementMonster) == 0)
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
                defElementImage.color = new Color(255, 255, 255, 0);//alpha 0
            }
        }
        
        else if (monsterStats.forceDefenseElement != Element.NONE)
        {
            defElementImage.color = new Color(255, 255, 255, 255);//alpha 1
            switch (monsterStats.forceDefenseElement)
            {
                case Element.FIRE:
                    elementDef = Element.FIRE;
                    defElementImage.sprite = GameManager.Instance.fireSprite;
                    break;
                case Element.ICE:
                    elementDef = Element.ICE;
                    defElementImage.sprite = GameManager.Instance.iceSprite;
                    break;
                case Element.ELECTRIC:
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

    public void SetMonsterStats(MonsterStats monster)
    {
        SetMonsterElements(monster);
        
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

        if (randomAtk == 0)            
        {
            randomAtk = atk = Random.Range(minAtk, maxAtk + 1);
            textMinAtkMonsterStats.gameObject.SetActive(true);
            textMaxAtkMonsterStats.gameObject.SetActive(true);
            textAtkMonsterStats.gameObject.SetActive(false);
        }

        UpdateUIMonster();
    }

    public IEnumerator DisplayRandomAtk()
    {
        Vector3 rectMin = textMinAtkMonsterStats.GetComponent<RectTransform>().position;
        Vector3 rectMax = textMaxAtkMonsterStats.GetComponent<RectTransform>().position;

        textMinAtkMonsterStats.GetComponent<RectTransform>().DOShakeAnchorPos(1, 15,100);
        textMaxAtkMonsterStats.GetComponent<RectTransform>().DOShakeAnchorPos(1, 15, 100);
        yield return new WaitForSeconds(0.7f);
        textMinAtkMonsterStats.GetComponent<RectTransform>().DOMove(textAtkMonsterStats.transform.position, 0.3f).SetEase(Ease.InBack);
        textMaxAtkMonsterStats.GetComponent<RectTransform>().DOMove(textAtkMonsterStats.transform.position, 0.3f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.3f);
        textMinAtkMonsterStats.gameObject.SetActive(false);
        textMaxAtkMonsterStats.gameObject.SetActive(false);
        textMinAtkMonsterStats.gameObject.GetComponent<RectTransform>().position = rectMin;
        textMaxAtkMonsterStats.gameObject.GetComponent<RectTransform>().position = rectMax;

        textAtkMonsterStats.gameObject.SetActive(true);

        textAtkMonsterStats.GetComponent<RectTransform>().DOScale(textAtkMonsterStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2,LoopType.Yoyo);
        yield break;
    }

    public void UpdateUIMonster()
    {
        textMinAtkMonsterStats.text = "[" + minAtk.ToString() + " - " + maxAtk.ToString() + "]";
        //textMaxAtkMonsterStats.text = maxAtk.ToString();
        textAtkMonsterStats.text = atk.ToString();
        textDefMonsterStat.text = def.ToString();
        textLootMonsterStats.text = loot.ToString();
        textHonorMonsterStats.text = honor.ToString();
    }
}
