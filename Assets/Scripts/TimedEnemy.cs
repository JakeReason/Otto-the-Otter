using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TimedEnemy : MonoBehaviour {

    private NavMeshAgent m_agent;
    [SerializeField]
    private Transform[] m_targetPoints;
    [SerializeField]
    private int m_nDestPoint = 0;
    [SerializeField]
    private float m_fCooldown = 1;
    private float m_fOriginalCooldown;

    // Use this for initialization
    void Start ()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_fOriginalCooldown = m_fCooldown;
        GotoNextPoint();
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (m_targetPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        m_agent.destination = m_targetPoints[m_nDestPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        m_nDestPoint = (m_nDestPoint + 1) % m_targetPoints.Length;
    }

    // Update is called once per frame
    void Update ()
    {
        if (m_agent.isOnNavMesh)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!m_agent.pathPending && m_agent.remainingDistance < 0.5f)
            {
                m_fCooldown -= Time.deltaTime;
                if (m_fCooldown <= 0)
                {
                    GotoNextPoint();
                    m_fCooldown = m_fOriginalCooldown;
                }
            }
        }
    }
}
