using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapScript : MonoBehaviour {

    [SerializeField]
    private RawImage m_image;

    [SerializeField]
    private Camera m_miniMapCamera;
    [SerializeField]
    private Camera m_mainCamera;
    [SerializeField]
    private float m_maxRayCastDistance = 100.0f;

    private int m_layerMask;

    public void OnPointerDown(BaseEventData eventData)
    {
        UpdatePointEvent(eventData as PointerEventData);
    }

    public void OnPointerDrag(BaseEventData eventData)
    {
        UpdatePointEvent(eventData as PointerEventData);
    }

    private void UpdatePointEvent(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_image.rectTransform, eventData.position, null, out localPoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(m_image.rectTransform.rect, localPoint);
        Vector3 miniMapPoint = new Vector3(normalizedPoint.x, normalizedPoint.y, 0);
        ScrollCamera(miniMapPoint);
    }

    private void ScrollCamera(Vector3 m_InputVector)
    {
        Ray mMapToWorld = m_miniMapCamera.ViewportPointToRay(m_InputVector);
        RaycastHit hit;
        if (Physics.Raycast(mMapToWorld, out hit, m_maxRayCastDistance))
        {
            Vector3 cameraTarget = hit.point;
            Ray mainCameraToWorldRay = new Ray(m_mainCamera.transform.position, m_mainCamera.transform.forward);
            if (Physics.Raycast(mainCameraToWorldRay, out hit, m_maxRayCastDistance))
            {
                Vector3 mainCameraTarget = hit.point;
                Vector3 delta = cameraTarget - mainCameraTarget;
                delta.y = 0;
                m_mainCamera.transform.position += delta;
            }
        }
    }
}
