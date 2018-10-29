using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator startButton;
	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        startButton.SetBool("PressedStart", false);
	}

	public void StartAnim()
	{
        startButton.SetBool("PressedStart", true);
	}

    public void StartGame (int nStartSceneIndex)
	{
		SceneManager.LoadScene(nStartSceneIndex);
	}
}
