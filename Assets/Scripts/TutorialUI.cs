//--------------------------------------------------------------------------------
// Author: Max Turner.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    public GameObject helpUI;


    private void Awake()
    {
        helpUI.SetActive(false);
    }

    private void OnTriggerEnter()
    {
        helpUI.SetActive(true);
    }

    private void OnTriggerExit()
    {
        helpUI.SetActive(false);
    }
}
