using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public string monsterName;
    public int monsterMinATK;
    public int monsterMaxATK;
    public int monsterHP;
    public int monsterLoot;
    public float monsterHonor;
    public Element forceAttackElement = Element.NULL;
    public Element forceDefenseElement = Element.NULL;
}
