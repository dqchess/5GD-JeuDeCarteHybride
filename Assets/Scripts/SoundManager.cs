using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using EventInstance = FMOD.Studio.EventInstance;
using RuntimeManager = FMODUnity.RuntimeManager;

public class SoundManager : MonoBehaviour
{
    [Header("FMOD Evenement")]

    /*[EventRef]
    public string gameBegin;*/
    [EventRef]
    public string monsterAppearance;
    [EventRef]
    public string negotiationBegin;
    [EventRef]
    public string scanPositif;
    [EventRef]
    public string scanNegatif;
    [EventRef]
    public string attachStuff;
    [EventRef]
    public string deattachStuff;
    [EventRef]
    public string validationStuff;
    [EventRef]
    public string endOfNegotiation;
    [EventRef]
    public string lanchFight;
    [EventRef]
    public string livingCrowd;
    [EventRef]
    public string monsterCry;
    [EventRef]
    public string attack;
    [EventRef]
    public string damageRecieve;
    [EventRef]
    public string fireDamage;
    [EventRef]
    public string iceDamage;
    [EventRef]
    public string electricDamage;
    [EventRef]
    public string idleMonster;
    [EventRef]
    public string deadMonsterCry;
    [EventRef]
    public string victoryCry;
    [EventRef]
    public string collectGold;
    [EventRef]
    public string levelUping;
    [EventRef]
    public string monsterVictoryCry;
    [EventRef]
    public string deathOfGladiator;
    [EventRef]
    public string losingCrowd;
    [EventRef]
    public string endOfFight;
    /*[EventRef]
    public string endOfGame;*/

