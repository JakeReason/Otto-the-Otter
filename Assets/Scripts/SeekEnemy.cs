using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeekEnemy : MonoBehaviour {

    private NavMeshAgent m_agent;
    [SerializeField]
    private Transform m_player;
    private float m_fDistanceFromPlayer;
    [SerializeField]
    private float m_fDistance = 2;

    // Use this for initialization
    void Start ()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_agent.isOnNavMesh)
        {
            m_fDistanceFromPlayer = Vector3.Distance(transform.position, m_player.position);
            if(m_fDistanceFromPlayer <= m_fDistance)
            {
                m_agent.SetDestination(m_player.position);

            }
        }
    }
}
