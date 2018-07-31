using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject m_player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            m_player.GetComponent<GrapplingHook>().m_bHooked = true;
            m_player.GetComponent<GrapplingHook>().m_hookedObj = other.gameObject;
        }
    }
}
