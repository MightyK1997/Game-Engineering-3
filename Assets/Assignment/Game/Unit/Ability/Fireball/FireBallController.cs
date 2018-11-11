using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public FireballDetails m_FireballDetails { get; set; }
    [SerializeField] private ParticleSystem m_ParticleSystem;
    [SerializeField] private ParticleSystem m_DestroyParticleSystem;
    public Unit CastingUnit {
        get { return CastingUnit;}
        set { CastingUnit = value; }
    }
    private Vector3 m_Direction;

    public Vector3 Direction
    {
        get { return m_Direction; }
        set { m_Direction = value; }
    }

    private float m_LocalTime = 0.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = -m_Direction * m_FireballDetails.m_ProjectileSpeed;
        gameObject.transform.localScale = gameObject.transform.localScale * m_FireballDetails.m_projectileSize;
        ParticleSystem.MainModule main = m_ParticleSystem.main;
        main.startSpeed = m_FireballDetails.m_ProjectileSpeed * 0.5f;
    }

    private void Update()
    {
        m_LocalTime += Time.deltaTime;
        if (m_LocalTime >= m_FireballDetails.m_ProjectileDuration)
        {
            m_DestroyParticleSystem.Play();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        GameObject otherGameObject = c.gameObject;
        Rigidbody otherGO_rb = otherGameObject.GetComponent<Rigidbody>()
            ? otherGameObject.GetComponent<Rigidbody>()
            : null;
        Health otherGO_health = otherGameObject.GetComponent<Health>() ? otherGameObject.GetComponent<Health>() : null;
        if (otherGO_rb)
        {
            otherGO_rb.AddForce(Vector3.back * m_FireballDetails.m_CollisionForce, ForceMode.Impulse);
        }
        if (otherGO_health)
        {
            otherGO_health.Damage(new DamageInfo {Source = CastingUnit, Amount = m_FireballDetails.m_CollisionDamage, DamageType = DamageType.Normal });
        }

        Destroy(this.gameObject);
    }
}
