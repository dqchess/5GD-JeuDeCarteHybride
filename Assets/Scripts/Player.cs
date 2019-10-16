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

    public TMP_Text textAtkPlayerStats;
    public TMP_Text textDefPlayerStats;
    public TMP_Text textTotalPoints;

    public GameObject equipmentGrid;
    public GameObject adventurersGrid;

    [Header("Fight")]
    public TMP_Text textHpPlayerFight;
    public TMP_Text textAtkPlayerFight;
    public TMP_Text textDefPlayerFight;

    [Header("Other")]
    public GameObject model;

    [HideInInspector] public int playerATK = 0;
    [HideInInspector] public int playerDEF = 0;

    public Dictionary<string, Adventurer> adventurersDictionnary = new Dictionary<string, Adventurer>();
    private Dictionary<string, GameObject> equipmentDictionnary = new Dictionary<string, GameObject>();

    [HideInInspector] public Adventurer currentAdventurer = null;

    private void Start()
    {
        ResetEquipment();
        ResetAdventurer();
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

            UpdateStatsUIPlayer();
        }       
    }

    public void ScanAdventurer(string IdAdv)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            if (!adventurersDictionnary.ContainsKey(IdAdv)) // create adventurer
            {
                GameObject a = Instantiate(GameManager.Instance.adventurer, adventurersGrid.transform);
                Adventurer adv = a.GetComponent<Adventurer>();
                adv.id = int.Parse(IdAdv);
                adventurersDictionnary.Add(adv.id.ToString(), adv);
                a.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
                ChangeCurrentAdventurer(adv);
            }

            Adventurer tmpAdv;
            if (adventurersDictionnary.TryGetValue(IdAdv, out tmpAdv))
            {
                ChangeCurrentAdventurer(tmpAdv);
            }
        }
    }

    public void ChangeCurrentAdventurer(Adventurer adv)
    {
        if (currentAdventurer != null)
            currentAdventurer.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        adv.gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.OutBounce);
        currentAdventurer = adv;
    }

    public void AddStatsPlayer(CardsInformations c)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            playerATK += int.Parse(c.damage);
            
            playerDEF += int.Parse(c.armor);

            GameObject card;

            if (c.damage != "0" && c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardMixtPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.GetComponent<Stuff>().textValueDef.text = c.armor;

                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);

                equipmentDictionnary.Add(c.id, card);
            }
            else if (c.damage != "0")
            {
                card = Instantiate(GameManager.Instance.cardAtkPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
                equipmentDictionnary.Add(c.id, card);
            }
            else if (c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardDefPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueDef.text = c.damage;
                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
                equipmentDictionnary.Add(c.id, card);
            }

            UpdateStatsUIPlayer();
        }
    }

    public void RemoveStatsPlayer(CardsInformations c)
    {
        GameObject g;
        bool b = equipmentDictionnary.TryGetValue(c.id, out g);
        if (b)
        {
            playerATK -= int.Parse(c.damage);
            

            playerDEF -= int.Parse(c.armor);
            equipmentDictionnary.Remove(c.id);

            for (int i = 0; i < equipmentGrid.transform.childCount; i++)
            {
                if (equipmentGrid.transform.GetChild(i).gameObject == g)
                {
                    GameObject u = equipmentGrid.transform.GetChild(i).gameObject;
                    u.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => Destroy(u.gameObject));                    
                }                    
            }
        }

        UpdateStatsUIPlayer();
    }

    public void ResetEquipment()
    {
        for (int i = 0; i < equipmentGrid.transform.childCount; i++)
        {
            Destroy(equipmentGrid.transform.GetChild(i).gameObject);
        }
        equipmentDictionnary.Clear();
    }

    public void ResetAdventurer()
    {
        for (int i = 0; i < adventurersGrid.transform.childCount; i++)
        {
            Destroy(adventurersGrid.transform.GetChild(i).gameObject);
        }
        adventurersDictionnary.Clear();
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

        //FIGHT
        textAtkPlayerFight.text = playerATK.ToString();
        textDefPlayerFight.text = playerDEF.ToString();
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
