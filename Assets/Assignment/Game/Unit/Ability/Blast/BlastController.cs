using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BlastController : MonoBehaviour
{
    private bool isCasting = false;
    private float timer = 0.0f;

    private BlastDetails details;

    private Animator m_Animator;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        details = GetComponent<AbilityController>().s_PlayerBlastDetails;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ((timer > details.CastTime) && (isCasting))
        {
            CastAbility();
        }

    }

    public void Cast()
    {
        details = GetComponent<AbilityController>().s_PlayerBlastDetails;
        isCasting = true;
        timer = 0.0f;
        m_Animator.SetBool("Casting", true);
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
        BlastDetails bd = GetComponent<AbilityController>().s_PlayerBlastDetails;
        RaycastHit[] allRayCastsInInnerRadius = Physics.SphereCastAll(transform.position + new Vector3(0, 5, 0), bd.m_InnerRadius, Vector3.forward);
        RaycastHit[] allRayCastsInOuterRadius = Physics.SphereCastAll(transform.position + new Vector3(0, 5, 0), bd.m_OuterRadius, Vector3.forward);

        foreach (var allRayCastInInnerRadius in allRayCastsInInnerRadius)
        {
            var someVal = allRayCastInInnerRadius.collider.gameObject;
            if (someVal.GetComponent<Unit>())
            {
                if (someVal.GetComponent<Health>()) someVal.GetComponent<Health>().Damage(new DamageInfo {Source = GetComponent<Unit>(), Amount = bd.m_InnerDamage });
                if (someVal != gameObject) someVal.GetComponent<Rigidbody>().AddForce(bd.m_InnerForce * (someVal.transform.position - transform.position).normalized, ForceMode.Impulse);
            }
        }

        foreach (var allRayCastInOuterRadius in allRayCastsInOuterRadius)
        {
            foreach (var allRayCastInInnerRadius in allRayCastsInInnerRadius)
            {
                if (allRayCastInOuterRadius.collider.gameObject != allRayCastInInnerRadius.collider.gameObject)
                {
                    var someVal = allRayCastInOuterRadius.collider.gameObject;
                    if (someVal.GetComponent<Unit>())
                    {
                        if (someVal != gameObject)
                        {

                            if (someVal.GetComponent<Health>())
                            {
                                someVal.GetComponent<Health>().Damage(new DamageInfo {Source = GetComponent<Unit>(), Amount = bd.m_OuterDamage});
                            }

                            someVal.GetComponent<Rigidbody>()
                                .AddForce(
                                    bd.m_OuterForce * (someVal.transform.position - transform.position).normalized,
                                    ForceMode.Impulse);
                        }
                    }
                }
            }
        }
    }
}
