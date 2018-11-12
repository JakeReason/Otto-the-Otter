using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

    public Animator quitButton;
    public AudioClip buttonPress;
    public AudioSource button;
    public AudioClip woodCrash;



    public void Awake()
    {
        quitButton.SetBool("Exit", false);
    }

    public void ExitAnim()
    {
        quitButton.SetBool("Exit", true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    public void PlaySound()
    {
        button.PlayOneShot(buttonPress);
    }

    public void Crash()
    {
        button.PlayOneShot(woodCrash);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }



}
