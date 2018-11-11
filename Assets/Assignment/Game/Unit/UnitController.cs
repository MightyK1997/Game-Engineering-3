using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

public class UnitController : MonoBehaviour, IUnitController
{
    private Vector3? moveTarget;

    [SerializeField]
    private ThirdPersonCharacter m_TpCharacter;

    [SerializeField]
    private Health m_UnitHealth;

    private GameObjectPool m_GameobjectList = new GameObjectPool();

    [SerializeField] private GameObject m_RagdollObject;

    private bool m_CanMove = true;

    #region IUnitController
    public bool HasReachedDestination
    {
        get { return moveTarget == transform.position; }
    }
    
    public void MoveTo(Vector3 targetPosition)
    {
        moveTarget = targetPosition;
    }
    public void StopAll()
    {
        moveTarget = null;
    }
    #endregion

    private void Start()
    {
        m_GameobjectList.Create(m_RagdollObject, 1000);
    }

    //Calling The move function of default Unity Third Person Controller.
    private void FixedUpdate()
    {
        if (m_CanMove)
        {
            if (moveTarget.HasValue)
            {
                Vector3 deltaVector = moveTarget.Value - transform.position;
                m_TpCharacter.Move(deltaVector, false, false);
            }

            if (!moveTarget.HasValue || IsNear(transform.position, moveTarget.Value, 0.1f))
            {
                moveTarget = null;
                m_TpCharacter.Move(Vector3.zero, false, false);
            }
        }
    }

    //Checking if the player is near to a position to fix the movement.
    private bool IsNear(Vector3 a, Vector3 b, float epsilon)
    {
        return Vector3.Distance(a, b) < epsilon;
    }

    //This is a listener to event Which listens the to the health class and spawns a rigid body.
    public void OnUnitDeath()
    {
        GameObject go = m_GameobjectList.Get();
        if (go)
        {
            go.transform.position = transform.position;
            go.transform.localScale = transform.localScale;
            AlignRagDoll(go.transform, transform);
            go.SetActive(true);
        }
    }

    //This is a listener from the UI to stop the player from moving when using abilities.
    public void StopMovement()
    {
        m_CanMove = false;
    }

    //Listener to move the player again
    public void StartMovement()
    {
        m_CanMove = true;
    }

    //get the value of canMove
    public bool CanPlayerMove()
    {
        return m_CanMove;
    }

    private void AlignRagDoll(Transform a, Transform b)
    {
        for (int i = 0; i < a.childCount; i++)
        {
            string name = a.name;
            Transform aChild = a.GetChild(i);
            Transform bChild = b.Find(name);

            if (bChild != null)
            {
                bChild.position = aChild.position;
                bChild.rotation = aChild.rotation;

                AlignRagDoll(aChild, bChild);
            }
        }
    }
}
