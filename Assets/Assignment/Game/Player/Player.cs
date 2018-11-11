using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IPlayer {

    [SerializeField]
    private string m_PlayerName;
    [SerializeField]
    private Color m_PlayerColor;
    [SerializeField]
    private bool m_PlayerIsLocal;

    private float m_PlayerGold = 10.0f;

    [SerializeField] private GameObject m_MiniMapObject;

#region IPlayer
    public Color Color { get { return m_PlayerColor; } }
    public bool IsLocal { get { return m_PlayerIsLocal; } }
    public string Name { get { return m_PlayerName; } }
    public float Gold
    {
        get { return m_PlayerGold; }
        set { m_PlayerGold = value; }
    }
#endregion

    private void Start()
    {
        m_MiniMapObject.GetComponent<Renderer>().material.color = m_PlayerColor;
    }
}
