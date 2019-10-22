using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardsInformations
{
    public string id;
    public string name;
    public string damage;
    public string armor;
    public string damageElement;
    public string armorElement;

    public CardsInformations(string id, string name, string damage, string armor, string damageElement, string armorElement)
    {
        this.id = id;
        this.name = name;
        this.damage = damage;
        this.armor = armor;
        this.damageElement = damageElement;
        this.armorElement = armorElement;
    } 
    public CardsInformations() { }
}
