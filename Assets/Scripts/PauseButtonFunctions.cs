using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine;

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
	private CinemachineFreeLook m_freeLook;
	[SerializeField]
	private EventSystem m_eventSystem;
	private bool m_bFPS;
	private int m_nSceneIndex;

	public void Resume()
	{
		m_freeLook = GetComponent<CinemachineFreeLook>();
		m_bFPS = false;
		m_nSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
		if (!m_bFPS)
		{
			m_FPS.SetActive(true);
			m_bFPS = true;
		}
	}

	//public void Inverted(bool ticked)
	//{
	//	if(ticked)
	//	{
	//		m_freeLook.m_XAxis();
	//		m_freeLook.m_YAxis();
	//	}
	//	if(!ticked)
	//	{
	//		m_freeLook.m_XAxis();
	//		m_freeLook.m_YAxis();
	//	}
	//}


	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}



}
