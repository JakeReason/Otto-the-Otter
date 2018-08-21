﻿//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using UnityEngine;

// Creates a class for the GrapplingHook
public class GrapplingHook : MonoBehaviour
{
    // Allows for access to the hook object itself
    public GameObject m_hook;

    // Represents where the hook object is being held in
    public GameObject m_hookHolder;

    // Indicates an object of which the hook has hooked onto
    public GameObject m_hookedObj;

    // Float represents the speed of the hook once launched
    public float m_fHookTravelSpeed;

    // Indicates the speed the player travels once hook is hooked on an object
    public float m_fTravelSpeed;

    // Represents the maximum distance the hook travels before retreating
    public float m_fMaxDistance;

    // Lists if the hook has been fired or not
    private bool m_bFired;

    // Bool lists if the hook has been fired or not
    private bool m_bHooked;
    private float m_fCurrentDistance;
    private CharacterController m_cc;
    private LineRenderer m_rope;
    private Transform m_originalTransform;

    void Awake()
    {
        m_cc = GetComponent<CharacterController>();
        m_rope = m_hook.GetComponent<LineRenderer>();

        m_originalTransform = m_hook.transform;
    }

    void Update()
    {
        // Firing the hook
        if (Input.GetButtonDown("Fire1") && !m_bFired)
        {
            m_bFired = true;
        }

        // Rope
        if (m_bFired)
        {
            m_rope.positionCount = 2;

            m_rope.SetPosition(0, m_hookHolder.transform.position);
            m_rope.SetPosition(1, m_hook.transform.position);
        }
        else
        {
            m_rope.positionCount = 0;
        }

        // Launched Hook
        if (m_bFired && !m_bHooked)
        {
            m_hook.transform.Translate(Vector3.forward * m_fHookTravelSpeed * Time.deltaTime);
            
            m_fCurrentDistance = Vector3.Distance(transform.position, m_hook.transform.position);

            if (m_fCurrentDistance >= m_fMaxDistance)
            {
                ReturnHook();
            }
        }

        // Translate towards the target
        if (m_bHooked && m_bFired)
        {
            m_hook.transform.parent = m_hookedObj.transform;
            //m_hook.transform.localScale = new Vector3(1, 1, 1);
            transform.position = Vector3.MoveTowards(transform.position, m_hook.transform.position, m_fTravelSpeed * Time.deltaTime);
            float fDistanceToHook = Vector3.Distance(transform.position, m_hook.transform.position);

            if (fDistanceToHook < 2.0f)
            {
                if (!m_cc.isGrounded)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 60.0f);
                    transform.Translate(Vector3.forward * Time.deltaTime * 50.0f);                  
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            m_hook.transform.parent = m_hookHolder.transform;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    private void ReturnHook()
    {
        m_hook.transform.position = m_hookHolder.transform.position;
        m_hook.transform.rotation = m_hookHolder.transform.rotation;
        m_hook.transform.localScale = new Vector3(1, 1, 1);

        m_bFired = false;
        m_bHooked = false;

        m_rope.positionCount = 0;
    }

    public bool GetFired()
    {
        return m_bFired;
    }

    public void SetHooked(bool bHooked)
    {
        m_bHooked = bHooked;
    }

    public bool GetHooked()
    {
        return m_bHooked;
    }
}
