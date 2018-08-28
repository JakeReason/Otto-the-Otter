//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Creates a class for the GrapplingHook
public class GrapplingHook : MonoBehaviour
{
	// Allows for access to the hook object itself
	public GameObject m_hook;

	// Represents where the hook object is being held in
	public GameObject m_hookHolder;

	// Indicates an object of which the hook has hooked onto
	public GameObject m_hookedObj;

	public Text m_hookableText;

	// Float represents the speed of the hook once launched
	public float m_fHookTravelSpeed;

	// Indicates the speed the player travels once hook is hooked on an object
	public float m_fTravelSpeed;

	// Represents the maximum distance the hook travels before retreating
	public float m_fMaxDistance;

	// Detects if a hookable object is within a certain distance from player
	public float m_fHookDistance = 5.0f;

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

	// List contains all hookable objects as GameObjects
	private List<GameObject> m_hookList;

	// Contains all of the hookable objects in range of the player
	private List<GameObject> m_hooksInRange;

	// Transform represents the transform of the locked on target
	private Transform m_target;

	//--------------------------------------------------------------------------------
	// Function is used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Gets the Character Controller component from the player
		m_cc = GetComponent<CharacterController>();

		// Gets the Line Renderer component from the hook child
		m_rope = m_hook.GetComponent<LineRenderer>();

		// Declares a "new" list of hookable objects
		m_hookList = new List<GameObject>();

		// Creates a "new" list of all in range hookable objects
		m_hooksInRange = new List<GameObject>();

		// If statement runs if the list of hookable objects is empty
		if (m_hookList == null)
		{
			// Stores all GameObjects with the tag "Hookable" in local array
			GameObject[] hookables = GameObject.FindGameObjectsWithTag("Hookable");

			// Runs for every GameObject in the hookables local array
			for (int i = 0; i < (hookables.Length - 1); ++i)
			{
				// Adds the hookable object to the hookable list if the slot is not null
				if (hookables[i] != null)
				{
					m_hookList.Add(hookables[i]);
				}
			}
		}

		// Sets the hookable objects in range list to initially be empty
		m_hooksInRange.Add(null);

		// Declares the target transform to be null
		m_target = null;

		// Defines the original transform to equal the hook's transform
		m_originalTransform = m_hook.transform;

