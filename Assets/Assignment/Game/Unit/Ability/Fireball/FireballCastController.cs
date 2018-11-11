using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FireballCastController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Fireball;
    private bool isCasting = false;
    private float timer = 0.0f;

    private Vector3 m_CastPosition;
    private Vector3 m_TargetDir;
    private Animator m_Animator;

    private FireballDetails details;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        details = GetComponent<AbilityController>().s_PlayerFireballDetails;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ((timer > details.CastTime) && isCasting)
        {
            CastAbility();
        }
    }


    public void Cast(Vector3 i_CastPosition, Vector3 i_TowardsPosition)
    {
        details = GetComponent<AbilityController>().s_PlayerFireballDetails;
        isCasting = true;
        m_Animator.SetBool("Casting", true);
        timer = 0.0f;
        m_CastPosition = i_CastPosition;
        m_TargetDir = i_TowardsPosition;
    }

    public void StopCast()
    {
        isCasting = false;
        timer = 0.0f;
        m_Animator.SetBool("Casting", false);
    }

    private void CastAbility()
    {
        StopCast();
        GameObject go = Instantiate(m_Fireball, m_CastPosition, Quaternion.identity);
        go.GetComponent<FireBallController>().Direction = m_TargetDir.normalized;
        go.GetComponent<FireBallController>().m_FireballDetails = details;
        go.GetComponent<FireBallController>().CastingUnit = GetComponent<Unit>();
    }
}