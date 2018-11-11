using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthController : MonoBehaviour
{

    private bool isCasting;
    private float m_Timer;
    private bool isAbilityCasted;
    private float m_AbilityTimer;
    private Animator m_Animator;
    private StealthDetails details;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        details = GetComponent<AbilityController>().s_PlayerStealthDetails;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    m_Timer += Time.deltaTime;
	    if (isCasting && (m_Timer > details.CastTime))
	    {
	        CastAbility();
	    }
	    if (isAbilityCasted)
	    {
	        m_AbilityTimer += Time.deltaTime;
	        if (m_AbilityTimer >= details.m_AbilityDuration)
	        {
	            isAbilityCasted = false;
                StopAbility();
	        }
	    }
	}


    private void CastAbility()
    {
        StopCast();
        isAbilityCasted = true;
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer  in renderers )
        {
            var c = renderer.material.color;
            renderer.material.color = new Color(c.r,c.g,c.b, 0);
        }
    }

    private void StopAbility()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            var c = renderer.material.color;
            renderer.material.color = new Color(c.r, c.g, c.b, 1);
        }
    }

    public void Cast()
    {
        details = GetComponent<AbilityController>().s_PlayerStealthDetails;
        isCasting = true;
        m_Timer = 0.0f;
        m_Animator.SetBool("Casting", true);
    }

    public void StopCast()
    {
        isCasting = false;
        m_Timer = 0.0f;
        m_Animator.SetBool("Casting", false);
    }
}
