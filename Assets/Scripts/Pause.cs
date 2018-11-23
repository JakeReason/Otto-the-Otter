//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	[SerializeField]
	// Gets reffernece to the pause menu object;
	private GameObject m_pauseMenu;

    [SerializeField]
	// Used to set the eventsystem current sellected to the Resume button GameObject.
	private GameObject m_resumeButton;

	[SerializeField]
	// Used to set the button when paused.
	private EventSystem m_eventSystem;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Sets the menu to false.
		m_pauseMenu.SetActive(false);
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. Checks if the pause buttons are pressed and 
	// opens the pause menu.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Checks if start or escape are being 
		if(XCI.GetButtonDown(XboxButton.Start) || Input.GetKeyDown(KeyCode.Escape))
		{
			// Sets the time scale to zero.
			Time.timeScale = 0;
			// Activates the pause menu.
			m_pauseMenu.SetActive(true);
			// Sets the pause menu button to selected.
			m_eventSystem.SetSelectedGameObject(m_resumeButton);
			// Sets the mouse cursor the locked which makes the cursor basically 
			// disable.
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
