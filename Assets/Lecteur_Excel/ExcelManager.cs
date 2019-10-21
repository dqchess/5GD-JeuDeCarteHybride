using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelManager : MonoBehaviour
{

    int lengtOfTheArrayOfCard;
    public int MaxStuff = 3;
    public string[] data;
    string[] row;
    [SerializeField]
    public List<CardsInformations> cardsInfos = new List<CardsInformations>();
    public List<CardsInformations> cardsScannedPlayerOne = new List<CardsInformations>();
    public List<CardsInformations> cardsScannedPlayerTwo = new List<CardsInformations>();
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
                ci.damageElement = row[4];
                ci.armorElement = row[5];

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

    public bool IsMyCardScannedPlayerOne(string id)
    {
        bool foundInCardsInfos = false;

        for (int i = 0; i < cardsScannedPlayerOne.Count; i++)
        {
            if (id == cardsScannedPlayerOne[i].id)
            {
                if (cardsScannedPlayerOne.Count == MaxStuff)
                {
                    //TropDeCarteScanner
                    print("ERROR 2 : Too many scanned cards for player 1");
                }
                foundInCardsInfos = false;
                return foundInCardsInfos;
            }
        }
        for (int i =0; i< cardsInfos.Count; i++)
        {
            if (id == cardsInfos[i].id)
            {
                foundInCardsInfos = true;
                return foundInCardsInfos;
            }
        }

        return foundInCardsInfos;
    }

    public bool IsMyCardScannedPlayerTwo(string id)
    {
        bool foundInCardsInfos = false;

        for (int i = 0; i < cardsScannedPlayerTwo.Count; i++)
        {
            if (id == cardsScannedPlayerTwo[i].id)
            {
                if (cardsScannedPlayerTwo.Count == MaxStuff)
                {
                    //TropDeCarteScanner
                    print("ERROR 3 : Too many scanned cards for player 2");
                }
                foundInCardsInfos = false;
                return foundInCardsInfos;
            }
        }
        for (int i = 0; i < cardsInfos.Count; i++)
        {
            if (id == cardsInfos[i].id)
            {
                foundInCardsInfos = true;
                return foundInCardsInfos;
            }
        }

        return foundInCardsInfos;
    }




    public CardsInformations GetInfosOfTheCard (string idOfTheScannedCard, int player)
    {
        CardsInformations c = new CardsInformations();
        bool cardAlreadyFound = false;
        for (int i =0; i<cardsInfos.Count; i++)
        {
            if (idOfTheScannedCard == cardsInfos[i].id)
            {
                c = cardsInfos[i];
                cardAlreadyFound = true;
                if (player == 1)
                {
                    cardsScannedPlayerOne.Add(c);
                }
                else
                {
                    cardsScannedPlayerTwo.Add(c);
                }
                cardsInfos.RemoveAt(i);

                break;
            }           
        }

        if (cardAlreadyFound == false)
        {
            if (player == 1)
            {
                for (int i = 0; i < cardsScannedPlayerOne.Count; i++)
                {
                    if (idOfTheScannedCard == cardsScannedPlayerOne[i].id)
                    {
                        c = cardsScannedPlayerOne[i];
                        cardsInfos.Add(c);
                        cardsScannedPlayerOne.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < cardsScannedPlayerTwo.Count; i++)
                {
                    if (idOfTheScannedCard == cardsScannedPlayerTwo[i].id)
                    {
                        c = cardsScannedPlayerTwo[i];
                        cardsInfos.Add(c);
                        cardsScannedPlayerTwo.RemoveAt(i);
                    }
                }
            }
        }
        return c;  
    }
    
    //A appeller lors de la fin du combat pour vider les nouvelles cartes
    public void EmptyCardsAfterCombat()
    {
        cardsScannedPlayerOne.Clear();
        cardsScannedPlayerTwo.Clear();
    }
}
