using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class MeteorScript : MonoBehaviour {

    public Unit m_CastingPlayer { get; set; }

    private bool isRolling;
    private float m_RollingTime;


    public MeteorDetails details { get; set; }
	
	// Update is called once per frame
	void Update ()
	{
	    m_RollingTime += Time.deltaTime;
	    if (isRolling && (m_RollingTime > details.m_RollDuration))
	    {
	        Destroy(this.gameObject);
	    }
	}

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name.Contains("Island"))
        {
            Collider[] allObjects = Physics.OverlapSphere(transform.position, details.m_ImpactRange);
            foreach (var collider in allObjects)
            {
                var go = collider.gameObject;
                if (go.GetComponent<Unit>())
                {
                    go.GetComponent<Health>().Damage(new DamageInfo {Source = m_CastingPlayer, Amount = details.m_ImpactDamage });
                    go.GetComponent<Rigidbody>().AddForce(details.m_ImpactForce * (go.transform.position - transform.position).normalized, ForceMode.Impulse);
                }
            }
            StartCoroutine(RollCoroutine());
        }
        else
        {
            var go = c.gameObject;
            if (c.gameObject.GetComponent<Unit>())
            {
                go.GetComponent<Health>().Damage(new DamageInfo { Source = m_CastingPlayer, Amount = details.m_RollDamage });
            }
        }
    }

    private IEnumerator RollCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        isRolling = true;
        m_RollingTime = 0.0f;
        GetComponent<Rigidbody>().AddForce(Vector3.forward * details.m_RollSpeed, ForceMode.VelocityChange);
    }
}