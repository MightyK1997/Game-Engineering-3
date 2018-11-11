using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private float i_PushForce = 10;

    public void Cast()
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), 10, transform.TransformDirection(Vector3.forward), out hit, 1000.0f))
        {
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForce(Vector3.back * i_PushForce, ForceMode.Impulse);
            }
        }
    }
}
