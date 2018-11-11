using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private MarkerScript m_Marker;
    private float m_MaxDistance;
    private bool isCasting = false;
    private Vector3 m_FinalDestination;
    private Animator m_Animator;
    private float timer;

    public UnityEvent m_RemoveMarkerEvent;

    private TeleportDetails details;

    private void Start()
    {
        m_RemoveMarkerEvent.AddListener(delegate { m_Marker.RemoveMarkers(); });
        m_Animator = gameObject.GetComponent<Animator>();
        details = GetComponent<AbilityController>().s_PlayerTeleportDetails;
        m_MaxDistance = details.m_MaxDistance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ((timer > details.CastTime) && isCasting)
        {
            CastAbility();
        }
    }


    public void Cast(Vector3 i_Location)
    {
        isCasting = true;
        timer = 0.0f;
        m_FinalDestination = i_Location;
        m_Animator.SetBool("Casting", true);
    }

    public void StopCast()
    {
        isCasting = false;
        m_Animator.SetBool("Casting", false);
        m_RemoveMarkerEvent.Invoke();
        timer = 0.0f;
    }

    private void CastAbility()
    {
        StopCast();
        float distance = (transform.position - m_FinalDestination).magnitude;
        if (distance <= m_MaxDistance)
        {
            Ray ray = new Ray(m_FinalDestination, Vector3.down);
            RaycastHit m_Info;
            bool isHit = Physics.Raycast(ray, out m_Info);
            if (isHit)
            {
                Collider c = m_Info.collider;
                if (!c.gameObject.name.Contains("Island"))
                {
                    Vector3 bounds = c.bounds.size;
                    m_FinalDestination = new Vector3(m_FinalDestination.x, m_FinalDestination.y,
                        m_FinalDestination.z - (bounds.z / 2));
                }
            }

            transform.position = m_FinalDestination;
        }
        else
        {
            Vector3 normalizedVector = (m_FinalDestination - transform.position).normalized;
            Vector3 destination = transform.position + (m_MaxDistance * normalizedVector);
            Ray ray = new Ray(destination, Vector3.down);
            RaycastHit m_Info;
            bool isHit = Physics.Raycast(ray, out m_Info);
            if (isHit)
            {
                Collider c = m_Info.collider;
                if (!c.gameObject.name.Contains("Island"))
                {
                    Vector3 bounds = c.bounds.size;
                    destination = new Vector3(destination.x, destination.y,
                        destination.z - (bounds.z / 2));
                }
            }

            transform.position = destination;
        }
    }
}
