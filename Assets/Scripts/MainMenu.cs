using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void StartButton(int nStartSceneIndex)
	{
		SceneManager.LoadScene(nStartSceneIndex);
	}
}
