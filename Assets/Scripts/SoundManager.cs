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

    [EventRef]
    public string debutDeJeu;
    [EventRef]
    public string apparitionMonstre;
    [EventRef]
    public string debutNegociation;
    [EventRef]
    public string scanPositif;
    [EventRef]
    public string scanNegatif;
    [EventRef]
    public string equipementMateriel;
    [EventRef]
    public string desequipementMateriel;
    [EventRef]
    public string validationEquipement;
    [EventRef]
    public string finDeNegociation;
    [EventRef]
    public string lancementCombat;
    [EventRef]
    public string fouleVivante;
    [EventRef]
    public string rugissementMonstre;
    [EventRef]
    public string attaque;
    [EventRef]
    public string degatsRecu;
    [EventRef]
    public string degatDeFeu;
    [EventRef]
    public string degatDeGlace;
    [EventRef]
    public string degatElectrique;
    [EventRef]
    public string ronronnementMachine;
    [EventRef]
    public string rugissementMonstreMort;
    [EventRef]
    public string criDeVictoire;
    [EventRef]
    public string recuperationDuButin;
    [EventRef]
    public string augmentationDeNiveau;
    [EventRef]
    public string rugissementTriomphant;
    [EventRef]
    public string disparitionPersonnage;
    [EventRef]
    public string hueeDeDefaite;
    [EventRef]
    public string finDuCombat;
    [EventRef]
    public string finDuJeu;

    private EventInstance debutDeJeuInstance;
    private EventInstance apparitionMonstreInstance;
    private EventInstance debutNegociationInstance;
    private EventInstance scanPositifInstance;
    private EventInstance scanNegatifInstance;
    private EventInstance equipementMaterielInstance;
    private EventInstance desequipementMaterielInstance;
    private EventInstance validationEquipementInstance;
    private EventInstance finDeNegociationInstance;
    private EventInstance lancementCombatInstance;
    private EventInstance fouleVivanteInstance;
    private EventInstance rugissementMonstreInstance;
    private EventInstance attaqueInstance;
    private EventInstance degatsRecuInstance;
    private EventInstance degatDeFeuInstance;
    private EventInstance degatDeGlaceInstance;
    private EventInstance degatElectriqueInstance;
    private EventInstance ronronnementMachineInstance;
    private EventInstance rugissementMonstreMortInstance;
    private EventInstance criDeVictoireInstance;
    private EventInstance recuperationDuButinInstance;
    private EventInstance augmentatonDeNiveauInstance;
    private EventInstance rugissementTriomphantInstance;
    private EventInstance disparitionPersonnageInstance;
    private EventInstance hueeDeDefaiteInstance;
    private EventInstance finDuCombatInstance;
    private EventInstance finDuJeuInstance;


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

        debutDeJeuInstance = RuntimeManager.CreateInstance(debutDeJeu);
        apparitionMonstreInstance = RuntimeManager.CreateInstance(apparitionMonstre);
        debutNegociationInstance = RuntimeManager.CreateInstance(debutNegociation);
        scanPositifInstance = RuntimeManager.CreateInstance(scanPositif);
        scanNegatifInstance = RuntimeManager.CreateInstance(scanNegatif);
        equipementMaterielInstance = RuntimeManager.CreateInstance(equipementMateriel);
        desequipementMaterielInstance = RuntimeManager.CreateInstance(desequipementMateriel);
        validationEquipementInstance = RuntimeManager.CreateInstance(validationEquipement);
        finDeNegociationInstance = RuntimeManager.CreateInstance(finDeNegociation);
        lancementCombatInstance = RuntimeManager.CreateInstance(lancementCombat);
        fouleVivanteInstance = RuntimeManager.CreateInstance(fouleVivante);
        rugissementMonstreInstance = RuntimeManager.CreateInstance(rugissementMonstre);
        attaqueInstance = RuntimeManager.CreateInstance(attaque);
        degatsRecuInstance = RuntimeManager.CreateInstance(degatsRecu);
        degatDeFeuInstance = RuntimeManager.CreateInstance(degatDeFeu);
        degatDeGlaceInstance = RuntimeManager.CreateInstance(degatDeGlace);
        degatElectriqueInstance = RuntimeManager.CreateInstance(degatElectrique);
        ronronnementMachineInstance = RuntimeManager.CreateInstance(ronronnementMachine);
        rugissementMonstreMortInstance = RuntimeManager.CreateInstance(rugissementMonstreMort);
        criDeVictoireInstance = RuntimeManager.CreateInstance(criDeVictoire);
        recuperationDuButinInstance = RuntimeManager.CreateInstance(recuperationDuButin);
        augmentatonDeNiveauInstance = RuntimeManager.CreateInstance(augmentationDeNiveau);
        rugissementTriomphantInstance = RuntimeManager.CreateInstance(rugissementTriomphant);
        disparitionPersonnageInstance = RuntimeManager.CreateInstance(disparitionPersonnage);
        hueeDeDefaiteInstance = RuntimeManager.CreateInstance(hueeDeDefaite);
        finDuCombatInstance = RuntimeManager.CreateInstance(finDuCombat);
        finDuJeuInstance = RuntimeManager.CreateInstance(finDuJeu);



    }



    /// <summary>
    /// Call it at the beginning of the game
    /// </summary>
    public void StartGame()
    {
        debutDeJeuInstance.start();
    }

    /// <summary>
    /// Call it when you spawn a new monster
    /// </summary>
    public void MonsterSpawn()
    {
        apparitionMonstreInstance.start();
    }

    /// <summary>
    /// Call it when the fight is over or when the game begin
    /// </summary>
    public void NegociationBegin()
    {
        debutNegociationInstance.start();
    }

    /// <summary>
    /// Call it when the player scan and there is not error like too many cards on the player
    /// </summary>
    public void PositifScan()
    {
        scanPositifInstance.start();
    }

    /// <summary>
    /// Call it when the player scan and there is an error like when the player scan a stuff card and there is already the max stuff attached to the player
    /// </summary>
    public void NegatifScan()
    {
        scanNegatifInstance.start();
    }

    /// <summary>
    /// Call it when the player have scan a stuff card and there is no error
    /// </summary>
    public void AttachedStuffToPlayer()
    {
        equipementMaterielInstance.start();
    }

    /// <summary>
    /// Call it when the player have scan a stuff already attach to the player
    /// </summary>
    public void DeattachedStuffToPlayer()
    {
        desequipementMaterielInstance.start();
    }

    /// <summary>
    /// Call it when the player have scan the READY card
    /// </summary>
    public void ValidationStuff()
    {
        validationEquipementInstance.start();
    }

    /// <summary>
    /// Call it when both of the player are ready
    /// </summary>
    public void EndOfNegociation()
    {
        finDeNegociationInstance.start();
    }

    /// <summary>
    /// Call it just after the end of negociation sound
    /// </summary>
    public void LaunchFight()
    {
        lancementCombatInstance.start();
    }

    /// <summary>
    /// Call it just after the end of launchFight sound
    /// </summary>
    public void BeginningOfTheFight()
    {
        fouleVivanteInstance.start();
    }

    /// <summary>
    /// Call it the animation of fight begin
    /// </summary>
    public void MonsterSoundBase()
    {
        rugissementMonstreInstance.start();
    }

    /// <summary>
    /// Call it when in the animation, the both player and monster touch each other
    /// </summary>
    public void Attack()
    {
        attaqueInstance.start();
    }

    /// <summary>
    /// Call it when the player or the monster recieve neutral damage
    /// </summary>
    public void DamageDeal()
    {
        degatsRecuInstance.start();
    }

    /// <summary>
    /// Call it when the player or the monster recieve fire damage
    /// </summary>
    public void DamageFireDeal()
    {
        degatDeFeuInstance.start();
    }

    /// <summary>
    /// Call it when the player or the monster recieve ice damage
    /// </summary>
    public void DamageIceDeal()
    {
        degatDeGlaceInstance.start();
    }

    /// <summary>
    /// Call it when the player or the monster recieve electrical damage
    /// </summary>
    public void DamageElecDeal()
    {
        degatElectriqueInstance.start();
    }

    /// <summary>
    /// Call it when the monster spawn
    /// </summary>
    public void IdleMonster()
    {
        ronronnementMachineInstance.start();
    }

    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void StopIdleMonster()
    {
        ronronnementMachineInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void MonsterScream()
    {
        rugissementMonstreMortInstance.start();
    }

    /// <summary>
    /// Call it when the monster die
    /// </summary>
    public void VictoryCry()
    {
        criDeVictoireInstance.start();
    }

    /// <summary>
    /// Call il when the gold appears on the screen
    /// </summary>
    public void GetTheGold()
    {
        recuperationDuButinInstance.start();
    }

    /// <summary>
    /// Call it when the gladiator level up
    /// </summary>
    public void LevelUping()
    {
        augmentatonDeNiveauInstance.start();
    }

    /// <summary>
    /// Call it when the monster win the fight
    /// </summary>
    public void TriumphantCry()
    {
        rugissementTriomphantInstance.start();
    }

    /// <summary>
    /// Call it when a gladiator die
    /// </summary>
    public void DeathOfTheGladiator()
    {
        disparitionPersonnageInstance.start();
    }

    /// <summary>
    /// Call it the gladiator loose the fight
    /// </summary>
    public void LoosingTheFight()
    {
        hueeDeDefaiteInstance.start();
    }

    /// <summary>
    /// Call it when a fight finish
    /// </summary>
    public void EndOfTheFight()
    {
        finDuCombatInstance.start();
    }

    /// <summary>
    /// Call it when the last monster got defeated
    /// </summary>
    public void EndOfTheGame()
    {
        finDuJeuInstance.start();
    }













}
