using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public enum State { STATS, FIGHT };
    [HideInInspector] public State state;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    [Header("Tweek This Bande de GD !")]
    [Tooltip("chance to have Element on atk and def : 1 on ? ")]
    public int randomElementMonster;

    [Header("Prefabs")]
    public Player player1;
    public Player player2;
    public MonsterManager monsterManager;
    public GameObject cardAtkPrefab;
    public GameObject cardDefPrefab;
    public GameObject cardMixtPrefab;
    public GameObject adventurer;
    
    [Header("UI")]
    public GameObject panelStats;
    public GameObject monsterUi;
    public TMP_Text textVictory;
    public TMP_Text textFight;
    public TMP_Text textDraw;
    public TMP_Text textDrawStats;
    public TMP_Text textEndFight;
    public TMP_Text textEndFightGold;

    [Header("Element")]
    public Sprite fireSprite;
    public Sprite iceSprite;
    public Sprite electricSprite;


    [Header("Particles")]
    public GameObject hitFight;
    public GameObject spawnMonster;

    [Header("Environment")]
    public GameObject plane;

    [Header("Stuff")]
    public Sprite[] spriteStuff;

    [HideInInspector] public Vector3 cameraPositionStats;
    [HideInInspector] public Vector3 cameraRotationStats;

    [HideInInspector] public Vector3 cameraPositionFight;
    [HideInInspector] public Vector3 cameraRotationFight;
    [HideInInspector] public int turn = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CardsInformations c = new CardsInformations("1.1","megamassue","5","0","electric", "");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CardsInformations c = new CardsInformations("1.1", "megamassue", "5", "0", "electric", "");
            player2.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "", "electric");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "", "electric");
            player2.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CardsInformations c = new CardsInformations("1.3", "megaboubou", "8", "8", "ice", "electric");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CardsInformations c = new CardsInformations("1.3", "megaboubou", "8", "8", "ice", "electric");
            player2.AddEquipment(c);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player1.ScanAdventurer("1");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            player1.ScanAdventurer("2");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            player2.ScanAdventurer("3");
        }
    }

    private void Start()
    {
        state = State.STATS;

        cameraPositionFight = new Vector3(0, 3, -8);
        cameraRotationFight = new Vector3(7, 0, 0);

        cameraPositionStats = new Vector3(0, 4.5f, -11);
        cameraRotationStats = new Vector3(7, 0, 0);

        Stats();
    }

    public void Stats()
    {
        state = State.STATS;
        SoundManager.instance.NegociationBegin();
        StartCoroutine(_Stats());
    }

    public IEnumerator _Stats()
    {
        if (turn != 0)
        {
            player1.DisplayUIStats(-1); //to the right
            player2.DisplayUIStats(1);//to the left
        }
        
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, 0, 0), 1f);
        monsterManager.InstantiateMonster();
        
        player1.ResetStats();
        player2.ResetStats();

        yield return new WaitForEndOfFrame();
    }

    public void Fight()
    {        
        SoundManager.instance.EndOfNegociation();
        state = State.FIGHT;        
        StartCoroutine(_Fight());        
    }

    public IEnumerator _Fight()
    {
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.LaunchFight();
        SoundManager.instance.IdleMonster();
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.BeginningOfTheFight();
        
        yield return new WaitForSeconds(1.5f);
        //START FIGHT
        player1.DisplayUIBeforeFight(1); //to the left
        player2.DisplayUIBeforeFight(-1);//to the right       
        monsterUi.transform.DOMoveY(monsterUi.transform.position.y + 5, 0.5f);
        monsterManager.monsterPreview.transform.DORotate(Quaternion.identity.eulerAngles + Vector3.up*90f, 1f);
        textFight.gameObject.transform.DOMoveY(textFight.transform.position.y - 4f, 1).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            SoundManager.instance.MonsterSoundBase();
            Camera.main.transform.DOLocalMoveY(cameraPositionFight.y, 0.5f).SetEase(Ease.OutExpo);
            Camera.main.transform.DOLocalMoveZ(cameraPositionFight.z, 0.5f).SetEase(Ease.OutExpo);
        });

        yield return new WaitForSeconds(3);

        monsterUi.transform.DOMoveY(monsterUi.transform.position.y - 5, 0.5f);

        //PLAYER1 FIGHT       
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, -240, 0), 1f);
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x - 4.5f, 1f);
        player1.DisplayUIFight();

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(monsterManager.DisplayRandomAtk());

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(StartFightPlayer(player1));

        yield return new WaitForSeconds(2);
        monsterManager.SetMonsterStats(monsterManager.monsterStats);
        player1.UnshowUIFight(-1);
        yield return new WaitForSeconds(0.5f);

        //PLAYER2 FIGHT
        player2.DisplayUIFight();
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x + 4.5f, 1f);
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, 60, 0), 1f);

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(StartFightPlayer(player2));

        yield return new WaitForSeconds(2f);

        player2.UnshowUIFight(1);

        yield return new WaitForSeconds(2);

        EndFight();

        yield return null;
    }

    public IEnumerator StartFightPlayer(Player p)
    {
        int monsterMaxDef = monsterManager.def;

        //I ATTACK THE MONSTER
        bool monsterIsDead = false;
        bool playerIsDead = false;
        bool b = false;
        p.atkUI.GetComponent<RectTransform>().DOShakeAnchorPos(2, 15, 100);
        yield return new WaitForSeconds(1.8f);
        p.atkUI.GetComponent<RectTransform>().DOMove(monsterManager.textDefMonsterStat.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2,LoopType.Yoyo).OnStepComplete(() => {
            if (!b)
            {
                b = true;
                Instantiate(hitFight, monsterManager.textDefMonsterStat.transform.position, Quaternion.identity);
                SoundManager.instance.Attack();
                if (p.playerFireATK > 0)
                    SoundManager.instance.DamageFireDeal();
                if (p.playerIceATK > 0)
                    SoundManager.instance.DamageIceDeal();
                if (p.playerElectricATK > 0)
                    SoundManager.instance.DamageElecDeal();
                monsterManager.def -= p.playerATKTotal;
                monsterManager.UpdateUIMonster();
                monsterManager.textDefMonsterStat.GetComponent<RectTransform>().DOScale(monsterManager.textDefMonsterStat.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
                if (monsterManager.def <= 0) //le monstre été tué
                {
                    monsterManager.textDefMonsterStat.text = "DEAD";
                }
            }
        });
        yield return new WaitForSeconds(1f);

        if (monsterManager.def <= 0) //le monstre a été tué
        {
            monsterIsDead = true;
            if (p == player1)
                yield return StartCoroutine(DisplayReward(p, 1));
            else if(p == player2)
                yield return StartCoroutine(DisplayReward(p, -1));
        }
        yield return new WaitForSeconds(1f);

        //THE MONSTER ATTACK ME
        b = false;
        monsterManager.atkUI.GetComponent<RectTransform>().DOShakeAnchorPos(2, 15, 100);
        yield return new WaitForSeconds(1.8f);
        monsterManager.atkUI.GetComponent<RectTransform>().DOMove(p.defUI.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnStepComplete(() => {
            if (!b)
            {
                b = true;
                Instantiate(hitFight, p.defUI.transform.position, Quaternion.identity);

                if (monsterManager.elementAtk == Element.FIRE)
                    SoundManager.instance.DamageFireDeal();
                if (monsterManager.elementAtk == Element.ICE)
                    SoundManager.instance.DamageIceDeal();
                if (monsterManager.elementAtk == Element.ELECTRIC)
                    SoundManager.instance.DamageElecDeal();
                if(monsterManager.elementAtk == Element.NULL)
                    SoundManager.instance.DamageDeal();

                p.playerDEFTotal -= monsterManager.atk;
                p.UpdateStatsUIPlayer();
                p.textDefPlayerStats.GetComponent<RectTransform>().DOScale(p.textDefPlayerStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
                if (p.playerDEFTotal <= 0) //j'ai été tué 
                {
                    p.textDefPlayerStats.text = "DEAD";
                }
            }
        });

        yield return new WaitForSeconds(1f);

        if (p.playerDEFTotal <= 0) //j'ai été tué 
        {
            playerIsDead = true;
            if (p == player1)
                yield return StartCoroutine(DisplayPunishment(p, 1));
            else if (p == player2)
                yield return StartCoroutine(DisplayPunishment(p, -1));
        }

        yield return new WaitForSeconds(1f);

        if (!playerIsDead && !monsterIsDead) //draw
        {
            //display random def 
            DOTween.To(() => textDraw.fontSize, x => textDraw.fontSize = x, 90, 1f).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo);
            yield return new WaitForSeconds(2f);
            monsterManager.textDefMonsterStat.text = monsterManager.def.ToString() + " / " + monsterMaxDef.ToString();
            monsterManager.textDefMonsterStat.GetComponent<RectTransform>().DOScale(monsterManager.textDefMonsterStat.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            monsterManager.defUI.transform.DOMoveX(monsterManager.defUI.transform.position.x - 1.7f, 0.3f);
            monsterManager.defUI.transform.DOMoveY(monsterManager.defUI.transform.position.y - 4, 0.3f);

            int pourcentageCrit = Mathf.CeilToInt(((float)monsterManager.def / (float)monsterMaxDef) * 100);
            textDrawStats.text = pourcentageCrit.ToString() + " % chance to crit";
            DOTween.To(() => textDrawStats.fontSize, x => textDrawStats.fontSize = x, 25, 1f).SetEase(Ease.OutSine).SetDelay(0.5f);

            yield return new WaitForSeconds(2.5f);

            monsterManager.defUI.GetComponent<RectTransform>().DOShakeAnchorPos(1.5f, 15, 100);

            yield return new WaitForSeconds(1.5f);

            if (Random.Range(0,100) <= pourcentageCrit) //monster crit and i lose hp
            {
                textDrawStats.text = "Monster critical success";
                textDrawStats.GetComponent<RectTransform>().DOScale(textDrawStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(1f);

                monsterManager.textAtkMonsterStats.text = "CRIT";
                b = false;
                monsterManager.atkUI.GetComponent<RectTransform>().DOShakeAnchorPos(1.2f, 15, 100);
                yield return new WaitForSeconds(1f);
                monsterManager.atkUI.GetComponent<RectTransform>().DOMove(p.defUI.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnStepComplete(() => {
                    if (!b)
                    {
                        b = true;
                        Instantiate(hitFight, p.defUI.transform.position, Quaternion.identity);
                        p.textDefPlayerStats.text = "DEAD";
                        p.textDefPlayerStats.GetComponent<RectTransform>().DOScale(p.textDefPlayerStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);

                        if (monsterManager.elementAtk == Element.FIRE)
                            SoundManager.instance.DamageFireDeal();
                        if (monsterManager.elementAtk == Element.ICE)
                            SoundManager.instance.DamageIceDeal();
                        if (monsterManager.elementAtk == Element.ELECTRIC)
                            SoundManager.instance.DamageElecDeal();
                        if (monsterManager.elementAtk == Element.NULL)
                            SoundManager.instance.DamageDeal();
                    }
                });
                yield return new WaitForSeconds(0.5f);
                if (p == player1)
                    yield return StartCoroutine(DisplayPunishment(p, 1));
                else if (p == player2)
                    yield return StartCoroutine(DisplayPunishment(p, -1));
            }
            else //monster don't crit and i gain honor
            {
                textDrawStats.text = "Monster critical fail";
                textDrawStats.GetComponent<RectTransform>().DOScale(textDrawStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(1f);

                p.textAtkPlayerStats.text = "CRIT";
                b = false;
                p.atkUI.GetComponent<RectTransform>().DOMove(monsterManager.textDefMonsterStat.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnStepComplete(() => {
                    if (!b)
                    {
                        b = true;
                        Instantiate(hitFight, monsterManager.textDefMonsterStat.transform.position, Quaternion.identity);
                        monsterManager.textDefMonsterStat.text = "DEAD";
                        monsterManager.textDefMonsterStat.GetComponent<RectTransform>().DOScale(monsterManager.textDefMonsterStat.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);                       
                    }
                });
                yield return new WaitForSeconds(0.5f);
                if (p == player1)
                    yield return StartCoroutine(DisplayReward(p, 1));
                else if (p == player2)
                    yield return StartCoroutine(DisplayReward(p, -1));

            }

            yield return new WaitForSeconds(1f);
            //reset defmonster Ui pos
            DOTween.To(() => textDrawStats.fontSize, x => textDrawStats.fontSize = x, 0, 1f).SetEase(Ease.OutSine).SetDelay(0.5f);
            monsterManager.defUI.transform.DOMoveX(monsterManager.defUI.transform.position.x + 1.7f, 0.3f);
            monsterManager.defUI.transform.DOMoveY(monsterManager.defUI.transform.position.y + 4, 0.3f);
        }   

        yield return null;
    }

    private IEnumerator DisplayReward(Player p, int xValue)
    {
        p.rewardBool = true;
        p.reward.transform.DOMoveX(p.reward.transform.position.x + (7.5f * xValue), 0.5f).SetEase(Ease.OutBounce);
        p.textHonorPlayer.text = p.currentAdventurer.level.ToString();
        p.textHonorMonster.text = monsterManager.honor.ToString();
        p.textHonorPlayerTotal.text = (p.currentAdventurer.level * monsterManager.honor).ToString();

        p.adventurerFight.GetComponent<Adventurer>().points += (p.currentAdventurer.level * monsterManager.honor);
        p.adventurerFight.GetComponent<Adventurer>().level += 1;
        SoundManager.instance.LevelUping();
        p.currentAdventurer.points += (p.currentAdventurer.level * monsterManager.honor);
        p.currentAdventurer.level += 1;

        yield return new WaitForSeconds(1f);
        p.currentAdventurer.UpdateUIAdventurer();
        p.adventurerFight.GetComponent<Adventurer>().UpdateUIAdventurer();

        yield return null;
    }

    private IEnumerator DisplayPunishment(Player p, int xValue)
    {
        p.punishmentBool = true;
        p.punishment.transform.DOMoveX(p.reward.transform.position.x + (7.5f * xValue), 0.5f).SetEase(Ease.OutBounce);
        SoundManager.instance.TriumphantCry();
        SoundManager.instance.LoosingTheFight();
        p.adventurerFight.GetComponent<Adventurer>().hp -= 1;

        p.currentAdventurer.hp -= 1;

        yield return new WaitForSeconds(1f);
        p.currentAdventurer.UpdateUIAdventurer();
        p.adventurerFight.GetComponent<Adventurer>().UpdateUIAdventurer();

        yield return null;
    }


    public void EndFight()
    {
        turn += 1;
        StartCoroutine(_EndFight());
        SoundManager.instance.EndOfTheFight();
        ExcelManager.Instance.EmptyCardsAfterCombat();
    }

    private IEnumerator _EndFight()
    {
        monsterManager.DestroyMonster();
        SoundManager.instance.GetTheGold();
        textEndFightGold.text = " Both players take your " + monsterManager.loot + " golds !";

        DOTween.To(() => textEndFightGold.fontSize, x => textEndFightGold.fontSize = x, 40, 1f).SetEase(Ease.OutBounce);
        DOTween.To(() => textEndFightGold.fontSize, x => textEndFightGold.fontSize = x, 0, 0.5f).SetEase(Ease.InSine).SetDelay(1.5f);
        yield return new WaitForSeconds(1.5f);
        
        Camera.main.transform.DOMove(cameraPositionStats, 1f);

        yield return new WaitForSeconds(3);
        Stats();
    }

}

public enum Element
{
    NULL,
    FIRE,
    ICE,
    ELECTRIC,
}
