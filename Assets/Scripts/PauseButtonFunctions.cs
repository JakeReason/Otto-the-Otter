using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonFunctions : MonoBehaviour
{
	[SerializeField]
	private GameObject m_pauseMenu;

	public void Resume()
	{
		Time.timeScale = 1;
		m_pauseMenu.SetActive(false);
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
}
