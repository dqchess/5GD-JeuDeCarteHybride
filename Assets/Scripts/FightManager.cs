using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [HideInInspector] public GameObject monster1;
    [HideInInspector] public GameObject monster2;

    public GameObject player1;
    public GameObject player2;

    private int cptMonstersDead = 0;

    private void Update()
    {
        if (cptMonstersDead == 2) //all monsters are dead 
        {
            cptMonstersDead = 0;
            StartCoroutine(EndFights());
        }
    }

    public void StartFight()
    {
        cptMonstersDead = 0;
        StartCoroutine(Fight1v1(player1, monster1));
        StartCoroutine(Fight1v1(player2, monster2));
    }

    IEnumerator EndFights()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.DisplayStats();
        yield break;
    }

    IEnumerator Fight1v1(GameObject player, GameObject monster)
    {
        Vector3 playerPos = player.transform.position;
        Vector3 monsterPos = monster.transform.position;
        Vector3 collisionPoint = (player.transform.position + monster.transform.position) / 2;
        while (true)
        {
            yield return new WaitForSeconds(3);
            //1s :
            //player to collisionPoint
            //monster to collisionPoint //continue whith particlesystem
            //1s :
            //player to playerPos
            //monster to monsterPos

            player.GetComponent<Player>().TakeDamagePlayer(monster.GetComponent<Monster>().monsterATK);
            monster.GetComponent<Monster>().TakeDamageMonster(player.GetComponent<Player>().playerATK);
           
            if (player.GetComponent<Player>().playerHP <= 0)
            {
                //GameManager.Instance.DisplayStats();
                yield break;
            }
            else if (monster.GetComponent<Monster>().monsterHP <= 0)
            {
                monster.GetComponent<Monster>().Die();
                cptMonstersDead++;
                yield return new WaitForSeconds(2);
                yield break;
            }                       
        }       
    }
}
