//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu. Editted by: Jeremy Zoitas.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Creates a class for the Detector script
public class Detector : MonoBehaviour
{
    // Represents the transform for the player
	public Transform m_player;

    // Indicates the mesh layer of a detected object
	public LayerMask m_meshLayer;

    // Used to indicate which object is selected to target
	public Image m_selectImage;

    // Represents the hook game object
	public GameObject m_hook;

    // Stores the previous distance to determine closest distance
	private float m_fPrevDistance;

    // A list where all hookable objects detected can go into
	private List<GameObject> m_hookables;

    // Represents the transform for the target
	private Transform m_target;

    // Used to access variables and functions from the hook script
	private Hook m_hookScript;

    //--------------------------------------------------------------------------------
    // Function is used for initialization.
    //--------------------------------------------------------------------------------
    void Awake()
	{
        // Sets previous distance to zero on awake
		m_fPrevDistance = 0.0f;

        // Defines a "new" list of game objects where all hookables can go
		m_hookables = new List<GameObject>();

        // Sets the target's transform as null as there is no target yet
		m_target = null;

        // Gets the Hook script component from the hook game object
		m_hookScript = m_hook.GetComponent<Hook>();
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
	{
        // Detects if there is no game objects in the hookables list
		if (m_hookables.Count == 0)
		{
            // Target transform gets set to null
			m_target = null;

            // Selection image does not get enabled as there is no target
			m_selectImage.enabled = false;
		}
        // Else checks if there is one hookable in the list
		else if (m_hookables.Count == 1)
		{
            // Checks if there are no objects between the player and the target
			if (!ObjectsBetween(m_hookables[0].transform))
			{
                // The object's transform in the list is stored in the target variable
				m_target = m_hookables[0].transform;

                // Enables the select image
				m_selectImage.enabled = true;

                // Converts the transform to the screen and stores in local vector
				Vector3 v3TargetPos = Camera.main.WorldToScreenPoint(m_target.position);

                // Sets the images position to the converted transform of the target
				m_selectImage.transform.position = v3TargetPos;

                // Launchable bool gets set to true in the hook script
				m_hookScript.SetLaunchable(true);
			}
		}
        // Detects if the hookables list contains more than one object
		else if (m_hookables.Count > 1)
		{
            // Calls the distance check function from this class
			DistanceCheck();
		}
        // Otherwise logs an error in debug saying the list has an invalid count
		else
		{
			Debug.Log("HOOKABLES LIST HAS INVALID COUNT!");
		}
	}

    //--------------------------------------------------------------------------------
    // Detects if there are any objects between the plater and the passed in target.
    //
    // Param:
    //      desiredTarget: Represents the target of a hookable object.
    // Return:
    //      Returns whether there is an object between or not as a bool.
    //--------------------------------------------------------------------------------
    private bool ObjectsBetween(Transform desiredTarget)
	{
        // Declares a RaycastHit variable
		RaycastHit hit;
        
        // Checks for a linecast hit between player and transform
        if (Physics.Linecast(m_player.position, desiredTarget.position, 
                             out hit, m_meshLayer))
		{
            // Returns true if the detected object has a tree tag
			if (hit.collider.gameObject.CompareTag("Tree"))
			{
				return true;
			}
            // Otherwise returns false
			else
			{
				return false;
			}
		}
        // Returns false if there are no objects found
		else
		{
			return false;
		}
	}

    //--------------------------------------------------------------------------------
    // Function checks the distance between all objects in hookables list.
    //--------------------------------------------------------------------------------
    private void DistanceCheck()
	{
        // Runs a for loop through every object in the list
		for (int i = 0; i < m_hookables.Count; ++i)
		{
            // Gets the distance between the player and selected object in list
			float fDistance = Vector3.Distance(m_player.position, 
											   m_hookables[i].transform.position);

            // Checks if the distance exceeds the previous distance in list
			if (fDistance <= m_fPrevDistance)
			{
                // Detects if there are no objects between the player and selected object
				if (!ObjectsBetween(m_hookables[0].transform))
				{
                    // Sets the target as the current object's transform
				    m_target = m_hookables[i].transform;

                    // Enables the image which will be attached to the object
				    m_selectImage.enabled = true;

                    // Converts the targets position to screen space and stores in Vector3
				    Vector3 v3TargetPos = Camera.main.WorldToScreenPoint(m_target.position);

                    // Places the select image over the target position with screen space
				    m_selectImage.transform.position = v3TargetPos;

                    // Launchable bool gets set to true in the hook script
                    m_hookScript.SetLaunchable(true);
				}
			}

            // Sets the previous distance to equal the determined distance
			m_fPrevDistance = fDistance;
		}

        // Resets the previous distance float back to zero
		m_fPrevDistance = 0.0f;
	}

    //--------------------------------------------------------------------------------
    // Function is called when the player is enters a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
	{
        // Adds object to the list if the object has a hookable tag
		if (other.CompareTag("Hookable"))
		{
			m_hookables.Add(other.gameObject);
		}
	}

    //--------------------------------------------------------------------------------
    // Function is called when the player is exits a trigger.
    //
    // Param:
    //      other: Represents the collider of the trigger.
    //--------------------------------------------------------------------------------
    private void OnTriggerExit(Collider other)
	{
        // Removes object to the list if the object has a hookable tag
        if (other.CompareTag("Hookable"))
		{
			m_hookables.Remove(other.gameObject);
		}
	}

    //--------------------------------------------------------------------------------
    // Function allows other classes to obtain the target transform.
    //
    // Return:
    //      Returns the target itself as a transform.
    //--------------------------------------------------------------------------------
    public Transform GetTarget()
	{
		return m_target;
	}

    //--------------------------------------------------------------------------------
    // Function removes the passed in target from the list when called.
    //
    // Param:
    //      objToRemove: Represents the GameObject which will be deleted.
    //--------------------------------------------------------------------------------
    public void ClearTarget(GameObject objToRemove)
	{
		m_hookables.Remove(objToRemove);
	}
}
