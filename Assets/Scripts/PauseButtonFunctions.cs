//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
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
	// Used to get the pause menu.
	private GameObject m_pauseMenu;

	[SerializeField]
	// Used to access the options menu.
	private GameObject m_optionsMenu;

	[SerializeField]
	// Used to acces the frame rate counter.
	private GameObject m_FPS;

	[SerializeField]
	// Used to get the cinemachine camera.
	private GameObject m_camera;

    [SerializeField]
	// Used to make to options button selected.
    private GameObject m_optionsButton;

    [SerializeField]
	// Used to make the resume button selected.
    private GameObject m_resumeButton;

    [SerializeField]
	// Used to access the cinemachine camera script.
	private CinemachineFreeLook m_freeLook;

	[SerializeField]
	// Used to make buttons selected.
	private EventSystem m_eventSystem;

	// Used to make the FPS UI pop up.
	private bool m_bFPS;

	// Used to invert the camera controls.
	private bool m_bInvertCamera;

	// Used to invert the camera controls.
	private bool m_bInvertCameraX;

	// Used to invert the camera controls.
	private bool m_bInvertCameraY;

	// Used to get the current scene index.
	private int m_nSceneIndex;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	private void Awake()
	{
		// Gets reference to the cinemachine camera.
		m_freeLook = m_camera.GetComponent<CinemachineFreeLook>();
		// Gets the current scene index.
		m_nSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame. Checks if puased and if b has been pressed to
	// go back from each pause menu.
	//--------------------------------------------------------------------------------
	private void Update()
	{
		if (XCI.GetButtonDown(XboxButton.B))
		{
			if (m_optionsMenu.activeInHierarchy)
			{
				// Sets time scale to one.
                Time.timeScale = 1;
				// Sets pause to false.
                m_pauseMenu.SetActive(false);
				// Sets options to false.
                m_optionsMenu.SetActive(false);
				// Sets the eventsystem current selected to null.
                m_eventSystem.SetSelectedGameObject(null);
				// Locks the mouse.
                Cursor.lockState = CursorLockMode.Locked;
				// Makes it invisible.
                Cursor.visible = false;
            }
			else if (m_pauseMenu.activeInHierarchy)
			{
				// Resumes the game if the pause menu is open.
				Resume();
			}
		}
	}

	//--------------------------------------------------------------------------------
	// Resumes the game and turns the pause menu off.
	//--------------------------------------------------------------------------------
	public void Resume()
	{
		// Sets time scale to one.
		Time.timeScale = 1;
		// Sets pause to false.
		m_pauseMenu.SetActive(false);
		// Sets options to false.
		m_optionsMenu.SetActive(false);
		// Sets the eventsystem current selected to null.
		m_eventSystem.SetSelectedGameObject(null);
		// Locks the mouse.
		Cursor.lockState = CursorLockMode.Locked;
		// Makes it invisible.
		Cursor.visible = false;
	}

	//--------------------------------------------------------------------------------
	// Loads a scene based on the index.
	//--------------------------------------------------------------------------------
	public void LoadScene(int index)
	{
		// Sets time scale to one.
		Time.timeScale = 1;
		// Loads a scene based on the index and build settings.
		SceneManager.LoadScene(index);
	}

	//--------------------------------------------------------------------------------
	// Resets the current scene.
	//--------------------------------------------------------------------------------
	public void ResetScene()
	{
		// Loads the current scene reseting it.
		SceneManager.LoadScene(m_nSceneIndex);
	}

	//--------------------------------------------------------------------------------
	// Opens the options menu and closes the pause menu.
	//--------------------------------------------------------------------------------
	public void Options()
	{
		// Sets pause to false.
		m_pauseMenu.SetActive(false);
		// Sets options to true.
		m_optionsMenu.SetActive(true);
		// Selects the options button.
        m_eventSystem.SetSelectedGameObject(m_optionsButton);
	}

	//--------------------------------------------------------------------------------
	// Used for a back buttonon the options menu.
	//--------------------------------------------------------------------------------
	public void Back()
	{
		// Sets pause to true.
		m_pauseMenu.SetActive(true);
		// Sets options to false.
		m_optionsMenu.SetActive(false);
		// Selects the options button.
		m_eventSystem.SetSelectedGameObject(m_resumeButton);
    }

	//--------------------------------------------------------------------------------
	// Turns the FPS on and off.
	//--------------------------------------------------------------------------------
	public void FPS()
	{
		if (m_bFPS)
		{
			// If the fps option is ticked then it turns the fps on.
			m_FPS.SetActive(true);
			m_bFPS = false;
		}
		else if (!m_bFPS)
		{
			// If the fps option is unticked then it turns the fps off.
			m_FPS.SetActive(false);
			m_bFPS = true;
		}
	}

	//--------------------------------------------------------------------------------
	// Inverts the both axis of the camera.
	//--------------------------------------------------------------------------------
	public void Inverted()
	{
		if (m_bInvertCamera)
		{
			// If the inverted option is ticked then it turns the controls inverted.
			m_freeLook.m_XAxis.m_InvertAxis = true;
			m_freeLook.m_YAxis.m_InvertAxis = true;
			m_bInvertCamera = false;
		}
		else if (!m_bInvertCamera)
		{
			// If the inverted option is unticked then it turns the controls back.
			m_freeLook.m_XAxis.m_InvertAxis = false;
			m_freeLook.m_YAxis.m_InvertAxis = false;
			m_bInvertCamera = true;
		}
	}

	//--------------------------------------------------------------------------------
	// Inverts the X axis of the camera.
	//--------------------------------------------------------------------------------
	public void InvertX()
	{
		if (m_bInvertCameraX)
		{
			// If the inverted option is unticked then it turns the controls inverted.
			m_freeLook.m_XAxis.m_InvertAxis = true;
			m_bInvertCameraX = false;
		}
		else if (!m_bInvertCameraX)
		{
			// If the inverted option is unticked then it turns the controls back.
			m_freeLook.m_XAxis.m_InvertAxis = false;
			m_bInvertCameraX = true;
		}
	}

	//--------------------------------------------------------------------------------
	// Inverts the Y axis of the camera.
	//--------------------------------------------------------------------------------
	public void InvertY()
	{
		if (m_bInvertCameraY)
		{
			// If the inverted option is unticked then it turns the controls inverted.
			m_freeLook.m_YAxis.m_InvertAxis = true;
			m_bInvertCameraY = false;
		}
		else if (!m_bInvertCameraY)
		{
			// If the inverted option is unticked then it turns the controls back.
			m_freeLook.m_YAxis.m_InvertAxis = false;
			m_bInvertCameraY = true;
		}
	}

	//--------------------------------------------------------------------------------
	// Quits unity editor and the application. 
	//--------------------------------------------------------------------------------
	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}
