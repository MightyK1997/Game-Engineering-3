using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityScript.TypeSystem;

public class Round : IRound<RoundPhase, RoundEvent>
{

    private List<Player> m_AllPlayers;
    private List<Unit> m_AllUnits;

    private List<Player> m_AlivePlayers;
    private List<Unit> m_AliveUnits;

    private IPlayer m_Winner  = null;

    #region IRound
    public RoundPhase Phase { get { throw new NotImplementedException(); } }
    public RoundEvent OnPhaseChanged { get { throw new NotImplementedException(); } }
    public IPlayer Winner { get { return m_Winner; } }
    public List<IUnit> AliveUnits
    {
        get
        {
            List<IUnit> retList = new List<IUnit>();
            foreach (var aliveUnit in m_AliveUnits)
            {
                retList.Add(aliveUnit);
            }
            return retList;
        }
    }
    public List<IPlayer> AlivePlayers
    {
        get
        {
            List<IPlayer> retList = new List<IPlayer>();
            foreach (var alivePlayer in m_AlivePlayers)
            {
                retList.Add(alivePlayer);
            }
            return retList;
        }
    }
    public void StartRound()
    {
        m_AllUnits = new List<Unit>(GameObject.FindObjectsOfType<Unit>());
        m_AllPlayers = new List<Player>();
        m_AlivePlayers = new List<Player>();
        m_AliveUnits = new List<Unit>();
        foreach (var units in m_AllUnits)
        {
            units.Health.SetStats(new HealthStatsInfo { Current = 100, Max = 100 });
            if (units.Owner)
            {
                m_AllPlayers.Add(units.Owner);
            }
        }

        foreach (var player in m_AllPlayers)
        {
            player.Gold += 5.0f;
        }
        m_AliveUnits = m_AllUnits;
        m_AlivePlayers = m_AllPlayers;
    }
    #endregion

    //Checking if the a unit dies and removes it from the living pool. When the living units is 1, set it as winner.
    public void Update()
    {
        foreach (var unit in m_AllUnits)
        {
            if (unit.Health.IsDead)
            {
                m_AliveUnits.Remove(unit);
                if (unit.Owner) m_AlivePlayers.Remove(unit.Owner);
            }
        }
        if (m_AliveUnits.Count == 1)
        {
            foreach (var unit in m_AliveUnits)
            {
                if (unit.Owner) m_Winner = unit.Owner;
                m_Winner.Gold += 4.0f;
            }
        }
    }
}
