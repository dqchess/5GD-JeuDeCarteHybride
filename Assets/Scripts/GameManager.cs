using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public MonsterManager monsterManager;
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

    public Player[] players;

    [HideInInspector] public enum State { STATS, FIGHT };
    [HideInInspector] public State state;

    public GameObject panelStats;
    public GameObject panelFight;
    public GameObject panelVictory;
    public TMP_Text textVictory;

    private void Start()
    {
        DisplayStats();
    }

    public void DisplayVictory(string player)
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
    }

    public void DisplayStats()
    {
        state = State.STATS;
        monsterManager.InstantiateMonsters();
        foreach(Player p in players)
        {
            p.ResetStats();
        }
        panelStats.SetActive(true);
        panelFight.SetActive(false);
    }

    public void DisplayFight()
    {
        state = State.FIGHT;
        foreach (Player p in players)
        {
            p.StartFight();
        }
        panelStats.SetActive(false);
        panelFight.SetActive(true);
        monsterManager.StartFight();
    }
}
