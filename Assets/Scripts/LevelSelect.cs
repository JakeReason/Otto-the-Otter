using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public int level1 = 0;
    public Animator canvus;


    public void loadLevel()
    {
        if (level1 == 1)
        {
            SceneManager.LoadScene("Greybox (Tutoral)");
        }
        else if (level1 == 2)
        {
            SceneManager.LoadScene("Level 1 Test");
        }
    }

    public void tut()
    {
        level1 = 1;
        canvus.SetBool("Fade", true);
    }

    public void level1Load()
    {
        level1 = 2;
        canvus.SetBool("Fade", true);
    }


    
}
