using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [HideInInspector] public GameObject monster1;
    [HideInInspector] public GameObject monster2;

    public GameObject fightParticle;

    public GameObject player1;
    public GameObject player2;

    private int cptMonstersDead = 0;
    private string playerDeadName;
    private bool playerDead;

    private void Update()
    {
        if (playerDead)
        {
            playerDead = false;
           GameManager.Instance.DisplayEndGame(playerDeadName);
        }
        if (cptMonstersDead == 2) //all monsters are dead 
        {
            cptMonstersDead = 0;
            GameManager.Instance.EndFight();
        }
    }

    public void StartFight()
    {
        cptMonstersDead = 0;
        StartCoroutine(Fight1v1(player1, monster1));
        StartCoroutine(Fight1v1(player2, monster2));
    }

    IEnumerator Fight1v1(GameObject player, GameObject monster)
    {
        bool boolBreak = false;
        Vector3 playerPos = player.transform.position;
        Vector3 monsterPos = monster.transform.position;
        Vector3 collisionPoint = (player.transform.position + monster.transform.position) / 2;
        while (true)
        {
            yield return new WaitForSeconds(1);
            //SetLoops(2, LoopType.Yoyo).
            player.transform.DOMove(collisionPoint, 1f).SetEase(Ease.InQuart).OnComplete(() => {
                Instantiate(fightParticle, collisionPoint, Quaternion.identity);
                int randomAtk = Random.Range(monster.GetComponent<Monster>().monsterMinATK, monster.GetComponent<Monster>().monsterMaxATK);
                player.GetComponent<Player>().TakeDamagePlayer(randomAtk);
                if (player.GetComponent<Player>().playerHP <= 0)
                {
                    player.GetComponent<Player>().Die();
                    playerDead = true;
                    playerDeadName = player.GetComponent<Player>().gameObject.name;
                    boolBreak = true;
                }
            });
            
            monster.transform.DOMove(collisionPoint, 1f).SetEase(Ease.InQuart).OnComplete(() => {
                monster.GetComponent<Monster>().TakeDamageMonster(player.GetComponent<Player>().playerATK);
                if (monster.GetComponent<Monster>().monsterHP <= 0)
                {
                    monster.GetComponent<Monster>().Die();
                    cptMonstersDead++;
                    boolBreak = true;
                }
            }); 

            monster.transform.DOMove(monsterPos, 1f).SetEase(Ease.InQuart).SetDelay(1);
            player.transform.DOMove(playerPos, 1f).SetEase(Ease.InQuart).SetDelay(1);

            yield return new WaitForSeconds(1);

            if (boolBreak)
            {
                yield break;
            }                        
        }       
    }
}
