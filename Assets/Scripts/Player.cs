using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats UI")]

    public TMP_Text textAtkPlayerStats;
    public TMP_Text textDefPlayerStats;
    public TMP_Text textTotalPoints;

    public GameObject equipmentGrid;
    public GameObject adventurersGrid;
    public GameObject atkUI;
    public GameObject defUI;

    [Header("Reward")]
    public GameObject reward;
    public GameObject honorPlayer;
    public GameObject honorMonster;
    public GameObject honorPlayerTotal;
    public TMP_Text textHonorPlayer;
    public TMP_Text textHonorMonster;
    public TMP_Text textHonorPlayerTotal;

    [Header("Punishment")]
    public GameObject punishment;

    [Header("Other")]
    public GameObject model;
    public GameObject adventurerFight;

    [Header("Stats Atk")]
    public int playerATKTotal = 0;
    public int playerATKNoElement = 0;
    public int playerFireATK = 0;
    public int playerIceATK = 0;
    public int playerElectricATK = 0;

    [Header("Stats Def")]
    public int playerDEFTotal = 0;
    public int playerDEFNoElement = 0;
    public int playerFireDEF = 0;
    public int playerIceDEF = 0;
    public int playerElectricDEF = 0;

    [HideInInspector] public Dictionary<string, Adventurer> adventurersDictionnary = new Dictionary<string, Adventurer>();
    [HideInInspector] private Dictionary<string, GameObject> equipmentDictionnary = new Dictionary<string, GameObject>();

    [HideInInspector] public Adventurer currentAdventurer = null;
    [HideInInspector] public bool rewardBool = false;
    [HideInInspector] public bool punishmentBool = false;

    private void Start()
    {
        textAtkPlayerStats.GetComponent<RectTransform>().DOScale(Vector3.one * 1.05f, 1f).SetLoops(-1, LoopType.Yoyo);
        textDefPlayerStats.GetComponent<RectTransform>().DOScale(Vector3.one * 1.05f, 1f).SetLoops(-1, LoopType.Yoyo);
        ResetStats();
        ResetAdventurer();
        UpdateStatsUIPlayer();        
    }

    private void Update()
    {
        
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
                if (!tmpAdv.isDead)
                    ChangeCurrentAdventurer(tmpAdv);
            }
        }
    }

    public void ChangeCurrentAdventurer(Adventurer adv)
    {
        if (currentAdventurer != null)
            currentAdventurer.gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);

        adv.gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.OutBounce);
        //adv.gameObject.transform.DOMoveY(adv.gameObject.transform.position.y + 0.2f, 0.5f).SetEase(Ease.OutBounce);
        currentAdventurer = adv;
    }

    public void AddEquipment(CardsInformations c)
    {
        if (GameManager.Instance.state == GameManager.State.STATS)
        {
            GameObject equipment;

            //add the gameobject to the equipment grid
            if (c.damage != "0" && c.armor != "0")
            {
                equipment = Instantiate(GameManager.Instance.cardMixtPrefab, equipmentGrid.transform);
                equipment.GetComponent<Stuff>().stuffImage.sprite = GameManager.Instance.spriteStuff[int.Parse((c.id).Split('.')[0]) - 1];
                equipment.GetComponent<Stuff>().textValueAtk.text = c.damage;
                equipment.GetComponent<Stuff>().textValueDef.text = c.armor;
                equipmentDictionnary.Add(c.id, equipment);
            }
            else if (c.damage != "0")
            {
                equipment = Instantiate(GameManager.Instance.cardAtkPrefab, equipmentGrid.transform);
                equipment.GetComponent<Stuff>().stuffImage.sprite = GameManager.Instance.spriteStuff[int.Parse((c.id).Split('.')[0]) - 1];
                equipment.GetComponent<Stuff>().textValueAtk.text = c.damage;
                equipmentDictionnary.Add(c.id, equipment);
            }
            else if (c.armor != "0")
            {
                equipment = Instantiate(GameManager.Instance.cardDefPrefab, equipmentGrid.transform);
                //equipment.GetComponent<Stuff>().stuffImage.sprite = GameManager.Instance.spriteStuff[int.Parse((c.id).Split('.')[0]) - 1];
                equipment.GetComponent<Stuff>().stuffImage.sprite = GameManager.Instance.spriteStuff[20];
                equipment.GetComponent<Stuff>().textValueDef.text = c.armor;               
                equipmentDictionnary.Add(c.id, equipment);
            }

            if (equipmentDictionnary.TryGetValue(c.id, out equipment)){
                equipment.transform.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBounce);
            }

            Stuff stuff = equipment.GetComponent<Stuff>();

            //calculate different type of atk and def
            Color co = new Color(stuff.imageElementAtk.color.r, stuff.imageElementAtk.color.g, stuff.imageElementAtk.color.b, 255);
            if (c.damageElement.Trim().Replace("\r", "") != "")
            {               
                switch (c.damageElement.Replace("\r", ""))
                {
                    case "fire": playerFireATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.fireSprite; stuff.imageElementAtk.color = co; stuff.elementAtk = Element.FIRE;  break;
                    case "ice": playerIceATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.iceSprite; stuff.imageElementAtk.color = co; stuff.elementAtk = Element.ICE; break;
                    case "electric": playerElectricATK += int.Parse(c.damage); stuff.imageElementAtk.sprite = GameManager.Instance.electricSprite; stuff.imageElementAtk.color = co; stuff.elementAtk = Element.ELECTRIC; break;                    
                }
            }
            else
            {
                playerATKNoElement += int.Parse(c.damage);
            }
            co = new Color(stuff.imageElementDef.color.r, stuff.imageElementDef.color.g, stuff.imageElementDef.color.b, 255);
            if (c.armorElement.Trim().Replace("\r", "") != "")
            {
                switch (c.armorElement.Replace("\r", ""))
                {
                    case "fire":  playerFireDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.fireSprite; stuff.imageElementDef.color = co; stuff.elementDef = Element.FIRE; break;
                    case "ice":  playerIceDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.iceSprite; stuff.imageElementDef.color = co; stuff.elementDef = Element.ICE; break;
                    case "electric": playerElectricDEF += int.Parse(c.armor); stuff.imageElementDef.sprite = GameManager.Instance.electricSprite; stuff.imageElementDef.color = co; stuff.elementDef = Element.ELECTRIC; break;
                    default: break;
                }
            }
            else
            {
                playerDEFNoElement += int.Parse(c.armor);
            }

            //feedback atk or def is double
            if (stuff.elementAtk == GameManager.Instance.monsterManager.elementDef && stuff.elementAtk != Element.NULL)
            {
                stuff.AtkIsDouble();
            }
            if (stuff.elementDef == GameManager.Instance.monsterManager.elementAtk && stuff.elementDef != Element.NULL)
            {
                stuff.DefIsDouble();
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
            if (equipmentGrid.transform.GetChild(i).GetComponent<Stuff>() != null)
                Destroy(equipmentGrid.transform.GetChild(i).gameObject);
        }
        equipmentDictionnary.Clear();
    }

    public void ResetAdventurer()
    {
        for (int i = 0; i < adventurersGrid.transform.childCount; i++)
        {
            if (adventurersGrid.transform.GetChild(i).GetComponent<Adventurer>() != null)
                Destroy(adventurersGrid.transform.GetChild(i).gameObject);
        }
        adventurersDictionnary.Clear();
    }

    public void DisplayUIBeforeFight(int xValue)
    {
        equipmentGrid.transform.DOMoveX(equipmentGrid.transform.position.x - (xValue*10), 1f);
        adventurersGrid.transform.DOMoveX(equipmentGrid.transform.position.x - (xValue*9), 1f);
        atkUI.SetActive(false);
        defUI.SetActive(false);
        atkUI.transform.DOMoveY(atkUI.transform.position.y - 12, 1f);
        defUI.transform.DOMoveY(defUI.transform.position.y - 12, 1f);
    }

    public void DisplayUIFight()
    {
        atkUI.SetActive(true);
        defUI.SetActive(true);
        atkUI.transform.DOMoveY(atkUI.transform.position.y +4, 1f).SetEase(Ease.OutSine);
        defUI.transform.DOMoveY(defUI.transform.position.y +4, 1f).SetEase(Ease.OutSine);

        adventurerFight.GetComponent<Adventurer>().hp = currentAdventurer.hp;
        adventurerFight.GetComponent<Adventurer>().level = currentAdventurer.level;
        adventurerFight.GetComponent<Adventurer>().points = currentAdventurer.points;
        adventurerFight.GetComponent<Adventurer>().UpdateUIAdventurer();

        adventurerFight.transform.DOMoveY(adventurerFight.transform.position.y + 5, 1f);
    }

    public void UnshowUIFight(int xValue)
    {
        atkUI.transform.DOMoveY(atkUI.transform.position.y - 4, 0.5f).SetEase(Ease.OutSine);
        defUI.transform.DOMoveY(defUI.transform.position.y - 4, 0.5f).SetEase(Ease.OutSine);
        adventurerFight.transform.DOMoveY(adventurerFight.transform.position.y - 5, 1f);     
        if (rewardBool)
            reward.transform.DOMoveX(reward.transform.position.x + (7.5f * xValue), 0.5f).SetEase(Ease.OutSine);
        if (punishmentBool)
            punishment.transform.DOMoveX(punishment.transform.position.x + (7.5f * xValue), 0.5f).SetEase(Ease.OutSine);

        rewardBool = false;
        punishmentBool = false;
    }

    public void DisplayUIStats(int xValue)
    {
        equipmentGrid.transform.DOMoveX(equipmentGrid.transform.position.x - (xValue*10), 1f);
        adventurersGrid.transform.DOMoveX(equipmentGrid.transform.position.x - (xValue*10.5f), 1f);
        atkUI.transform.DOMoveY(atkUI.transform.position.y + 12, 1f).SetEase(Ease.OutSine);
        defUI.transform.DOMoveY(defUI.transform.position.y + 12, 1f).SetEase(Ease.OutSine);
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
            case Element.FIRE: playerDEFTotal = playerFireDEF*2 + playerIceDEF + playerElectricDEF + playerDEFNoElement; break;
            case Element.ICE: playerDEFTotal = playerFireDEF + playerIceDEF*2 + playerElectricDEF + playerDEFNoElement; break;
            case Element.ELECTRIC: playerDEFTotal = playerFireDEF + playerIceDEF + playerElectricDEF*2 + playerDEFNoElement; break;
            case Element.NULL: playerDEFTotal = playerFireDEF + playerIceDEF + playerElectricDEF + playerDEFNoElement; break;
        }
        Element elementMonsterDef = GameManager.Instance.monsterManager.elementDef;
        switch (elementMonsterDef)
        {
            case Element.FIRE: playerATKTotal = playerFireATK * 2 + playerIceATK + playerElectricATK + playerATKNoElement; break;
            case Element.ICE: playerATKTotal = playerFireATK + playerIceATK * 2 + playerElectricATK + playerATKNoElement; break;
            case Element.ELECTRIC: playerATKTotal = playerFireATK + playerIceATK + playerElectricATK * 2 + playerATKNoElement; break;
            case Element.NULL: playerATKTotal = playerFireATK + playerIceATK + playerElectricATK + playerATKNoElement; break;
        }
    }

    public void UpdateStatsUIPlayer()
    {
        textAtkPlayerStats.text = playerATKTotal.ToString();
        textDefPlayerStats.text = playerDEFTotal.ToString();
        
        UpdateAdventurer();        
    }

    public void UpdateAdventurer()
    {
        float p = 0;
        foreach (KeyValuePair<string, Adventurer> a in adventurersDictionnary)
        {
            if (a.Value.hp <= 0)
            {
                SoundManager.instance.DeathOfTheGladiator();
                a.Value.isDead = true;
                a.Value.isDeadPicture.SetActive(true);
                return;
            }               
            p += a.Value.points;
        }
        textTotalPoints.text = p.ToString();
    }

    public void ResetStats()
    {
        playerATKTotal = 0;
        playerATKNoElement = 0;
        playerFireATK = 0;
        playerIceATK = 0;
        playerElectricATK = 0;

        playerDEFTotal = 0;
        playerDEFNoElement = 0;
        playerFireDEF = 0;
        playerIceDEF = 0;
        playerElectricDEF = 0;

        UpdateStatsUIPlayer();
        ResetEquipment();        
    }


}
