using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeOut : MonoBehaviour {

    public Animator animator;

    private int levelToLoad;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PLayer")
        {

        }
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }


}
