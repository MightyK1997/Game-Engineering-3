using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour
{
    public AbilityController abilityController
    {
        get { return m_AC; }
        set { m_AC = value; }
    }

    public Unit AbilityUnit
    {
        get { return m_AbilityUnit; }
        set { m_AbilityUnit = value; }
    }

    private AbilityController m_AC;
    private float m_MaxCooldown;
    private Unit m_AbilityUnit;

    public UnityEvent m_MovementEvent;

    public float coolDown
    {
        get { return m_MaxCooldown; }
        set { m_MaxCooldown = value; }
    }
    private float m_Timer;

    private void Start()
    {
        m_Timer = m_MaxCooldown + 0.1f;
        //m_MovementEvent.AddListener(delegate { m_AbilityUnit.StopMovement();});
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        gameObject.GetComponent<Button>().interactable = m_Timer > m_MaxCooldown;
    }

    //This is called onclick for the button
    public void OnClick(Abilities.AbilitiesEnum m_Ability)
    {
        if (m_AC) m_AC.SetAbility(m_Ability);
        m_Timer = 0.0f;
        m_AbilityUnit.StopMovement();
    }
}
