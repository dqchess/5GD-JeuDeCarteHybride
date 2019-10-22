using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class Adventurer : MonoBehaviour
{
    public int id;
    public int hp;
    public int level;
    public float honorRatio;
    public float points;

    public TMP_Text textHp;
    public TMP_Text textLevel;
    public TMP_Text textpoints;

    private void Start()
    {
        UpdateUIAdventurer();
    }

    public void UpdateUIAdventurer()
    {
        textHp.text = hp.ToString();
        textLevel.text = level.ToString();
        textpoints.text = points.ToString();
    }
}
