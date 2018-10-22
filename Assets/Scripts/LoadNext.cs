using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadNext : MonoBehaviour {

    public GameObject m_fadeToBlack;

    private DeathFade m_deathFade;

    private void Awake()
    {
        m_deathFade = m_fadeToBlack.GetComponent<DeathFade>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadetoDarknessAyy());

            m_deathFade.DoFadeIn();
        }
    }


    private IEnumerator FadetoDarknessAyy()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
