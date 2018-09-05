//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the HookDetector
public class Hook : MonoBehaviour
{
	// Allows the script to access the player
    public GameObject m_player;

	// Collider used to switch on and off, depending on if it has been fired
    private Collider m_collider;

	// Represents where the hook object is being held in
	public GameObject m_hookHolder;

	// Indicates an object of which the hook has hooked onto
	[HideInInspector]
	public GameObject m_hookedObj;

	// Float represents the speed of the hook once launched
	public float m_fHookTravelSpeed;

	// Indicates the speed the player travels once hook is hooked on an object
	public float m_fTravelSpeed;

	// Represents the maximum distance the hook travels before retreating
	public float m_fMaxDistance;

	// Detects if a hookable object is within a certain distance from player
	public float m_fHookDistance = 5.0f;

	public float m_fClimbUp;
	
	public float m_fClimbForward;

	// Lists if the hook has been fired or not
	private bool m_bFired;

	// Bool lists if the hook has been fired or not
	private bool m_bHooked;

	// Represents the distance between the player and the hook
	private float m_fCurrentDistance;

	// Allows script to access the player's character controller
	private CharacterController m_cc;

	// Accesses the line renderer of the hook
	private LineRenderer m_rope;

	// Indicates the initial transform of the hook
	private Transform m_originalTransform;

	// Transform represents the transform of the locked on target
	private Transform m_target;

	private Collider m_detector;

	private Detector m_detectorScript;

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

		// Disables the hook's collider initially
		m_collider.enabled = false;

		// Gets the Character Controller component from the player
		m_cc = m_player.GetComponent<CharacterController>();

		// Gets the Line Renderer component from the hook child
		m_rope = GetComponent<LineRenderer>();

		// Declares the target transform to be null
		m_target = null;

		// Defines the original transform to equal the hook's transform
		m_originalTransform = transform;

		m_detector = gameObject.GetComponentInChildren<Collider>();

		if (!m_detector)
		{
			Debug.Log("GET COLLIDER FAILED!");
		}

		m_detectorScript = gameObject.GetComponentInChildren<Detector>();

		if (!m_detectorScript)
		{
			Debug.Log("GET DETECTOR FAILED!");
		}
	}

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
	void Update()
    {
		// Sets fired bool to equal true if fire button has been pressed
		if (Input.GetButtonDown("Fire1") && !m_bFired)
		{
			m_bFired = true;
		}

		// Checks if fired bool is set to true
		if (m_bFired)
		{
			m_collider.enabled = true;

			// Declares the rope to have two positions
			m_rope.positionCount = 2;

			// Sets position zero to equal the position of the hook's holder
			m_rope.SetPosition(0, m_hookHolder.transform.position);

			// Sets position one to be the hook's position
			m_rope.SetPosition(1, transform.position);
		}
		// Else rope is not created if fired bool is false
		else
		{
			m_collider.enabled = false;

			m_rope.positionCount = 0;
		}

		// Runs if fired book is true but hooked bool is false
		if (m_bFired && !m_bHooked)
		{
			//if (m_detectorScript.GetInRange())
			//{
			//	if (!m_detectorScript.GetTarget())
			//	{
			//		Debug.Log("TARGET SYSTEM NOT WORKING!");
			//	}
			//	else
			//	{
			//		transform.position = Vector3.MoveTowards(transform.position,
			//												 m_detectorScript.GetTarget().position,
			//												 m_fHookTravelSpeed * Time.deltaTime);
			//	}
			//}
			//else
			//{
				transform.Translate(Vector3.forward * m_fHookTravelSpeed * Time.deltaTime);
			//}

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

		// Calls the return hook function after yielding for 0.1 seconds
		ReturnHook();
	}

	//--------------------------------------------------------------------------------
	// Returns the hook back to the player when called.
	//--------------------------------------------------------------------------------
	private void ReturnHook()
	{
		transform.parent = m_hookHolder.transform;

		// Sets the hook's transform position and rotation to equal that of the holder
		transform.position = m_hookHolder.transform.position;
		transform.rotation = m_hookHolder.transform.rotation;

		// Sets the local scale of the hook to equal a Vector3 of ones
		transform.localScale = new Vector3(1, 1, 1);

		// Initialises fired and hooked bools back to false
		m_bFired = false;
		m_bHooked = false;

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

	public bool GetHooked()
	{
		return m_bHooked;
	}

	public bool GetFired()
	{
		return m_bFired;
	}
}
