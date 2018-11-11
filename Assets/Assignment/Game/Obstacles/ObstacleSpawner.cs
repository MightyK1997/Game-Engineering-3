using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Obstacle;
    [SerializeField]
    private int m_NumberOfObstaclesToSpawn;

    [SerializeField] private GameObject m_spawnPoint;
    [SerializeField] private Island m_Island;

    //Spawns the set number of obstacles at random on the island
    public void SpawnObstacles()
    {
        for (var i = 0; i < m_NumberOfObstaclesToSpawn; i++)
        {
            Instantiate(m_Obstacle, new Vector3(
                m_spawnPoint.transform.position.x + Random.Range(-m_Island.Radius / 2, m_Island.Radius / 2),
                m_spawnPoint.transform.position.y,
                m_spawnPoint.transform.position.z + Random.Range(-m_Island.Radius / 2, m_Island.Radius / 2)), Quaternion.identity);
        }
    }
}
