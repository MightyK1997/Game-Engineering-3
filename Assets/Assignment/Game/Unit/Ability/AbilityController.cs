using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class AbilityController : MonoBehaviour
{

    //This script is the controller for all the abilities that a player/unit can have.
    [SerializeField]
    private UpdateAbilities m_AbilitiesUI;
    [SerializeField]
    private Unit m_Unit;

    //This is the list of abilities that a player can have
    [SerializeField] private List<Abilities.AbilitiesEnum> m_Abilities;
    [SerializeField] private Push pushAbility;
    [SerializeField] private TeleportController m_TeleportAbility;
    [SerializeField] private BlastController m_BlastController;
    [SerializeField] private FireballCastController m_FireballController;
    [SerializeField] private MarkerScript m_Markers;
    [SerializeField] private MeteorController m_MeteorController;
    [SerializeField] private StealthController m_StealthController;
    [SerializeField] private GravityController m_GravityController;

    public Levels m_PlayerLevel;
    private Abilities.AbilitiesEnum m_CastedAbility;
    public UnityEvent m_MovementEvent;

    //Static Data for all the ability details that this unit has

    public BlastDetails s_PlayerBlastDetails;
    public FireballDetails s_PlayerFireballDetails;
    public TeleportDetails s_PlayerTeleportDetails;
    public MeteorDetails s_PlayerMeteorDetails;
    public StealthDetails s_PlayerStealthDetails;
    public GravityDetails s_PlayerGravityDetails;

    private Dictionary<Abilities.AbilitiesEnum, float> abilityLevels;


    private void OnMouseDown () {
	    m_AbilitiesUI.UpdateAbilityUI(m_Unit);
	}

    public List<Abilities.AbilitiesEnum> GetAbilities()
    {
        return m_Abilities;
    }

    public void SetAbility(Abilities.AbilitiesEnum i_Ability)
    {
        m_CastedAbility = i_Ability;
        if (i_Ability == Abilities.AbilitiesEnum.Blast)
        {
            CastAbility(null);
        }
    }

    private void Start()
    {
        m_MovementEvent.AddListener(delegate {m_Unit.StartMovement();});
        GlobalCooldowns.Start();
        GetPlayerAbilitiesData();
        abilityLevels = new Dictionary<Abilities.AbilitiesEnum, float>();
        abilityLevels.Add(Abilities.AbilitiesEnum.Blast, m_PlayerLevel.BlastLevel);
        abilityLevels.Add(Abilities.AbilitiesEnum.Stealth, m_PlayerLevel.StealthLevel);
        abilityLevels.Add(Abilities.AbilitiesEnum.Fireball, m_PlayerLevel.FireballLevel);
        abilityLevels.Add(Abilities.AbilitiesEnum.Gravity, m_PlayerLevel.GravityLevel);
        abilityLevels.Add(Abilities.AbilitiesEnum.Meteor, m_PlayerLevel.MeteorLevel);
        abilityLevels.Add(Abilities.AbilitiesEnum.Teleport, m_PlayerLevel.TeleportLevel);
    }
    public void CastAbility(Vector3? i_InputVector)
    {
        StopCasting();
        if (m_CastedAbility == Abilities.AbilitiesEnum.Push)
        {
            pushAbility.Cast();
        }
        if (m_CastedAbility == Abilities.AbilitiesEnum.Fireball)
        {
            m_Markers.AddMarkerToScreen(i_InputVector.Value);
            Vector3 targetVec = Vector3.zero;
            Vector3 targetDir = Vector3.zero;
            if (i_InputVector.HasValue)
            {
                targetDir = RotateTowardsPoint(i_InputVector.Value);
                targetVec = transform.position + new Vector3(0, 5, 0) + (targetDir.normalized * -3);
            }
            m_FireballController.Cast(targetVec, targetDir);

            m_MovementEvent.Invoke();
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Teleport)
        {
            m_Markers.AddMarkerToScreen(i_InputVector.Value);
            if (i_InputVector.HasValue)
            {
                RotateTowardsPoint(i_InputVector.Value);
                m_TeleportAbility.Cast(i_InputVector.Value);
                m_MovementEvent.Invoke();
            }
        }
        if (m_CastedAbility == Abilities.AbilitiesEnum.Blast)
        {
            m_BlastController.Cast();
            m_MovementEvent.Invoke();
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Meteor)
        {
            if (i_InputVector.HasValue)
            {
                m_MeteorController.Cast(i_InputVector.Value);
                m_MovementEvent.Invoke();
            }
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Gravity)
        {
            if (i_InputVector.HasValue)
            {
                m_GravityController.Cast(i_InputVector.Value);
                m_MovementEvent.Invoke();
            }
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Stealth)
        {
            m_StealthController.Cast();
            m_MovementEvent.Invoke();
        }
    }

    public void StopCasting()
    {
        m_Markers.RemoveMarkers();
        if (m_CastedAbility == Abilities.AbilitiesEnum.Teleport)
        {
            m_TeleportAbility.StopCast();
        }
        if (m_CastedAbility == Abilities.AbilitiesEnum.Blast)
        {
            m_BlastController.StopCast();
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Fireball)
        {
            m_FireballController.StopCast();
        }

        if (m_CastedAbility == Abilities.AbilitiesEnum.Meteor)
        {
            m_MeteorController.StopCast();
        }
        if (m_CastedAbility == Abilities.AbilitiesEnum.Gravity)
        {
            m_GravityController.StopCast();
        }
        if (m_CastedAbility == Abilities.AbilitiesEnum.Stealth)
        {
            m_StealthController.StopCast();
        }
    }

    private Vector3 RotateTowardsPoint(Vector3? i_Input)
    {
        Vector3 targetDir = transform.position - i_Input.Value;
        transform.Rotate(Vector3.up, Vector3.SignedAngle(Vector3.forward, targetDir, Vector3.up));
        return targetDir;
    }

    private void OnApplicationQuit()
    {
        File.WriteAllText(Application.dataPath + "/Assignment/Game/Data/" + GetComponent<Unit>().Name + "level.json", JsonUtility.ToJson(m_PlayerLevel));
    }

    public void GetPlayerAbilitiesData()
    {
        Debug.Log(Application.dataPath + "/Assignment/Game/Data/" + GetComponent<Unit>().Name + "level.json");
        if (File.Exists(Application.dataPath + "/Assignment/Game/Data/" + GetComponent<Unit>().Name + "level.json"))
        {
            m_PlayerLevel = JsonUtility.FromJson<Levels>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/" + GetComponent<Unit>().Name + "level.json"));
        }
        s_PlayerBlastDetails = GlobalCooldowns.GetBlastDetailsForLevel(m_PlayerLevel.BlastLevel);
        s_PlayerFireballDetails = GlobalCooldowns.GetFireballDetailsForLevel(m_PlayerLevel.FireballLevel);
        s_PlayerTeleportDetails = GlobalCooldowns.GetTeleportDetailsForLevel(m_PlayerLevel.TeleportLevel);
        s_PlayerGravityDetails = GlobalCooldowns.GetGravityDetailsForLevel(m_PlayerLevel.GravityLevel);
        s_PlayerMeteorDetails = GlobalCooldowns.GetMeteorDetailsDFForLevel(m_PlayerLevel.MeteorLevel);
        s_PlayerStealthDetails = GlobalCooldowns.GetStealthDetailsForLevel(m_PlayerLevel.StealthLevel);
    }

    public void UpdatePlayerLevel(Abilities.AbilitiesEnum? i_Ability)
    {
        if (i_Ability.HasValue)
        {
            abilityLevels[i_Ability.Value]++;

            if (i_Ability.Value == Abilities.AbilitiesEnum.Blast)
            {
                GetComponent<Player>().Gold -= s_PlayerBlastDetails.m_CostToUpgrade;
            }
            else if (i_Ability.Value == Abilities.AbilitiesEnum.Fireball)
            {
                GetComponent<Player>().Gold -= s_PlayerFireballDetails.m_CostToUpgrade;
            }
            else if (i_Ability.Value == Abilities.AbilitiesEnum.Teleport)
            {
                GetComponent<Player>().Gold -= s_PlayerTeleportDetails.m_CostToUpgrade;
            }
            else if (i_Ability.Value == Abilities.AbilitiesEnum.Gravity)
            {
                GetComponent<Player>().Gold -= s_PlayerGravityDetails.m_CostToUpgrade;
            }
            else if (i_Ability.Value == Abilities.AbilitiesEnum.Meteor)
            {
                GetComponent<Player>().Gold -= s_PlayerMeteorDetails.m_CostToUpgrade;
            }
            else if (i_Ability.Value == Abilities.AbilitiesEnum.Stealth)
            {
                GetComponent<Player>().Gold -= s_PlayerStealthDetails.m_CostToUpgrade;
            }
        }
    }
}