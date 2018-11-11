using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour, IHealth {

    private float m_PlayerHealth;

    private float m_MaxPlayerHealth;

    private bool m_IsDeathFunctionCalled = false;

    //Calls the Ragdoll spawning function in unit controller
    public UnityEvent OnDeath;

    private Unit m_DamageSourceUnit;

#region IHealth

    public float Current { get { return m_PlayerHealth; } }
    public float Max { get { return m_MaxPlayerHealth; } }
    public float Percent { get { return m_PlayerHealth / m_MaxPlayerHealth; } }
    public bool IsAlive { get { return m_PlayerHealth > 0.0f; } }
    public bool IsDead { get { return m_PlayerHealth <= 0.0f; } }
    public void Damage(IDamageInfo damageInfo)
    {
        m_PlayerHealth = m_PlayerHealth - damageInfo.Amount >= 0 ? m_PlayerHealth - damageInfo.Amount : 0;
        m_DamageSourceUnit = damageInfo.Source != null ? (Unit)damageInfo.Source : null;
    }
    public void SetStats(IHealthStatsInfo stats)
    {
        m_MaxPlayerHealth = stats.Max;
        m_PlayerHealth = stats.Current;
        m_IsDeathFunctionCalled = false;
    }
    #endregion
    private void Start()
    {
        if (gameObject.GetComponent<UnitController>())
        {
            OnDeath.AddListener(delegate { gameObject.GetComponent<UnitController>().OnUnitDeath(); });
        }
    }

    private void Update()
    {
        if (IsDead && !m_IsDeathFunctionCalled)
        {
            OnDeath.Invoke();
            if (m_DamageSourceUnit)
            {
                m_DamageSourceUnit.GetComponent<Player>().Gold += 2;
            }
            m_IsDeathFunctionCalled = true;
        }
    }
}
