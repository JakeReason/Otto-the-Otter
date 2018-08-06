using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject m_hook;
    public GameObject m_hookHolder;
    public GameObject m_hookedObj;
    public float m_fHookTravelSpeed;
    public float m_fTravelSpeed;
    public float m_fMaxDistance;

    private bool m_bFired;
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
