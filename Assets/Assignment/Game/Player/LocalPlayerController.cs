using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class LocalPlayerController : PlayerController
{

    [SerializeField]
    private UnitController m_Character;

    [SerializeField] private MarkerScript m_marker;

    [SerializeField] private AbilityController m_LocalPlayerAbilities;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = GetMousePosition();
            if (m_Character.CanPlayerMove())
            {
                m_Character.MoveTo(mousePosition);
                m_LocalPlayerAbilities.StopCasting();
            }
            else
            {
                m_LocalPlayerAbilities.CastAbility(mousePosition);
            }
        }
        else if (!m_Character.CanPlayerMove())
        {
            m_marker.UpdateArrowPosition(GetMousePosition());
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(transform.up, transform.position);

        float distance;
        bool isHit = p.Raycast(ray, out distance);
        if (isHit)
        {
            Vector3 mousePosition = ray.GetPoint(distance);
            return mousePosition;
        }
        return Vector3.zero;
    }
}
