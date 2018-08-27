//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject m_player;
    private Collider m_collider;

    void Awake()
    {
        m_collider = GetComponent<Collider>();

        if (!m_collider)
        {
            Debug.Log("NO COLLIDER!");
        }

        m_collider.enabled = false;
    }

    void Update()
    {
		if (m_player.GetComponent<GrapplingHook>() == null)
		{
			Debug.Log("NO HOOK SON!");
		}

        if (m_player.GetComponent<GrapplingHook>().GetFired())
        {
            m_collider.enabled = true;
        }
        else
        {
            m_collider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            m_player.GetComponent<GrapplingHook>().SetHooked(true);
            m_player.GetComponent<GrapplingHook>().m_hookedObj = other.gameObject;
        }
    }
}
