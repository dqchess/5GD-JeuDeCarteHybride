using DG.Tweening;
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
    public TMP_Text textDmgReceivePlayerStats;
    public GameObject grid;

    [Header("Fight")]
    public TMP_Text textHpPlayerFight;
    public TMP_Text textAtkPlayerFight;
    public TMP_Text textDefPlayerFight;

    [Header("Other")]
    public GameObject model;

    [HideInInspector] public int playerATK = 1;
    [HideInInspector] public int playerDEF = 0;
    [HideInInspector] public int playerHP = 20;

    private Dictionary<string, GameObject> gridDictionnary = new Dictionary<string, GameObject>();

    private void Start()
    {
        ResetGrid();
        ResetStats();
        UpdateStatsUIPlayer();        
    }


    private void Update()
    {
        
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
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            playerATK += atk;
            playerDEF += def;

            UpdateStatsDmgReceivePlayer();
            UpdateStatsUIPlayer();
        }       
    }

    public void AddStatsPlayer(CardsInformations c)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            if (gridDictionnary.Count == 0)
            {
                playerATK = int.Parse(c.damage);
            }   
            else
            {
                playerATK += int.Parse(c.damage);
            }
            playerDEF += int.Parse(c.armor);

            GameObject card;

            if (c.damage != "0" && c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardMixtPrefab, grid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.GetComponent<Stuff>().textValueDef.text = c.armor;

                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);

                gridDictionnary.Add(c.id, card);
            }
            else if (c.damage != "0")
            {
                card = Instantiate(GameManager.Instance.cardAtkPrefab, grid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
                gridDictionnary.Add(c.id, card);
            }
            else if (c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardDefPrefab, grid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
                gridDictionnary.Add(c.id, card);
            }

            
            UpdateStatsDmgReceivePlayer();
            UpdateStatsUIPlayer();
        }
    }

    public void RemoveStatsPlayer(CardsInformations c)
    {
        GameObject g;
        bool b = gridDictionnary.TryGetValue(c.id, out g);
        if (b)
        {
            if (gridDictionnary.Count == 1)
            {
                playerATK = 1;
            }
            else
            {
                playerATK -= int.Parse(c.damage);
            }

            playerDEF -= int.Parse(c.armor);
            gridDictionnary.Remove(c.id);

            for (int i = 0; i < grid.transform.childCount; i++)
            {
                if (grid.transform.GetChild(i).gameObject == g)
                {
                    GameObject u = grid.transform.GetChild(i).gameObject;
                    u.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => Destroy(u.gameObject));                    
                }                    
            }
        }

        UpdateStatsDmgReceivePlayer();
        UpdateStatsUIPlayer();
    }

    public void ResetGrid()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        gridDictionnary.Clear();
    }

    public void StartFight()
    {
        //UpdateStatsUIPlayer();
    }

    public void UpdateStatsDmgReceivePlayer()
    {
        int playerDMGReceiveMin = 0;
        int playerDMGReceiveMax = 0;

        int minAtkMonster = GameManager.Instance.monsterManager.minAtk;
        int maxAtkMonster = GameManager.Instance.monsterManager.maxAtk;
        int hpMonster = GameManager.Instance.monsterManager.hp;
        int nbAtk;

        nbAtk = Mathf.CeilToInt(hpMonster / playerATK) + 1;


        playerDMGReceiveMin = (nbAtk * minAtkMonster) - playerDEF;
        playerDMGReceiveMax = (nbAtk * maxAtkMonster) - playerDEF;


        if (playerDMGReceiveMin < 0)
            playerDMGReceiveMin = 0;
        if (playerDMGReceiveMax < 0)
            playerDMGReceiveMax = 0;

        textDmgReceivePlayerStats.text = playerDMGReceiveMin.ToString() + "  /  " + playerDMGReceiveMax.ToString();
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
        playerATK = 1;
        playerDEF = 0;
        UpdateStatsUIPlayer();
    }
}
