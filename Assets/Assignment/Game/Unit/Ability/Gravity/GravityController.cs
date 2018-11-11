using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

    private bool isCasting;
    private float m_Timer;
    private float m_SetupTime;
    private float m_GivenSetupTime = 1;
    private Vector3 m_CastPosition;
    private bool startedCasting;
    private float m_CastDuration;
    private Animator m_Animator;
    private GravityDetails details;


	// Use this for initialization
	void Start ()
	{
	    m_Animator = gameObject.GetComponent<Animator>();
        details = GetComponent<AbilityController>().s_PlayerGravityDetails;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    m_Timer += Time.deltaTime;
	    if (isCasting && (m_Timer > details.CastTime))
	    {
	        m_SetupTime += Time.deltaTime;
	        if (m_SetupTime > details.m_SetupTime)
	        {
                CastAbility();
	        }

	        if (startedCasting)
	        {
	            m_CastDuration += Time.deltaTime;
	            if (m_CastDuration > details.m_Duration)
	            {
	                isCasting = false;
	            }
	        }
	    }
	}

    private void CastAbility()
    {
        if (isCasting)
        {
            if ((transform.position - m_CastPosition).magnitude > details.m_CastRange)
            {
                transform.position = transform.position + (5 * (transform.position - m_CastPosition).normalized);
            }
            startedCasting = true;
            Collider[] allInnerObjects = Physics.OverlapSphere(m_CastPosition, details.m_InnerRadius);
            Collider[] allOuterObjects = Physics.OverlapSphere(m_CastPosition, details.m_OuterRadius);
            foreach (var allOuterObject in allOuterObjects)
            {
                foreach (var allInnerObject in allInnerObjects)
                {
                    if (allInnerObject.gameObject == allOuterObject.gameObject)
                    {
                        if (allInnerObject.GetComponent<Rigidbody>())
                        {
                            allInnerObject.GetComponent<Rigidbody>().AddForce(
                                (m_CastPosition - allInnerObject.transform.position).normalized * -details.m_InnerForce,
                                ForceMode.Impulse);
                        }
                    }
                    else
                    {
                        if (allOuterObject.GetComponent<Rigidbody>())
                        {
                            allOuterObject.GetComponent<Rigidbody>().AddForce(
                                (m_CastPosition - allOuterObject.transform.position).normalized *
                                (m_CastPosition - allInnerObject.transform.position).sqrMagnitude *
                                -details.m_OuterForce, ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }

    public void Cast(Vector3 i_Position)
    {

        isCasting = true;
        m_Timer = 0.0f;
        m_CastPosition = i_Position;
        m_Animator.SetBool("Casting", true);
    }

    public void StopCast()
    {
        isCasting = false;
        m_Timer = 0.0f;
        m_Animator.SetBool("Casting", false);
    }
}
