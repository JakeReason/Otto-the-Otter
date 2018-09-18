using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

	[SerializeField]
	private GameObject m_pauseMenu;
	[SerializeField]
	private GameObject m_resumeButton;
	[SerializeField]
	private EventSystem m_eventSystem;


	// Use this for initialization
	void Awake()
	{
		m_pauseMenu.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if(XCI.GetButtonDown(XboxButton.Start) || Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 0;
			m_pauseMenu.SetActive(true);
			m_eventSystem.SetSelectedGameObject(m_resumeButton);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
