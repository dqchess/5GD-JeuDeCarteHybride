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
    public FightManager fightManager;
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
    [HideInInspector] public int turn =0 ;

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
        StartCoroutine(_Stats());
    }

    public IEnumerator _Stats()
    {   
       
        Camera.main.transform.DOMove(cameraPositionStats, 1f).OnComplete(() => {
            if (turn != 0)
            {
                player1.DisplayUIStats(-1); //to the right
                player2.DisplayUIStats(1);//to the left
            }
        });

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
        player1.DisplayUIFight(1); //to the left
        player2.DisplayUIFight(-1);//to the right       
        monsterUi.transform.DOMoveY(monsterUi.transform.position.y + 5, 0.5f);
        monsterManager.monsterPreview.transform.DORotate(Quaternion.identity.eulerAngles + Vector3.up*90f, 1f);
        textFight.gameObject.transform.DOLocalMoveY(textFight.transform.position.y - 5f, 1).SetEase(Ease.OutBounce).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            Camera.main.transform.DOLocalMoveY(cameraPositionFight.y, 0.5f).SetEase(Ease.OutExpo);
            Camera.main.transform.DOLocalMoveZ(cameraPositionFight.z, 0.5f).SetEase(Ease.OutExpo);
        });

        yield return new WaitForSeconds(3);

        //PLAYER1 FIGHT
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x - 4.5f, 1f);
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, -5, 0), 1f);
        monsterUi.transform.DOMoveY(monsterUi.transform.position.y - 5, 0.5f);

        //yield return StartCoroutine(fightManager.StartFightPlayer1());
        yield return new WaitForSeconds(2);

        //END FIGHT PLAYER 1
        //RECAP FIGHT PLAYER 1

        yield return new WaitForSeconds(3);

        //PLAYER2 FIGHT
        Camera.main.transform.DOLocalMoveX(cameraPositionFight.x + 4.5f, 1f);
        monsterManager.monsterPreview.transform.DORotate(new Vector3(0, 60, 0), 1f);

        //yield return StartCoroutine(fightManager.StartFightPlayer2());
        yield return new WaitForSeconds(2);

        //END FIGHT PLAYER 2
        //RECAP FIGHT PLAYER 2

        EndFight();

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
