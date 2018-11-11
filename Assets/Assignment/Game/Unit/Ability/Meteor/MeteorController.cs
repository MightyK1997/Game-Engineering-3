using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    public GameObject m_MeteorGameObject;
    private Vector3 m_MeteorPosition;
    private Vector3 m_MeteorFinalPosition;
    private bool isCasting;
    private float m_Timer;
    private Animator m_Animator;
    private MeteorDetails details;

    // Use this for initialization
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_MeteorPosition = new Vector3(4.2f, 31.5f, -73.1f);
        details = GetComponent<AbilityController>().s_PlayerMeteorDetails;
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer += Time.deltaTime;
        if (isCasting && (m_Timer >details.CastTime))
        {
            CastAbility();
        }
    }

    private void CastAbility()
    {
        StopCast();
        GameObject go = Instantiate(m_MeteorGameObject, m_MeteorPosition, Quaternion.identity);
        go.transform.LookAt(m_MeteorFinalPosition);
        go.GetComponent<MeteorScript>().m_CastingPlayer = GetComponent<Unit>();
        go.GetComponent<MeteorScript>().details = details;
        go.GetComponent<Rigidbody>().AddForce(((m_MeteorFinalPosition - m_MeteorPosition).magnitude / details.m_FallTime) * Vector3.forward, ForceMode.Acceleration);
    }

    public void Cast(Vector3 i_Position)
    {
        details = GetComponent<AbilityController>().s_PlayerMeteorDetails;
        isCasting = true;
        m_Timer = 0.0f;
        m_MeteorFinalPosition = i_Position;
        m_Animator.SetBool("Casting", true);
    }

    public void StopCast()
    {
        isCasting = false;
        m_Timer = 0.0f;
        m_Animator.SetBool("Casting", false);
    }
}