using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelManager : MonoBehaviour
{

    int lengtOfTheArrayOfCard;

    public string[] data;
    string[] row;
    [SerializeField]
    public List<CardsInformations> cardsInfos = new List<CardsInformations>();
    public List<CardsInformations> cardsScanned = new List<CardsInformations>();
    public List<int> positionOfCardsScanned = new List<int>();

    private static ExcelManager _instance;
    public static ExcelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ExcelManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }


    void Start()
    {
        TextAsset cardsData = Resources.Load<TextAsset>("cardsdata");

        data = cardsData.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length - 1; i++)
        {
            row = data[i].Split(new char[] { ';' });

            if (row[1] != "")
            {
                CardsInformations ci = new CardsInformations();

                ci.id = row[0];
                ci.name = row[1];
                ci.damage = row[2];
                ci.armor = row[3];
                ci.specialStat = row[4];

                cardsInfos.Add(ci);
            }
        }

        foreach (CardsInformations q in cardsInfos)
        {
            //Debug.Log(q.name);
        }
    }

    void Update()
    {

    }

    public bool IsMyCardScanned(string id)
    {
        bool foundInCardsInfos = false;

        for (int i =0; i< cardsInfos.Count; i++)
        {
            if (id == cardsInfos[i].id)
            {
                foundInCardsInfos = true;
                return foundInCardsInfos;
            }
        }

        for (int i = 0; i < cardsScanned.Count; i++)
        {
            if (id == cardsScanned[i].id)
            {
                foundInCardsInfos = false;
                return foundInCardsInfos;
            }
        }
        return foundInCardsInfos;
    }

    public CardsInformations GetInfosOfTheCard (string idOfTheScannedCard)
    {
        CardsInformations c = new CardsInformations();
        bool cardAlreadyFound = false;
        for (int i =0; i<cardsInfos.Count; i++)
        {
            if (idOfTheScannedCard == cardsInfos[i].id)
            {
                c = cardsInfos[i];
                cardAlreadyFound = true;
                cardsScanned.Add(c);
                cardsInfos.RemoveAt(i);

                break;
            }           
        }

        if (cardAlreadyFound == false)
        {
            for (int i = 0; i < cardsScanned.Count; i++)
            {
                if (idOfTheScannedCard == cardsScanned[i].id)
                {
                    c = cardsScanned[i];
                    cardsInfos.Add(c);
                    cardsScanned.RemoveAt(i);

                }
            }
        }
        return c;  
    }
}
