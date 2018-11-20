//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

// Creates a class for the Hook script
public class Hook : MonoBehaviour
{
	// Allows the Hook script to access the player
    public GameObject m_player;

	// Represents where the hook object is being held in
	public GameObject m_hookHolder;

    // Indicates the object that detects for all hookable objects
	public GameObject m_detector;

    // Represents the player's scarf as a GameObject
	public GameObject m_scarf;

	// Indicates an object of which the hook has hooked onto
	[HideInInspector]
	public GameObject m_hookedObj;

    // Stores the layer of the player for collision purposes
	public LayerMask m_playerLayer;

	// Float represents the speed of the hook once launched
	public float m_fHookTravelSpeed;

	// Indicates the speed the player travels once hook is hooked on an object
	public float m_fTravelSpeed;

	// Represents the maximum distance the hook travels before retreating
	public float m_fMaxDistance;

	// Detects if a hookable object is within a certain distance from player
	public float m_fHookDistance = 5.0f;

    // Public float used to represent the height you climb after reaching target
	public float m_fClimbUp;

    // Float used to represent the length you climb after reaching target
    public float m_fClimbForward;

	// Lists if the hook has been fired or not
	private bool m_bFired;

	// Bool lists if the hook has been fired or not
	private bool m_bHooked;

    // Indicates if the hook can be launched or not
	private bool m_bLaunchable;

    // Used to detect if hook is attached to an enemy
	private bool m_bEnemyHooked;

	// Represents the distance between the player and the hook
	private float m_fCurrentDistance;

	// Allows script to access the player's character controller
	private CharacterController m_cc;

	// Accesses the line renderer of the hook
	private LineRenderer m_rope;

	// Indicates the initial transform of the hook
	private Transform m_originalTransform;

	// Transform represents the transform of the locked on target
	private Transform m_hookTarget;

	// Collider used to switch on and off, depending on if it has been fired
	private Collider m_collider;

    // Used to access the variables and functions from the detector script
	private Detector m_detectorScript;

    // Determines the initial scale of the hook
	private Vector3 m_v3HookScale;

    // Used to access the MeshRenderer component and change its properties
	private MeshRenderer m_mesh;

	//--------------------------------------------------------------------------------
	// Function is used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
    {
        // Sets the scarf active on awake
		m_scarf.SetActive(true);

        // Gets the Mesh Renderer component from the hook
		m_mesh = GetComponent<MeshRenderer>();

        // Logs an error message in debug mode if no mesh renderer could be found
		if (!m_mesh)
		{
			Debug.Log("NO MESH RENDERER ATTACHED!");
		}

        // Disables the mesh renderer
		m_mesh.enabled = false;

		// Gets the hook's collider component
		m_collider = GetComponent<Collider>();

		// Logs to the console in debug if there is no collider on the hook
        if (!m_collider)
        {
            Debug.Log("NO COLLIDER!");
        }

		// Disables the hook's collider initially
		m_collider.enabled = false;

		// Gets the Character Controller component from the player
		m_cc = m_player.GetComponent<CharacterController>();

		// Gets the Line Renderer component from the hook child
		m_rope = GetComponent<LineRenderer>();

		// Declares the target transform to be null
		m_hookTarget = null;

		// Defines the original transform to equal the hook's transform
		m_originalTransform = transform;

        // Accesses the Detector script from the detector itself
		m_detectorScript = m_detector.GetComponent<Detector>();

        // Logs an error message in debug if the Detector script could not be found
		if (!m_detectorScript)
		{
			Debug.Log("GET DETECTOR FAILED!");
		}

        // Sets both private bools to false initially
		m_bLaunchable = false;
		m_bEnemyHooked = false;

        // Stores the localscale in the hook scale vector
		m_v3HookScale = transform.localScale;
	}

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
	void Update()
    {
		// Sets fired bool to equal true if fire button has been pressed
		if (Input.GetButtonDown("Grapple") && !m_bFired && m_bLaunchable)
		{
			m_bFired = true;
		}

		// Checks if fired bool is set to true
		if (m_bFired)
		{
            // Enables the mesh for the hook so it can be seen
			m_mesh.enabled = true;

            // Disables the player's scarf
			m_scarf.SetActive(false);

            // Enables the collider for the scarf so it can hit objects
			m_collider.enabled = true;

			// Declares the rope to have two positions
			m_rope.positionCount = 2;

			// Sets position zero to equal the position of the hook's holder
			m_rope.SetPosition(0, m_hookHolder.transform.position);

			// Sets position one to be the hook's position
			m_rope.SetPosition(1, transform.position);
		}
		// Else rope is not created if fired bool is false
		else if (!m_bHooked || !m_bFired)
		{
            // Turns the collider off
			m_collider.enabled = false;

            // Resets the enemy hook bool back to false
			m_bEnemyHooked = false;

            // Sets the ropes position count to zero
			m_rope.positionCount = 0;

            // Accesses the target from the detector script and stores it
			m_hookTarget = m_detectorScript.GetTarget();
		}

		// Runs if fired book is true but hooked bool is false
		if (m_bFired && !m_bHooked)
		{
            // Checks if the hook has a desired target
			if (m_hookTarget)
			{
                // Makes the scarf move towards the target
				transform.position = Vector3.MoveTowards(transform.position, 
														 m_hookTarget.position + Vector3.up * 0.5f,
														 m_fHookTravelSpeed * Time.deltaTime);
			}
            // Else the scarf travels forward by speed and time
			else
			{
				transform.Translate(Vector3.forward * m_fHookTravelSpeed * Time.deltaTime);
			}

			// Detects distance between the player and the hook and stores in float
			m_fCurrentDistance = Vector3.Distance(m_player.transform.position,
												  transform.position);

			// Returns hook if the distance exceeds the maximum distance
			if (m_fCurrentDistance >= m_fMaxDistance)
			{
				ReturnHook();
			}
		}

		// If statement runs if both hooked and fired bools are true
		if (m_bHooked && m_bFired)
		{
			// Sets the hook's parent transform to equal the transform as the hooked object
			transform.parent = m_hookedObj.transform;

			// Allows player to move towards the hook by a certain travel speed
			m_player.transform.position = Vector3.MoveTowards(m_player.transform.position,
													 transform.position,
													 m_fTravelSpeed * Time.deltaTime);

			// Detects distance between the player and hook and stores in local float
			float fDistanceToHook = Vector3.Distance(m_player.transform.position,
													 transform.position);

			// Checks if the distance is less than two
			if (fDistanceToHook < 2.0f)
			{
				// Detects if the player is not grounded
				if (!m_cc.isGrounded)
				{
					// Translates the player up and forward to represent climbing
					m_player.transform.Translate(Vector3.up * Time.deltaTime * m_fClimbUp);
					m_player.transform.Translate(Vector3.forward * Time.deltaTime * m_fClimbForward);
				}

				// Calls the climb coroutine by calling climb function
				StartCoroutine("Climb");
			}
		}
		// Else sets hook's parent transform to be hook holder transform
		else
		{
			transform.parent = m_hookHolder.transform;
		}
	}

