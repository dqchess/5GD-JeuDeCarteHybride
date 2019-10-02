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

    public CardsInformations GetInfosOfTheCard (string idOfTheScannedCard)
    {
        CardsInformations c = new CardsInformations();
        for (int i =0; i<cardsInfos.Count -1; i++)
        {
            if (idOfTheScannedCard == cardsInfos[i].id)
            {
                //Envoyer CardsInformation (CarsInfos[i])
                //print("ID : " + cardsInfos[i].id + "; Name : " + cardsInfos[i].name + "; Damage : " + cardsInfos[i].damage + "; Armor : " + cardsInfos[i].armor + ";");
                c = cardsInfos[i];
                break;
            }           
        }
        return c;  
    }
}
