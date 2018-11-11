using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float m_delta = 10.0f;
    [SerializeField]
    private float m_speed = 3.0f;

    private Vector3 m_forwardDirection;
    private Vector3 m_rightDirection;
    private Vector3 m_upDirection;

    [Tooltip("x and y are for camera movemnt when scrolling near edge of screen. z is for mouse wheel scroll")]
    [SerializeField]
    private Vector3 m_maxDistance;
    // Use this for initialization

    private Vector3 m_initialPosition;

    void Start()
    {
        m_forwardDirection = transform.forward;
        m_rightDirection = transform.right;
        m_upDirection = transform.up;
        m_initialPosition = transform.position;
    }
    void LateUpdate()
    {
        if (Input.mousePosition.x >= Screen.width - m_delta)
        {
            transform.position += m_rightDirection * Time.fixedDeltaTime * m_speed;
        }
        if (Input.mousePosition.x <= m_delta)
        {
            transform.position -= m_rightDirection * Time.fixedDeltaTime * m_speed;
        }
        if (Input.mousePosition.y >= Screen.height - m_delta)
        {
            transform.position += m_upDirection * Time.fixedDeltaTime * m_speed;
        }
        if (Input.mousePosition.y <= m_delta)
        {
            transform.position -= m_upDirection * Time.fixedDeltaTime * m_speed;
        }

        if(!Mathf.Approximately(Input.GetAxis("Mouse ScrollWheel"), 0.0f))
        {
            transform.position += m_forwardDirection * Input.GetAxis("Mouse ScrollWheel") * m_speed;
        }

        //This controls the limits of the camera bounds when you're scrolling
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_initialPosition.x - m_maxDistance.x, m_initialPosition.x + m_maxDistance.x), Mathf.Clamp(transform.position.y, m_initialPosition.y - m_maxDistance.y, m_initialPosition.y + m_maxDistance.y), Mathf.Clamp(transform.position.z, m_initialPosition.z - m_maxDistance.z, m_initialPosition.z + m_maxDistance.z));
    }
}
