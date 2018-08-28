//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using UnityEngine;

// Creates a class for the HookDetector
public class HookDetector : MonoBehaviour
{
	// Allows the script to access the player
    public GameObject m_player;

	// Collider used to switch on and off, depending on if it has been fired
    private Collider m_collider;

	//--------------------------------------------------------------------------------
	// Function is used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
    {
		// Gets the hook's collider component
        m_collider = GetComponent<Collider>();

		// Logs to the console if there is no collider on the hook
        if (!m_collider)
        {
            Debug.Log("NO COLLIDER!");
        }

		// Logs to the console if there is no grappling hook script attached to player
		if (m_player.GetComponent<GrapplingHook>() == null)
		{
			Debug.Log("NO HOOK!");
		}

		// Disables the hook's collider initially
		m_collider.enabled = false;
    }

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
	void Update()
    {
		// Enables hook collider if fired bool in Grappling Hook script is true
        if (m_player.GetComponent<GrapplingHook>().GetFired())
        {
            m_collider.enabled = true;
        }
		// Else hook collider is not enabled if fired bool is false
        else
        {
            m_collider.enabled = false;
        }
    }

	//--------------------------------------------------------------------------------
	// Function runs if hook collides with a trigger.
	//
	// Param:
	//		other: Represents the collider of object the hook is colliding with.
	//--------------------------------------------------------------------------------
	private void OnTriggerEnter(Collider other)
    {
		// Checks if the object's tag is "Hookable"
        if (other.tag == "Hookable")
        {
			// Sets hooked bool to true in Grappling Hook script
            m_player.GetComponent<GrapplingHook>().SetHooked(true);

			// Sets the hooked object in Grappling Hook script to equal the colliding object
			m_player.GetComponent<GrapplingHook>().m_hookedObj = other.gameObject;
        }
    }
}
