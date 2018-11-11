using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageOverTime : MonoBehaviour, IDamageSource {

    public Player m_DamageSourcePlayer
    {
        get { return null; }
        set { m_DamageSourcePlayer = null; }
    }

    [SerializeField]
    private float m_AmountOfDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().Damage(new DamageInfo {Source = null, Amount = m_AmountOfDamage * Time.deltaTime, DamageType = DamageType.Normal });
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            collision.gameObject.GetComponent<Health>().Damage(new DamageInfo {Source = null, Amount = m_AmountOfDamage * Time.deltaTime, DamageType = DamageType.Normal });
        }
    }
}
