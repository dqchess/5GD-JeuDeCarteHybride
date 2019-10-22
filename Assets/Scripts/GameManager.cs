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
    public GameObject panelFight;
    public GameObject monsterUi;
    public TMP_Text textVictory;
    public TMP_Text textFight;
    public TMP_Text textEndFight;
    public TMP_Text textEndFightGold;

    [Header("Element")]
    public Sprite fireSprite;
    public Sprite iceSprite;
    public Sprite electricSprite;


    [Header("Particles")]
    public GameObject despawnMonster;
    public GameObject spawnMonster;

    [Header("Environment")]
    public GameObject plane;

    [HideInInspector] public Vector3 cameraPositionStats;
    [HideInInspector] public Vector3 cameraRotationStats;

    [HideInInspector] public Vector3 cameraPositionFight;
    [HideInInspector] public Vector3 cameraRotationFight;
    [HideInInspector] public int turn = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CardsInformations c = new CardsInformations("1.1","megamassue","5","0","electric", "");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CardsInformations c = new CardsInformations("1.1", "megamassue", "5", "0", "electric", "");
            player1.RemoveEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "", "electric");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "", "electric");
            player1.RemoveEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CardsInformations c = new CardsInformations("1.3", "megaboubou", "8", "8", "ice", "electric");
            player1.AddEquipment(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CardsInformations c = new CardsInformations("1.3", "megaboubou", "8", "8", "ice", "electric");
            player1.RemoveEquipment(c);
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            player1.ScanAdventurer("1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            player1.ScanAdventurer("2");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
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

        player1.ScanAdventurer("5");
        player2.ScanAdventurer("6");

        Stats();
    }

    public void Stats()
    {
        state = State.STATS;
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
        state = State.FIGHT;
        StartCoroutine(_Fight());        
    }

    public IEnumerator _Fight()
    {
        //START FIGHT
        player1.DisplayUIBeforeFight(1); //to the left
        player2.DisplayUIBeforeFight(-1);//to the right       
        monsterUi.transform.DOMoveY(monsterUi.transform.position.y + 5, 0.5f);
        monsterManager.monsterPreview.transform.DORotate(Quaternion.identity.eulerAngles + Vector3.up*90f, 1f);
        textFight.gameObject.transform.DOLocalMoveY(textFight.transform.position.y - 2f, 1).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            Camera.main.transform.DOLocalMoveY(cameraPositionFight.y, 0.5f).SetEase(Ease.OutExpo);
            Camera.main.transform.DOLocalMoveZ(cameraPositionFight.z, 0.5f).SetEase(Ease.OutExpo);
        });

        yield return new WaitForSeconds(3);

        monsterUi.transform.DOMoveY(monsterUi.transform.position.y - 5, 0.5f);

        //PLAYER1 FIGHT
        player1.DisplayUIFight();
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x - 4.5f, 1f);
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, -240, 0), 1f);

        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(monsterManager.DisplayRandomAtk());
        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(StartFightPlayer1());

        //player1.UnshowUIFight();

        yield return new WaitForSeconds(2);
        monsterManager.SetMonsterStats(monsterManager.monsterStats);
        //PLAYER2 FIGHT
        player2.DisplayUIFight();
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x + 4.5f, 1f);
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, 60, 0), 1f);

        yield return StartCoroutine(StartFightPlayer2());

        //player2.UnshowUIFight();

        yield return new WaitForSeconds(2);

        EndFight();

        yield return null;
    }

    public IEnumerator StartFightPlayer1()
    {
        bool b = false;
        player1.atkUI.GetComponent<RectTransform>().DOShakeAnchorPos(2, 15, 100);
        yield return new WaitForSeconds(1.8f);
        player1.atkUI.GetComponent<RectTransform>().DOMove(monsterManager.textDefMonsterStat.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2,LoopType.Yoyo).OnStepComplete(() => {
            if (!b)
            {
                b = true;
                monsterManager.def -= player1.playerATKTotal;
                monsterManager.UpdateUIMonster();
                monsterManager.textDefMonsterStat.GetComponent<RectTransform>().DOScale(monsterManager.textDefMonsterStat.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            }
        });
        yield return new WaitForSeconds(1f);

        if (monsterManager.def > 0)
        {
            //CHANCE DE CRIT SA MERE SALLLLLLLEEEEEEEEEEEEEEEEEE
        }
        else if (monsterManager.def <= 0)
        {
            //ATK NORMALE
        }

        b = false;
        monsterManager.atkUI.GetComponent<RectTransform>().DOShakeAnchorPos(2, 15, 100);
        yield return new WaitForSeconds(1.8f);
        monsterManager.atkUI.GetComponent<RectTransform>().DOMove(player1.defUI.transform.position, 0.5f).SetEase(Ease.InBack).SetLoops(2, LoopType.Yoyo).OnStepComplete(() => {
            if (!b)
            {
                b = true;
                player1.playerDEFTotal -= monsterManager.atk;
                player1.UpdateStatsUIPlayer();
                player1.textDefPlayerStats.GetComponent<RectTransform>().DOScale(player1.textDefPlayerStats.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            }
        });

        yield return null;
    }

    public IEnumerator StartFightPlayer2()
    {
        yield return null;
    }

    public void EndFight()
    {
        turn += 1;
        StartCoroutine(_EndFight());
        ExcelManager.Instance.EmptyCardsAfterCombat();
    }

    private IEnumerator _EndFight()
    {
        monsterManager.DestroyMonster();

        textEndFightGold.text = " \r Get your " + monsterManager.loot + " golds !";

        DOTween.To(() => textEndFightGold.fontSize, x => textEndFightGold.fontSize = x, 60, 1f).SetDelay(0.5f).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo);

        Camera.main.transform.DOMove(cameraPositionStats, 1f);

        yield return new WaitForSeconds(3);
        Stats();
    }

}

public enum Element
{
    FIRE,
    ICE,
    ELECTRIC,
    NULL,
}
