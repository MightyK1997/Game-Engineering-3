using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Island : MonoBehaviour {
    [SerializeField]
    private GameObject m_Island;
    [SerializeField]
    private Game m_Game;
    private float radius = 50.0f;

    public UnityEvent WakeEvent;

    private bool wake = false;

    private float m_TimeDelta = 0.0f;
    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
        }
    }

    private void Start()
    {
        WakeEvent.AddListener(delegate {m_Game.WakeUnits();});
    }

    private void Update()
    {
        m_TimeDelta += Time.deltaTime;
        if (m_TimeDelta >= 5.0f)
        {
            radius -= Time.deltaTime;
            if(!wake) WakeEvent.Invoke();
            UpdateRadius(radius);
        }
    }

    public void UpdateRadius(float i_Radius)
    {
        radius = i_Radius;
        m_Island.transform.localScale = new Vector3(i_Radius, m_Island.transform.localScale.y, i_Radius);
    }

    public void ResetIsland()
    {
        radius = 50.0f;
        UpdateRadius(radius);
        m_TimeDelta = 0.0f;
        wake = false;
    }
}