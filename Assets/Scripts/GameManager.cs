using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    [Header("Particles")]
    public GameObject despawnMonster;
    public GameObject spawnMonster;

    [Header("Environment")]
    public GameObject plane;

    private void Start()
    {
        cameraPositionFight = new Vector3(0, 7, -11);
        cameraRotationFight = new Vector3(32, 0, 0);

        cameraPositionStats = new Vector3(0, 4.5f, -11);
        cameraRotationStats = new Vector3(7, 0, 0);

        textFight.fontSize = 0;

        DisplayStats();
    }


    public void EndFight()
    {
        StartCoroutine(EndFightCoroutine());
    }
    private IEnumerator EndFightCoroutine()
    {
        DOTween.To(() => panelFight.transform.localScale, x => panelFight.transform.localScale = x, new Vector3(5, 5, 5), 0.5f).OnComplete(()=> panelFight.SetActive(false));
        DOTween.To(() => textEndFight.fontSize, x => textEndFight.fontSize = x, 93, 0.7f).SetEase(Ease.OutBounce);
        DOTween.To(() => textEndFight.fontSize, x => textEndFight.fontSize = x, 0, 0.7f).SetDelay(0.6f);
        
        yield return new WaitForSeconds(2);
        DisplayStats();
        panelFight.transform.localScale = Vector3.one;
    }


    public void DisplayStats()
    {
        StartCoroutine(Stats());
    }

    private IEnumerator Stats()
    {
        monsterManager.InstantiateMonsterStats();

        player1.ResetStats();
        player2.ResetStats();

        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);

        panelStats.SetActive(true);
        panelFight.SetActive(false);

        plane.transform.DOScale(Vector3.one, 1f);
        DOTween.To(() => panelStats.transform.localScale, x => panelStats.transform.localScale = x, new Vector3(5, 5, 5), 0.5f).From();
        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, cameraPositionStats, 2);
        DOTween.To(() => Camera.main.transform.rotation, x => Camera.main.transform.rotation = x, cameraRotationStats, 2);

        yield return new WaitForSeconds(2);

        yield return null;
    }

    public void DisplayFight()
    {
        StartCoroutine(Fight());        
    }

    private IEnumerator Fight()
    {
        //particles
        Instantiate(despawnMonster, monsterManager.monsterPreview.transform.position, Quaternion.identity);
        Instantiate(spawnMonster, monsterManager.monster1.transform.position, Quaternion.identity);
        Instantiate(spawnMonster, monsterManager.monster2.transform.position, Quaternion.identity);
        //fight text
        DOTween.To(() => textFight.fontSize, x => textFight.fontSize = x, 150, 0.7f).SetEase(Ease.OutBounce);

        monsterManager.InstantiateMonstersFight();

        DOTween.To(() => Camera.main.transform.position, x => Camera.main.transform.position = x, cameraPositionFight, 2);
        DOTween.To(() => Camera.main.transform.rotation, x => Camera.main.transform.rotation = x, cameraRotationFight, 2);

        DOTween.To(() => panelStats.transform.localScale, x => panelStats.transform.localScale = x, new Vector3(5 ,5, 5), 0.5f);

        player1.gameObject.SetActive(true);
        player2.gameObject.SetActive(true);
        DOTween.To(() => player1.gameObject.transform.localScale, x => player1.gameObject.transform.localScale = x, new Vector3(0, 0, 0), 1f).From();
        DOTween.To(() => player2.gameObject.transform.localScale, x => player2.gameObject.transform.localScale = x, new Vector3(0, 0, 0), 1f).From();

        plane.transform.DOScale(new Vector3(2.75f, 2.75f, 2.75f), 1f);

        yield return new WaitForSeconds(2);

        DOTween.To(() => panelFight.transform.localScale, x => panelFight.transform.localScale = x, new Vector3(5, 5, 5), 0.5f).From();

        player1.StartFight();
        player2.StartFight();

        //reset values
        textFight.fontSize = 0;
        panelStats.transform.localScale = Vector3.one;

        panelStats.SetActive(false);
        panelFight.SetActive(true);
        monsterManager.StartFight();

        //Camera.main.transform.position = cameraPositionFight;
        //Camera.main.transform.rotation = Quaternion.Euler(cameraRotationFight);
        yield return null;
    }

    public void DisplayEndGame(string player)
    {
        StopAllCoroutines();
        StartCoroutine(EndGame(player));
    }

    private IEnumerator EndGame(string player)
    {
        panelStats.SetActive(false);
        panelFight.SetActive(false);
        panelVictory.SetActive(true);
        if (player.Contains("1"))
        {
            textVictory.text = "Player 2 win !";
        }
        else if (player.Contains("2"))
        {
            textVictory.text = "Player 1 win !";
        }

        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);

        monsterManager.monster1.SetActive(false);
        monsterManager.monster2.SetActive(false);

        yield return null;
    }
}
