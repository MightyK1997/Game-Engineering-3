using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerScript : MonoBehaviour
{
    [SerializeField]
    private Image m_Marker;
    [SerializeField]
    private Image m_Arrow;

    private bool Casting = false;
    private bool MarkersRemvoed = true;

  
    public void UpdateArrowPosition(Vector3 i_MousePosition)
    {
        i_MousePosition.y = gameObject.transform.position.y;
        m_Arrow.transform.LookAt(i_MousePosition);
    }

    public void AddMarkerToScreen(Vector3 i_Position)
    {
        Casting = true;
        m_Marker.gameObject.SetActive(false);
        m_Marker.transform.position = i_Position;
    }

    public void RemoveMarkers()
    {
        Casting = false;
        m_Marker.gameObject.SetActive(false);
    }
}