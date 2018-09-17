using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookCamera : MonoBehaviour
{
	private CinemachineFreeLook m_freeLook;

	// Use this for initialization
	void Awake()
	{
		m_freeLook = GetComponent<CinemachineFreeLook>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxis("RightStickHorizontal") < 0.4f &&
			Input.GetAxis("RightStickVertical") < 0.4f &&
			Input.GetAxis("RightStickHorizontal") > -0.4f &&
			Input.GetAxis("RightStickVertical") > -0.4f)
		{
			m_freeLook.m_XAxis.m_InputAxisName = "MouseX";
			m_freeLook.m_YAxis.m_InputAxisName = "MouseY";
		}
		else
		{
			m_freeLook.m_XAxis.m_InputAxisName = "RightStickHorizontal";
			m_freeLook.m_YAxis.m_InputAxisName = "RightStickVertical";
		}
	}
}
