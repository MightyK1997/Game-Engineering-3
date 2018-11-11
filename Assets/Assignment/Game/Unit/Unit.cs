using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Unit : MonoBehaviour, IUnit, IDamageSource {
    [SerializeField]
    private Health m_UnitHealth;
    [SerializeField]
    private string m_UnitName;
    [SerializeField]
    private Player m_UnitOwner;
    [SerializeField]
    private UnitController m_UnitController;

#region IUnit
    public IHealth Health { get { return m_UnitHealth; } }
    public string Name { get { return m_UnitName; } }
    public Player Owner { get { return m_UnitOwner; } }
    public IUnitController Controller { get { return m_UnitController; } }
    #endregion

    //This is a listener from the UI to stop the player from moving when using abilities.
    public void StopMovement()
    {
        m_UnitController.StopMovement();
    }

    //Listener to move the player again
    public void StartMovement()
    {
        m_UnitController.StartMovement();
    }

}
