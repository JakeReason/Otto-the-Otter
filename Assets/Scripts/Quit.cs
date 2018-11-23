//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
	// Used to animate the quit button.
	public Animator quitButton;

	// Used to animate the button press.
	public AudioClip buttonPress;

	// Audio scource used to play a sound when the button is pressed.
	public AudioSource button;

	// Audio Clip used to play a sound when the wooden plank UI falls.
	public AudioClip woodCrash;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	public void Awake()
	{
		quitButton.SetBool("Exit", false);
	}

	//--------------------------------------------------------------------------------
	// Exits the animator.
	//--------------------------------------------------------------------------------
	public void ExitAnim()
	{
		quitButton.SetBool("Exit", true);
	}

	//--------------------------------------------------------------------------------
	// Exits the game or the unity editor.
	//--------------------------------------------------------------------------------
	public void Exit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	//--------------------------------------------------------------------------------
	// Plays the button sound.
	//--------------------------------------------------------------------------------
	public void PlaySound()
	{
		button.PlayOneShot(buttonPress);
	}

	//--------------------------------------------------------------------------------
	// Plays the crash sound.
	//--------------------------------------------------------------------------------
	public void Crash()
	{
		button.PlayOneShot(woodCrash);
	}

	//--------------------------------------------------------------------------------
	// Loads the credits scene.
	//--------------------------------------------------------------------------------
	public void Credits()
	{
		SceneManager.LoadScene("Credits");
	}
}
