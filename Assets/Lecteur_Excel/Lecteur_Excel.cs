using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecteur_Excel : MonoBehaviour
{
    [ExecuteInEditMode]
    //Test Scanning

    public string idFictif;


    //
    int lengtOfTheArrayOfCard;
    public string[] data;
    string[] row;
    [SerializeField]
    public List<CardsInformations> cardsInfos = new List<CardsInformations>();
    // Start is called before the first frame update
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
            Debug.Log(q.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetInfosOfTheCard(idFictif);
        }
    }

    public void GetInfosOfTheCard (string idOfTheScannedCard)
    {
        for (int i =0; i<cardsInfos.Count -1; i++)
        {
            if (idOfTheScannedCard == cardsInfos[i].id)
            {
                print("ID : " + cardsInfos[i].id + "; Name : " + cardsInfos[i].name + "; Damage : " + cardsInfos[i].damage + "; Armor : " + cardsInfos[i].armor + ";");
                return;
            }
        }
        print("There is no card with this ID: " + idOfTheScannedCard);
       
    }
}
