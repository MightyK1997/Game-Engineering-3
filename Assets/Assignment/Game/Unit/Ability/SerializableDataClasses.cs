using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//List of all Classes for which we are retrieving data from JSON Files.
[System.Serializable]
public class TeleportDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_MaxDistance;
}

[System.Serializable]
public class FireballDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_ProjectileSpeed;
    public float m_ProjectileDuration;
    public float m_projectileSize;
    public float m_CollisionDamage;
    public float m_CollisionForce;
}

[System.Serializable]
public class MeteorDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_ImpactDamage;
    public float m_RollDamage;
    public float m_FallTime;
    public float m_ImpactForce;
    public float m_ImpactRange;
    public float m_RollSpeed;
    public float m_RollDuration;
}

[System.Serializable]
public class StealthDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_AbilityDuration;
}

[System.Serializable]
public class GravityDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_InnerRadius;
    public float m_OuterRadius;
    public float m_InnerForce;
    public float m_OuterForce;
    public float m_SetupTime;
    public float m_Duration;
    public float m_CastRange;
}

[System.Serializable]
public class BlastDetails
{
    public int m_Level;
    public float m_CostToUpgrade;
    public float CooldownTime;
    public float CastTime;
    public float m_InnerRadius;
    public float m_OuterRadius;
    public float m_InnerForce;
    public float m_OuterForce;
    public float m_InnerDamage;
    public float m_OuterDamage;
}

[System.Serializable]
public class Levels
{
    public int FireballLevel;
    public int TeleportLevel;
    public int BlastLevel;
    public int MeteorLevel;
    public int GravityLevel;
    public int StealthLevel;
}