	//--------------------------------------------------------------------------------
	// Allows the player to climb up an object once hooked onto it.
	//
	// Return:
	//		Waits for 0.1 seconds before being called.
	//--------------------------------------------------------------------------------
	IEnumerator Climb()
	{
		// Waits for 0.1 seconds before being called
		yield return new WaitForSeconds(0.1f);

        // Sets the enemy hooked boolean to be true
		m_bEnemyHooked = true;

		// Calls the return hook function after yielding for 0.1 seconds
		ReturnHook();
	}

	//--------------------------------------------------------------------------------
	// Returns the hook back to the player when called.
	//--------------------------------------------------------------------------------
	private void ReturnHook()
	{
        // Parent is set back to be the hook holder
		transform.parent = m_hookHolder.transform;

		// Sets the hook's transform position and rotation to equal that of the holder
		transform.position = m_hookHolder.transform.position;
		transform.rotation = m_hookHolder.transform.rotation;

		// Sets the local scale of the hook to equal a Vector3 of ones
		transform.localScale = m_v3HookScale;

		// Initialises fired and hooked bools back to false
		m_bFired = false;
		m_bHooked = false;

        // Resets the target back to null
		m_hookTarget = null;

        // Disables the mesh renderer for the hook
		m_mesh.enabled = false;

        // Enables the mesh of the scarf around the player
		m_scarf.SetActive(true);

		// Deletes the rope to have no positions
		m_rope.positionCount = 0;
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
        if (other.CompareTag("Hookable"))
        {
			// Sets hooked bool to true in Grappling Hook script
			m_bHooked = true;

			// Sets the hooked object in Grappling Hook script to equal the colliding object
			m_hookedObj = other.gameObject;
        }
    }

    //--------------------------------------------------------------------------------
    // Function allows other scripts to get the Hooked variable.
    //
    // Return:
    //		Returns the Hooked bool.
    //--------------------------------------------------------------------------------
    public bool GetHooked()
	{
		return m_bHooked;
	}

    //--------------------------------------------------------------------------------
    // Function allows other scripts to get the Fired variable.
    //
    // Return:
    //		Returns the Fired bool.
    //--------------------------------------------------------------------------------
    public bool GetFired()
	{
		return m_bFired;
	}

    //--------------------------------------------------------------------------------
    // Function allows other scripts to set the launchable bool to a value.
    //
    // Param:
    //		bLaunchable: Represents the new value of the launchable bool.
    //--------------------------------------------------------------------------------
    public void SetLaunchable(bool bLaunchable)
	{
		m_bLaunchable = bLaunchable;
	}

    //--------------------------------------------------------------------------------
    // Function allows other scripts to get the Enemy Hooked variable.
    //
    // Return:
    //		Returns the Enemy Hooked bool.
    //--------------------------------------------------------------------------------
    public bool GetEnemyHooked()
	{
		return m_bEnemyHooked;
	}
}
