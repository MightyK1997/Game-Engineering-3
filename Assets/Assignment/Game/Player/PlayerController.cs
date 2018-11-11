using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, IPlayerController {

    [SerializeField]
    private Player m_Player;

    [SerializeField]
    private Unit[] m_allUnitControllers;

    #region IPlayerController
    public IPlayer Player { get { return m_Player; } }
    public List<IUnit> Units { get { return new List<IUnit>(m_allUnitControllers); } }
#endregion

}
