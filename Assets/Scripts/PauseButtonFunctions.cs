using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseButtonFunctions : MonoBehaviour
{
	[SerializeField]
	private GameObject m_pauseMenu;
	[SerializeField]
	private EventSystem m_eventSystem;

    public Animator quitButton;

    public void Awake()
    {
        quitButton.SetBool("Exit", false);
    }

    public void Resume()
	{
		Time.timeScale = 1;
		m_pauseMenu.SetActive(false);
		m_eventSystem.SetSelectedGameObject(null);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void LoadScene(int index)
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(index);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

    public void ExitAnim()
    {
        quitButton.SetBool("Exit", true);
    }

}