		//m_hookableText.text = "No Objects Nearby.";
	}

	//--------------------------------------------------------------------------------
	// Function is called once every frame.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Calls function to check if a hookable object is in range
		HookRangeCheck();

		// Checks if there are any GameObjects in Hooks In Range lists
		//if (m_hooksInRange[0] != null)
		//{
		//	m_hookableText.text = "Objects Nearby!";
		//	m_target = m_hooksInRange[0].transform;			
		//}

		// Sets fired bool to equal true if fire button has been pressed
		if (Input.GetButtonDown("Fire1") && !m_bFired)
		{
			m_bFired = true;
		}

		// Checks if fired bool is set to true
		if (m_bFired)
		{
			// Declares the rope to have two positions
			m_rope.positionCount = 2;

			// Sets position zero to equal the position of the hook's holder
			m_rope.SetPosition(0, m_hookHolder.transform.position);

			// Sets position one to be the hook's position
			m_rope.SetPosition(1, m_hook.transform.position);
		}
		// Else rope is not created if fired bool is false
		else
		{
			m_rope.positionCount = 0;
		}

		// Runs if fired book is true but hooked bool is false
		if (m_bFired && !m_bHooked)
		{
			m_hook.transform.Translate(Vector3.forward * m_fHookTravelSpeed * Time.deltaTime);
			//// Fires hook forward if the player has no target
			//if (m_target == null)
			//{
			//	m_hook.transform.Translate(Vector3.forward * m_fHookTravelSpeed * Time.deltaTime);
			//}
			//// Otherwise fires hook towards the target if there is a target
			//else
			//{
			//	m_hook.transform.position = Vector3.MoveTowards(m_hook.transform.position,
			//													m_target.position,
			//													m_fHookTravelSpeed *
			//													Time.deltaTime);
			//}

			// Detects distance between the player and the hook and stores in float
			m_fCurrentDistance = Vector3.Distance(transform.position,
												  m_hook.transform.position);

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
			m_hook.transform.parent = m_hookedObj.transform;

			// Allows player to move towards the hook by a certain travel speed
			transform.position = Vector3.MoveTowards(transform.position,
													 m_hook.transform.position,
													 m_fTravelSpeed * Time.deltaTime);

			// Detects distance between the player and hook and stores in local float
			float fDistanceToHook = Vector3.Distance(transform.position,
													 m_hook.transform.position);

			// Checks if the distance is less than two
			if (fDistanceToHook < 2.0f)
			{
				// Detects if the player is not grounded
				if (!m_cc.isGrounded)
				{
					// Translates the player up and forward to represent climbing
					transform.Translate(Vector3.up * Time.deltaTime * 60.0f);
					transform.Translate(Vector3.forward * Time.deltaTime * 50.0f);
				}

				// Calls the climb coroutine by calling climb function
				StartCoroutine("Climb");
			}
		}
		// Else sets hook's parent transform to be hook holder transform
		else
		{
			m_hook.transform.parent = m_hookHolder.transform;
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
	// Waits for one second before continuing running code.
	//
	// Return:
	//		Returns the amount of time program needs to wait for.
	//--------------------------------------------------------------------------------
	IEnumerator RangeCheck()
	{
		yield return new WaitForSeconds(0.1f);
	}

	//--------------------------------------------------------------------------------
	// Detects if any hookable objects are in range.
	//--------------------------------------------------------------------------------
	private void HookRangeCheck()
	{
		// Runs for every hookable object in the hook list
		for (int i = 0; i < m_hookList.Count; ++i)
		{
			// Records distance between player and current hookable object in local float
			float fDist = Vector3.Distance(transform.position,
										   m_hookList[i].transform.position);

			//Debug.Log("Distance: " + fDist + ", Range: " + m_fHookDistance);

			// Checks if the hookable object is in range from player
			if (fDist < m_fHookDistance)
			{
				// Adds the currently selected object to hook in range list
				m_hooksInRange.Add(m_hookList[i]);
			}

			// Else if the hooks in range list has object and hookable object isn't in range
			if (m_hooksInRange.Contains(m_hookList[i]) && fDist >= m_fHookDistance)
			{
				// Removes that object from the hook 
				m_hooksInRange.Remove(m_hookList[i]);
			}
		}

		// Calls the RangeCheck coroutine by calling RangeCheck function
		//StartCoroutine("RangeCheck");
	}

	//--------------------------------------------------------------------------------
	// Returns the hook back to the player when called.
	//--------------------------------------------------------------------------------
	private void ReturnHook()
	{
		// Sets the hook's transform position and rotation to equal that of the holder
		m_hook.transform.position = m_hookHolder.transform.position;
		m_hook.transform.rotation = m_hookHolder.transform.rotation;

		// Sets the local scale of the hook to equal a Vector3 of ones
		m_hook.transform.localScale = new Vector3(1, 1, 1);

		// Initialises fired and hooked bools back to false
		m_bFired = false;
		m_bHooked = false;

		// Deletes the rope to have no positions
		m_rope.positionCount = 0;
	}

	//--------------------------------------------------------------------------------
	// Function returns the fired bool when called.
	//
	// Return:
	//		The result of the fired bool.
	//--------------------------------------------------------------------------------
	public bool GetFired()
	{
		return m_bFired;
	}

	//--------------------------------------------------------------------------------
	// Allows the hooked bool to be set to whatever is passed in to the function.
	//
	// Param:
	//		bHooked: Represents the value of what hooked bool will be set to.
	//--------------------------------------------------------------------------------
	public void SetHooked(bool bHooked)
	{
		m_bHooked = bHooked;
	}

	//--------------------------------------------------------------------------------
	// Function returns the hooked bool when called.
	//
	// Return:
	//		The result of the hooked bool.
	//--------------------------------------------------------------------------------
	public bool GetHooked()
	{
		return m_bHooked;
	}
}