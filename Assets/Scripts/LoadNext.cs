using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNext : MonoBehaviour {

    public GameObject m_fadeToBlack;
    public Animator endGameDoor;
    public GameObject freelookCamera;
    public GameObject endGameCamera;



    private DeathFade m_deathFade;


    private void Awake()
    {
        m_deathFade = m_fadeToBlack.GetComponent<DeathFade>();
        endGameCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //StartCoroutine(FadetoDarknessAyy());
            endGameDoor.SetBool("GameEnd", true);
            endGameCamera.SetActive(true);
            freelookCamera.SetActive(false);
        }
    }


    private IEnumerator FadetoDarknessAyy()
    {
        yield return new WaitForSeconds(1f);
        m_deathFade.DoFadeIn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
