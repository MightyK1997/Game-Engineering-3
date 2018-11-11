using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System;

public static class GlobalCooldowns
{
    public static BlastDetails g_BlastDetails = new BlastDetails();

    private static BlastArrayClass b;
    private static FireballArrayClass f;
    private static TeleportArrayClass t;
    private static StealthArrayClass s;
    private static MeteorArrayClass m;
    private static GravityArrayClass g;

    public static void Start()
    {
        b = JsonUtility.FromJson<BlastArrayClass>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/BlastData.json"));
        t = JsonUtility.FromJson<TeleportArrayClass>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/TeleportData.json"));
        f = JsonUtility.FromJson<FireballArrayClass>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/FireballData.json"));
        s = JsonUtility.FromJson<StealthArrayClass>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/StealthData.json"));
        m = JsonUtility.FromJson<MeteorArrayClass>(File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/MeteorData.json"));
        g = JsonUtility.FromJson<GravityArrayClass>(
            File.ReadAllText(Application.dataPath + "/Assignment/Game/Data/GravityData.json"));
    }

    public static BlastDetails GetBlastDetailsForLevel(int level)
    {
        if (level <= b.m_AllBlastLevels.Length)
        {
            return b.m_AllBlastLevels[level - 1];
        }
        return null;
    }
    public static FireballDetails GetFireballDetailsForLevel(int level)
    {
        if (level <= f.m_AllFireballLevels.Length)
        {
            return f.m_AllFireballLevels[level - 1];
        }
        return null;
    }
    public static TeleportDetails GetTeleportDetailsForLevel(int level)
    {
        if (level <= t.m_AllTeleportLevels.Length)
        {
            return t.m_AllTeleportLevels[level - 1];
        }
        return null;
    }

    public static MeteorDetails GetMeteorDetailsDFForLevel(int level)
    {
        if (level <= m.m_AllMeteorLevels.Length)
        {
            return m.m_AllMeteorLevels[level - 1];
        }
        return null;
    }

    public static StealthDetails GetStealthDetailsForLevel(int level)
    {
        if (level <= s.m_AllStealthLevels.Length)
        {
            return s.m_AllStealthLevels[level - 1];
        }
        return null;
    }

    public static GravityDetails GetGravityDetailsForLevel(int level)
    {
        if (level <= g.m_AllGravityLevels.Length)
        {
            return g.m_AllGravityLevels[level - 1];
        }
        return null;
    }
}

[Serializable]
public class BlastArrayClass
{
    public BlastDetails[] m_AllBlastLevels = new BlastDetails[]{};
}

[Serializable]
public class FireballArrayClass
{
    public FireballDetails[] m_AllFireballLevels = new FireballDetails[]{};
}

[Serializable]
public class TeleportArrayClass
{
    public TeleportDetails[] m_AllTeleportLevels = new TeleportDetails[]{};
}

[Serializable]
public class MeteorArrayClass
{
    public MeteorDetails[] m_AllMeteorLevels = new MeteorDetails[]{};
}

[Serializable]
public class StealthArrayClass
{
    public StealthDetails[] m_AllStealthLevels = new StealthDetails[]{};
}

[Serializable]
public class GravityArrayClass
{
    public GravityDetails[] m_AllGravityLevels = new GravityDetails[]{};
}