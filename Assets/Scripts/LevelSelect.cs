//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public int currentLevel = 0;
    public Animator canvus;
    public AudioSource buttonLvL1;
    public AudioSource buttonTut;
    public AudioClip selection;


    public void loadLevel()
    {
        if (currentLevel == 1)
        {
            SceneManager.LoadScene("Greybox (Tutoral)");
        }
        else if (currentLevel == 2)
        {
            SceneManager.LoadScene("Level 1 Test");
        }
    }

    public void tut()
    {
        currentLevel = 1;
        canvus.SetBool("Fade", true);
        buttonTut.PlayOneShot(selection);
    }

    public void level1Load()
    {
        currentLevel = 2;
        canvus.SetBool("Fade", true);
        buttonLvL1.PlayOneShot(selection);
    }



}
