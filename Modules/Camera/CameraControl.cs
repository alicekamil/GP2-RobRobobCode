using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpaceGame;
using UnityEngine;

public class CameraControl : MonoSingleton<CameraControl>
{
    public float m_DampTime = 0.2f; //Approx time for camera movement                 
    public float m_ScreenEdgeBuffer = 4f;          
    public float m_MinSize = 6.5f; // Minimum zoom for the camera                 
    public Transform OverrideTarget;

    private List<Transform> m_Targets; 
    private Camera m_Camera;                        
    private float m_ZoomSpeed; // Damp variable for zoom speed                      
    private Vector3 m_MoveVelocity; // Damp variable for movevelocity                  
    private Vector3 m_DesiredPosition; // Average of both players position
    private CameraShake _cameraShake;

    public Camera Camera => m_Camera;

    public void Shake() => _cameraShake.Shake();


    protected override void Awake()
    {
        base.Awake();
        m_Camera = GetComponentInChildren<Camera>();
        _cameraShake = m_Camera.GetComponent<CameraShake>();
        m_Targets = FindObjectsOfType<CharacterLogic>().Select(c => c.transform).ToList();
    }

    private void FixedUpdate()
    {
        CameraMove();
        CameraZoom();
    }
    
    private void CameraMove()
    {
        FindAveragePosition(); 

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime); 
    }

    private void FindAveragePosition()
    {
        if (OverrideTarget == null)
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0; // Number of players

            for (int i = 0; i < m_Targets.Count; i++) 
            {
                if (!m_Targets[i].gameObject.activeSelf) // Prevent following dead player
                    continue; 

                averagePos += m_Targets[i].position; // New pos
                numTargets++; 
            }

            if (numTargets > 0) // Avg
                averagePos /= numTargets;

            averagePos.y = transform.position.y; // Dont change the y-position(Inherit camera rig y-position). Safety
            // Don't move along Z axis
            averagePos.z = transform.position.z;
            m_DesiredPosition = averagePos;
        }
        else
        {
            m_DesiredPosition = OverrideTarget.position;
            m_DesiredPosition.z = transform.position.z;
        }
    }


    private void CameraZoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        float size = 0f; // Largest size picked
        
        if (OverrideTarget == null)
        {
            for (int i = 0; i < m_Targets.Count; i++)
            {
                if (!m_Targets[i].gameObject.activeSelf)
                    continue;

                Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position); // World to local space

                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
            }
        }
        else
        {
            size = m_MinSize;
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize() // Reseting scene every start of round
    {
        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }
}