    //private EventInstance gameBeginInstance;
    private EventInstance monsterAppearanceInstance;
    private EventInstance negotiationBeginInstance;
    private EventInstance scanPositifInstance;
    private EventInstance scanNegatifInstance;
    private EventInstance attachStuffInstance;
    private EventInstance deattachStuffInstance;
    private EventInstance validationStuffInstance;
    private EventInstance endOfNegotiationInstance;
    private EventInstance lanchFightInstance;
    private EventInstance livingCrowdInstance;
    private EventInstance monsterCryInstance;
    private EventInstance attackInstance;
    private EventInstance damageRecieveInstance;
    private EventInstance fireDamageInstance;
    private EventInstance iceDamageInstance;
    private EventInstance electricDamageInstance;
    private EventInstance idleMonsterInstance;
    private EventInstance deadMonsterCryInstance;
    private EventInstance victoryCryInstance;
    private EventInstance collectGoldInstance;
    private EventInstance levelUpingInstance;
    private EventInstance monsterVictoryCryInstance;
    private EventInstance deathOfGladiatorInstance;
    private EventInstance losingCrowdInstance;
    private EventInstance endOfFightInstance;
    //private EventInstance endOfGameInstance;


    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }

        //gameBeginInstance = RuntimeManager.CreateInstance(gameBegin);
        monsterAppearanceInstance = RuntimeManager.CreateInstance(monsterAppearance);
        negotiationBeginInstance = RuntimeManager.CreateInstance(negotiationBegin);
        scanPositifInstance = RuntimeManager.CreateInstance(scanPositif);
        scanNegatifInstance = RuntimeManager.CreateInstance(scanNegatif);
        attachStuffInstance = RuntimeManager.CreateInstance(attachStuff);
        deattachStuffInstance = RuntimeManager.CreateInstance(deattachStuff);
        validationStuffInstance = RuntimeManager.CreateInstance(validationStuff);
        endOfNegotiationInstance = RuntimeManager.CreateInstance(endOfNegotiation);
        lanchFightInstance = RuntimeManager.CreateInstance(lanchFight);
        livingCrowdInstance = RuntimeManager.CreateInstance(livingCrowd);
        monsterCryInstance = RuntimeManager.CreateInstance(monsterCry);
        attackInstance = RuntimeManager.CreateInstance(attack);
        damageRecieveInstance = RuntimeManager.CreateInstance(damageRecieve);
        fireDamageInstance = RuntimeManager.CreateInstance(fireDamage);
        iceDamageInstance = RuntimeManager.CreateInstance(iceDamage);
        electricDamageInstance = RuntimeManager.CreateInstance(electricDamage);
        idleMonsterInstance = RuntimeManager.CreateInstance(idleMonster);
        deadMonsterCryInstance = RuntimeManager.CreateInstance(deadMonsterCry);
        victoryCryInstance = RuntimeManager.CreateInstance(victoryCry);
        collectGoldInstance = RuntimeManager.CreateInstance(collectGold);
        levelUpingInstance = RuntimeManager.CreateInstance(levelUping);
        monsterVictoryCryInstance = RuntimeManager.CreateInstance(monsterVictoryCry);
        deathOfGladiatorInstance = RuntimeManager.CreateInstance(deathOfGladiator);
        losingCrowdInstance = RuntimeManager.CreateInstance(losingCrowd);
        endOfFightInstance = RuntimeManager.CreateInstance(endOfFight);
        //endOfGameInstance = RuntimeManager.CreateInstance(endOfGame);



    }



    //TODO
    /// <summary>
    /// Call it at the beginning of the game
    /// </summary>
    public void StartGame()
    {
        //gameBeginInstance.start();
    }

    //TODO:
    /// <summary>
    /// Call it when you spawn a new monster
    /// </summary>
    public void MonsterSpawn()
    {
        monsterAppearanceInstance.start();
    }

    //TODO: 
    /// <summary>
    /// Call it when the fight is over or when the game begin
    /// </summary>
    public void NegociationBegin()
    {
        negotiationBeginInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when the player scan and there is not error like too many cards on the player
    /// </summary>
    public void PositifScan()
    {
        scanPositifInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when the player scan and there is an error like when the player scan a stuff card and there is already the max stuff attached to the player
    /// </summary>
    public void NegatifScan()
    {
        scanNegatifInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when the player have scan a stuff card and there is no error
    /// </summary>
    public void AttachedStuffToPlayer()
    {
        attachStuffInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when the player have scan a stuff already attach to the player
    /// </summary>
    public void DeattachedStuffToPlayer()
    {
        deattachStuffInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when the player have scan the READY card
    /// </summary>
    public void ValidationStuff()
    {
        validationStuffInstance.start();
    }

    //DONE
    /// <summary>
    /// Call it when both of the player are ready
    /// </summary>
    public void EndOfNegociation()
    {
        endOfNegotiationInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it just after the end of negociation sound
    /// </summary>
    public void LaunchFight()
    {
        lanchFightInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it just after the end of launchFight sound
    /// </summary>
    public void BeginningOfTheFight()
    {
        livingCrowdInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it the animation of fight begin
    /// </summary>
    public void MonsterSoundBase()
    {
        monsterCryInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when in the animation, the both player and monster touch each other
    /// </summary>
    public void Attack()
    {
        attackInstance.start();
    }

    //TODO:Gaby
    /// <summary>
    /// Call it when the player or the monster recieve neutral damage
    /// </summary>
    public void DamageDeal()
    {
        damageRecieveInstance.start();
    }


    //TODO:Gaby
    /// <summary>
    /// Call it when the player or the monster recieve fire damage
    /// </summary>
    public void DamageFireDeal()
    {
        fireDamageInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the player or the monster recieve ice damage
    /// </summary>
    public void DamageIceDeal()
    {
        iceDamageInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the player or the monster recieve electrical damage
    /// </summary>
    public void DamageElecDeal()
    {
        electricDamageInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the monster spawn
    /// </summary>
    public void IdleMonster()
    {
        idleMonsterInstance.start();
    }

    //TODO : Gaby
    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void StopIdleMonster()
    {
        idleMonsterInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void MonsterScream()
    {
        deadMonsterCryInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void VictoryCry()
    {
        victoryCryInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call il when the gold appears on the screen
    /// </summary>
    public void GetTheGold()
    {
        collectGoldInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the gladiator level up
    /// </summary>
    public void LevelUping()
    {
        levelUpingInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when the monster win the fight
    /// </summary>
    public void TriumphantCry()
    {
        monsterVictoryCryInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when a gladiator die
    /// </summary>
    public void DeathOfTheGladiator()
    {
        deathOfGladiatorInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it the gladiator loose the fight
    /// </summary>
    public void LoosingTheFight()
    {
        losingCrowdInstance.start();
    }

    //TODO: Gaby
    /// <summary>
    /// Call it when a fight finish
    /// </summary>
    public void EndOfTheFight()
    {
        endOfFightInstance.start();
    }

    //TODO
    /// <summary>
    /// Call it when the last monster got defeated
    /// </summary>
    public void EndOfTheGame()
    {
        //endOfGameInstance.start();
    }













}
