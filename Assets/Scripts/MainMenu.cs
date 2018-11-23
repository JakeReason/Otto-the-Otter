//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeToBlack;
    public Animator startButton;
    public AudioClip buttonPress;
    public AudioClip woodCrash;
    public AudioSource button;
	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        startButton.SetBool("PressedStart", false);
        fadeToBlack.SetBool("StartFade", false);
	}

	public void StartAnim()
	{
        startButton.SetBool("PressedStart", true);
	}

    public void StartGame (int nStartSceneIndex)
	{
        SceneManager.LoadScene(nStartSceneIndex);
	}

    public void StartFade()
    {
        fadeToBlack.SetBool("StartFade", true);
    }
    public void PlaySound()
    {
        button.PlayOneShot(buttonPress);
    }
    public void Crash()
    {
        button.PlayOneShot(woodCrash);
    }

}
