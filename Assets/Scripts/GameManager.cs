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

    [Header("Stuff")]
    public Player player1;
    public Player player2;
    public MonsterManager monsterManager;
    public FightManager fightManager;

    [HideInInspector] public Vector3 cameraPositionStats;
    [HideInInspector] public Vector3 cameraRotationStats; 

    [HideInInspector] public Vector3 cameraPositionFight;
    [HideInInspector] public Vector3 cameraRotationFight;


    [Header("Panel / UI")]
    public GameObject panelStats;
    public GameObject panelFight;
    public GameObject panelVictory;
    public TMP_Text textVictory;
    public TMP_Text textFight;
    public TMP_Text textEndFight;
    public TMP_Text textEndFightGold;
    public GameObject cardAtkPrefab;
    public GameObject cardDefPrefab;
    public GameObject cardMixtPrefab;
    public GameObject adventurer;

    [Header("Particles")]
    public GameObject despawnMonster;
    public GameObject spawnMonster;

    [Header("Environment")]
    public GameObject plane;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CardsInformations c = new CardsInformations("1.1","megaboubou","5","0","");
            player1.AddStatsPlayer(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CardsInformations c = new CardsInformations("1.1", "megaboubou", "5", "0", "");
            player1.RemoveStatsPlayer(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "");
            player1.AddStatsPlayer(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            CardsInformations c = new CardsInformations("1.2", "megaboubou", "0", "5", "");
            player1.RemoveStatsPlayer(c);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            player1.ScanAdventurer("1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player1.ScanAdventurer("2");
        }

    }

    private void Start()
    {
        state = State.STATS;

        cameraPositionFight = new Vector3(0, 7, -11);
        cameraRotationFight = new Vector3(32, 0, 0);

        cameraPositionStats = new Vector3(0, 4.5f, -11);
        cameraRotationStats = new Vector3(7, 0, 0);

        textFight.fontSize = 0;

        DisplayStats();
    }


    public void DisplayStats()
    {
        StartCoroutine(Stats());
    }

    public void DisplayUIStats()
    {
        //faire revenir l'UI parti
        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, cameraPositionStats, 2);
        DOTween.To(() => Camera.main.transform.rotation, x => Camera.main.transform.rotation = x, cameraRotationStats, 2);
    }

    private IEnumerator Stats()
    {
        DisplayUIStats();

        monsterManager.InstantiateMonsterStats();
        state = State.STATS;

        player1.ResetStats();
        player2.ResetStats();

        panelStats.SetActive(true);

        plane.transform.DOScale(Vector3.one, 1f);

        yield return new WaitForEndOfFrame();    
    }

    public void DisplayFight()
    {
        StartCoroutine(Fight());        
    }

    public void DisplayUIFight()
    {
        DOTween.To(() => textFight.fontSize, x => textFight.fontSize = x, 150, 0.7f).SetEase(Ease.OutBounce);
        //DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, cameraPositionFight, 2);
        //DOTween.To(() => Camera.main.transform.rotation, x => Camera.main.transform.rotation = x, cameraRotationFight, 2);
        //Instantiate(despawnMonster, monsterManager.monsterPreview.transform.position, Quaternion.identity);
        //Instantiate(spawnMonster, monsterManager.monster1.transform.position, Quaternion.identity);
        //Instantiate(spawnMonster, monsterManager.monster2.transform.position, Quaternion.identity);
        plane.transform.DOScale(new Vector3(2.75f, 2.75f, 2.75f), 1f);
    }

    private IEnumerator Fight()
    {
        DisplayUIFight();
        
        state = State.FIGHT;       

        //anim monstre attaque

        yield return new WaitForSeconds(2);

        player1.StartFight();
        player2.StartFight();

        textFight.fontSize = 0;

        fightManager.StartFight();

        yield return null;
    }

    public void EndFight()
    {
        StartCoroutine(EndFightCoroutine());
    }

    private void DisplayUIEndFight()
    {
        textEndFightGold.text = " \r Get your " + monsterManager.loot + " golds !";

        DOTween.To(() => textEndFight.fontSize, x => textEndFight.fontSize = x, 93, 0.7f).SetEase(Ease.OutBounce);
        DOTween.To(() => textEndFight.fontSize, x => textEndFight.fontSize = x, 0, 0.7f).SetDelay(3f);

        DOTween.To(() => textEndFightGold.fontSize, x => textEndFightGold.fontSize = x, 60, 0.7f).SetDelay(0.5f).SetEase(Ease.OutBounce);
        DOTween.To(() => textEndFightGold.fontSize, x => textEndFightGold.fontSize = x, 0, 0.7f).SetDelay(3f);
    }

    private IEnumerator EndFightCoroutine()
    {

        DisplayUIEndFight();

        yield return new WaitForSeconds(3);
        DisplayStats();
    }

    public void DisplayEndGame(string player)
    {
        StopAllCoroutines();
        StartCoroutine(EndGame(player));
    }

    private IEnumerator EndGame(string player)
    {

        //DOTween.To(() => panelFight.transform.localScale, x => panelFight.transform.localScale = x, new Vector3(5, 5, 5), 0.5f).OnComplete(() => panelFight.SetActive(false));

        yield return new WaitForSeconds(3f);

        panelVictory.SetActive(true);
        DOTween.To(() => panelVictory.transform.localScale, x => panelVictory.transform.localScale = x, new Vector3(5,5,5), 0.7f).SetEase(Ease.OutBounce).From();

        if (player.Contains("1") && player.Contains("2"))
        {
            textVictory.text = "You're both dead !";
        }
        else if (player.Contains("1"))
        {
            textVictory.text = "Player 2 win !";
        }
        else if (player.Contains("2"))
        {
            textVictory.text = "Player 1 win !";
        }

        yield return null;
    }
}
