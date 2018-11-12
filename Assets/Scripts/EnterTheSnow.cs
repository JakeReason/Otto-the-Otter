using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class EnterTheSnow : MonoBehaviour {

    public GameObject snowDome;
    public ParticleSystem snow;
    public GameObject leafs;
    public AudioSource summer;
    public AudioSource winter;
    public GameObject camera;
    public PostProcessingProfile m_postProcessingProfile;
    private PostProcessingBehaviour m_postBehaviour;
    public GameObject cameraFrost;
    private FrostEffect cameraFrostScript;
    public Animator frostedAnim;

    // Use this for initialization
    void Start () {
        frostedAnim.SetBool("Base", true);
        winter.enabled = false;
        snowDome.SetActive(false);
        m_postBehaviour = camera.GetComponent<PostProcessingBehaviour>();
        cameraFrostScript = cameraFrost.GetComponent<FrostEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        frostedAnim.SetBool("Frosty", true);
        cameraFrostScript.GetComponent<FrostEffect>().enabled = true;
        summer.enabled = false;
        winter.enabled = true;
        snowDome.SetActive(true);
        snow.Play();
        leafs.SetActive(false);
    }

    public void winterShadder()
    {
        //cameraFrostScript.GetComponent<FrostEffect>().enabled = false;
        m_postBehaviour.profile = m_postProcessingProfile;
    }

    public void ByeBye()
    {
        cameraFrostScript.GetComponent<FrostEffect>().enabled = false;
        Destroy(this);
    }

}
