//--------------------------------------------------------------------------------
// Author: Matthew Le Nepveu.
//--------------------------------------------------------------------------------

// Accesses the plugins from Unity folder
using Cinemachine;
using UnityEngine;

// Creates a class for the Player script requiring a CharacterController
[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookCamera : MonoBehaviour
{
    // Private variable used to access the Cinemachine script
	private CinemachineFreeLook m_freeLook;

    //--------------------------------------------------------------------------------
    // Function is used for initialization.
    //--------------------------------------------------------------------------------
    void Awake()
	{
        // Gets the Cinemachine script component on awake
		m_freeLook = GetComponent<CinemachineFreeLook>();
	}

    //--------------------------------------------------------------------------------
    // Function is called once every frame.
    //--------------------------------------------------------------------------------
    void Update()
	{
        // Checks if there is no input from the right stick
		if (Input.GetAxis("RightStickHorizontal") < 0.4f &&
			Input.GetAxis("RightStickVertical") < 0.4f &&
			Input.GetAxis("RightStickHorizontal") > -0.4f &&
			Input.GetAxis("RightStickVertical") > -0.4f)
		{
            // Informs cinemachine to use mouse controls for camera movement
			m_freeLook.m_XAxis.m_InputAxisName = "MouseX";
			m_freeLook.m_YAxis.m_InputAxisName = "MouseY";
		}
        // Else right stick gets used for camera input
		else
		{
			m_freeLook.m_XAxis.m_InputAxisName = "RightStickHorizontal";
			m_freeLook.m_YAxis.m_InputAxisName = "RightStickVertical";
		}
	}
}
