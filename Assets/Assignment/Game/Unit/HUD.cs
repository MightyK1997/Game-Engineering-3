using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Unit m_thisUnit = null;
    [SerializeField] private Camera m_mainCamera;
	
    /// <summary>
    /// Update the text and health value;
    /// </summary>
	void Update ()
	{
	    this.gameObject.GetComponentInChildren<Text>().text = m_thisUnit.Name;
	    this.GetComponentInChildren<Slider>().value = m_thisUnit.Health.Percent;
	}

    private void LateUpdate()
    {
        transform.LookAt(transform.position + m_mainCamera.transform.rotation * Vector3.back, m_mainCamera.transform.rotation * Vector3.up);
    }
}
