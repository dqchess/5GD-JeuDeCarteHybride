using DG.Tweening;
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
    public bool isDead = false;

    public TMP_Text textHp;
    public TMP_Text textLevel;
    public TMP_Text textpoints;
    public GameObject isDeadPicture;

    private void Start()
    {
        UpdateUIAdventurer();
    }

    public void UpdateUIAdventurer()
    {
        if (textHp.text != hp.ToString())
        {
            //textHp.gameObject.GetComponent<RectTransform>().DOScale(textHp.gameObject.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            textHp.text = hp.ToString();
        }
        if (textLevel.text != level.ToString())
        {
            textLevel.gameObject.GetComponent<RectTransform>().DOScale(textLevel.gameObject.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            textLevel.text = level.ToString();
        }
        if (textpoints.text != points.ToString())
        {
            textpoints.gameObject.GetComponent<RectTransform>().DOScale(textpoints.gameObject.GetComponent<RectTransform>().transform.localScale * 1.5f, 0.2f).SetLoops(2, LoopType.Yoyo);
            textpoints.text = points.ToString();
        }
    }
}
