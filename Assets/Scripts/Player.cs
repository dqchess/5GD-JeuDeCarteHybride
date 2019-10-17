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

    [Header("Other")]
    public GameObject model;

    public int playerATKTotal = 0;
    public int playerATKNoElement = 0;
    public int playerFireATK = 0;
    public int playerIceATK = 0;
    public int playerElectricATK = 0;

    public int playerDEFTotal = 0;
    public int playerDEFNoElement = 0;
    public int playerFireDEF = 0;
    public int playerIceDEF = 0;
    public int playerElectricDEF = 0;

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
            playerATKNoElement += tmpInt;           
            ATKInputPlayer.text = "";
        }
        b = false;
        b = int.TryParse(DEFInputPlayer.text, out tmpInt);
        if (b)
        {
            playerDEFNoElement += tmpInt;            
            DEFInputPlayer.text = "";
        }
        UpdateStatsUIPlayer();
    }

    public void AddStatsPlayer(int atk, int def)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            playerATKNoElement += atk;
            playerDEFNoElement += def;

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

    public void AddEquipment(CardsInformations c)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            GameObject card;

            if (c.damage != "0" && c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardMixtPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                card.GetComponent<Stuff>().textValueDef.text = c.armor;
                equipmentDictionnary.Add(c.id, card);
            }
            else if (c.damage != "0")
            {
                card = Instantiate(GameManager.Instance.cardAtkPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueAtk.text = c.damage;
                equipmentDictionnary.Add(c.id, card);
            }
            else if (c.armor != "0")
            {
                card = Instantiate(GameManager.Instance.cardDefPrefab, equipmentGrid.transform);
                //card.GetComponent<Stuff>().stuffImage.texture = c.texture;
                card.GetComponent<Stuff>().textValueDef.text = c.armor;               
                equipmentDictionnary.Add(c.id, card);
            }

            if (equipmentDictionnary.TryGetValue(c.id, out card)){
                card.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
            }

            Stuff stuff = card.GetComponent<Stuff>();

            if (c.damageElement != "")
            {
                switch (c.damageElement)
                {
                    case "fire": playerFireATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.fireSprite; stuff.elementAtk = Element.FIRE;  break;
                    case "ice": playerIceATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.iceSprite; stuff.elementAtk = Element.ICE; break;
                    case "electric": playerElectricATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.electricSprite; stuff.elementAtk = Element.ELECTRIC; break;
                }
            }
            else 
            {
                playerATKNoElement += int.Parse(c.damage);
            }
            if (c.armorElement != "")
            {
                switch (c.armorElement)
                {
                    case "fire": playerFireDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.fireSprite; stuff.elementDef = Element.FIRE; break;
                    case "ice": playerIceDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.iceSprite; stuff.elementDef = Element.ICE; break;
                    case "electric": playerElectricDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.electricSprite; stuff.elementDef = Element.ELECTRIC; break;
                }
            }
            else
            {
                playerDEFNoElement += int.Parse(c.damage);
            }
            UpdateStatsPlayer();
            UpdateStatsUIPlayer();
        }
    }

    public void RemoveEquipment(CardsInformations c)
    {
        GameObject g;
        bool b = equipmentDictionnary.TryGetValue(c.id, out g);
        if (b)
        {
            equipmentDictionnary.Remove(c.id);

            for (int i = 0; i < equipmentGrid.transform.childCount; i++)
            {
                if (equipmentGrid.transform.GetChild(i).gameObject == g)
                {
                    GameObject u = equipmentGrid.transform.GetChild(i).gameObject;
                    u.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine).OnComplete(() => Destroy(u.gameObject));                    
                }                    
            }

            if (c.damageElement != "")
            {
                switch (c.damageElement)
                {
                    case "fire": playerFireATK -= int.Parse(c.damage); break;
                    case "ice": playerIceATK -= int.Parse(c.damage); break;
                    case "electric": playerElectricATK -= int.Parse(c.damage); break;
                }
            }
            if (c.armorElement != "")
            {
                switch (c.armorElement)
                {
                    case "fire": playerFireDEF -= int.Parse(c.armor); break;
                    case "ice": playerIceDEF -= int.Parse(c.armor); break;
                    case "electric": playerElectricDEF -= int.Parse(c.armor); break;
                }
            }
        }
        UpdateStatsPlayer();
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

    public void UpdateStatsPlayer()
    {
        Element elementMonsterAtk = GameManager.Instance.monsterManager.elementAtk;
        switch (elementMonsterAtk)
        {
            case Element.FIRE: playerDEFTotal = playerFireDEF*2 + playerIceDEF + playerElectricDEF; break;
            case Element.ICE: playerDEFTotal = playerFireDEF + playerIceDEF*2 + playerElectricDEF; break;
            case Element.ELECTRIC: playerDEFTotal = playerFireDEF + playerIceDEF + playerElectricDEF*2; break;
            case Element.NULL: playerDEFTotal = playerFireDEF + playerIceDEF + playerElectricDEF; break;
        }
        Element elementMonsterDef = GameManager.Instance.monsterManager.elementDef;
        switch (elementMonsterDef)
        {
            case Element.FIRE: playerATKTotal = playerFireATK * 2 + playerIceATK + playerElectricATK; break;
            case Element.ICE: playerATKTotal = playerFireATK + playerIceATK * 2 + playerElectricATK; break;
            case Element.ELECTRIC: playerATKTotal = playerFireATK + playerIceATK + playerElectricATK * 2; break;
            case Element.NULL: playerATKTotal = playerFireATK + playerIceATK + playerElectricATK; break;
        }
    }

    public void UpdateStatsUIPlayer()
    {
        textAtkPlayerStats.text = playerATKTotal.ToString();
        textDefPlayerStats.text = playerDEFTotal.ToString();
    }

    public void ResetStats()
    {
        playerATKNoElement = 0;
        playerDEFNoElement = 0;
        UpdateStatsUIPlayer();
    }


}
