using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float m_MaxHealth;

    private Health m_ObstacleHealth;
    private void Start()
    {
        m_ObstacleHealth = gameObject.GetComponent<Health>();
        m_ObstacleHealth.SetStats(new HealthStatsInfo {Current = m_MaxHealth, Max = m_MaxHealth });
    }

    private void Update()
    {
        if (m_ObstacleHealth.IsDead)
        {
            Destroy(this.gameObject);
        }
    }
}
