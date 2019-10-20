using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stuff : MonoBehaviour
{
    public RawImage stuffImage;
    public TMP_Text textValueAtk;
    public TMP_Text textValueDef;
    public TMP_Text textAtkIsDouble;
    public TMP_Text textDefIsDouble;
    public Element elementAtk;
    public Element elementDef;
    public Image imageElementAtk;
    public Image imageElementDef;


    private void Update()
    {
        //textValueAtk.gameObject.transform.DOScale(transform.localScale * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void AtkIsDouble()
    {
        Debug.Log("atkIsDouble " + gameObject);
        textAtkIsDouble.gameObject.SetActive(true);
        textAtkIsDouble.gameObject.transform.DOScale(Vector3.one * 1.5f, 0.8f).SetLoops(-1, LoopType.Yoyo);
    }

    public void DefIsDouble()
    {
        Debug.Log("defIsDouble " + gameObject);
        textDefIsDouble.gameObject.SetActive(true);
        textDefIsDouble.gameObject.transform.DOScale(Vector3.one * 1.5f, 0.8f).SetLoops(-1, LoopType.Yoyo);
    }
}
