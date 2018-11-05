using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;
using XboxCtrlrInput;

public class PauseButtonFunctions : MonoBehaviour
{
	[SerializeField]
	private GameObject m_pauseMenu;
	[SerializeField]
	private GameObject m_optionsMenu;
	[SerializeField]
	private GameObject m_FPS;
	[SerializeField]
	private GameObject m_camera;
    [SerializeField]
    private GameObject m_optionsButton;
    [SerializeField]
	private CinemachineFreeLook m_freeLook;
	[SerializeField]
	private EventSystem m_eventSystem;
	private bool m_bFPS;
	private bool m_bInvertCamera;
	private bool m_bInvertCameraX;
	private bool m_bInvertCameraY;
	private int m_nSceneIndex;

	private void Awake()
	{
		m_freeLook = m_camera.GetComponent<CinemachineFreeLook>();
		m_nSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	private void Update()
	{
		if (XCI.GetButtonDown(XboxButton.B))
		{
			if (m_optionsMenu.activeInHierarchy)
			{
				m_optionsMenu.SetActive(false);
				m_pauseMenu.SetActive(true);
			}
			else if (m_pauseMenu.activeInHierarchy)
			{
				Resume();
			}
		}
	}

	public void Resume()
	{
		Time.timeScale = 1;
		m_pauseMenu.SetActive(false);
		m_optionsMenu.SetActive(false);
		m_FPS.SetActive(false);
		m_eventSystem.SetSelectedGameObject(null);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void LoadScene(int index)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(index);
	}

	public void ResetScene()
	{
		SceneManager.LoadScene(m_nSceneIndex);
	}

	public void Options()
	{
		m_pauseMenu.SetActive(false);
		m_optionsMenu.SetActive(true);
        m_eventSystem.SetSelectedGameObject(m_optionsButton);
	}

	public void Back()
	{
		m_pauseMenu.SetActive(true);
		m_optionsMenu.SetActive(false);
	}

	public void FPS()
	{
		if (m_bFPS)
		{
			m_FPS.SetActive(true);
			m_bFPS = false;
		}
		else if (!m_bFPS)
		{
			m_FPS.SetActive(false);
			m_bFPS = true;
		}
	}

	public void Inverted()
	{
		if (m_bInvertCamera)
		{
			m_freeLook.m_XAxis.m_InvertAxis = true;
			m_freeLook.m_YAxis.m_InvertAxis = true;
			m_bInvertCamera = false;
		}
		else if (!m_bInvertCamera)
		{
			m_freeLook.m_XAxis.m_InvertAxis = false;
			m_freeLook.m_YAxis.m_InvertAxis = false;
			m_bInvertCamera = true;
		}
	}

	public void InvertX()
	{
		if (m_bInvertCameraX)
		{
			m_freeLook.m_XAxis.m_InvertAxis = true;
			m_bInvertCameraX = false;
		}
		else if (!m_bInvertCameraX)
		{
			m_freeLook.m_XAxis.m_InvertAxis = false;
			m_bInvertCameraX = true;
		}
	}

	public void InvertY()
	{
		if (m_bInvertCameraY)
		{
			m_freeLook.m_YAxis.m_InvertAxis = true;
			m_bInvertCameraY = false;
		}
		else if (!m_bInvertCameraY)
		{
			m_freeLook.m_YAxis.m_InvertAxis = false;
			m_bInvertCameraY = true;
		}
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
