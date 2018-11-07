using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFadeReciever : MonoBehaviour {

    public GameObject snowColider;
    private EnterTheSnow postSwitch;


    private void Awake()
    {
        postSwitch = snowColider.GetComponent <EnterTheSnow>();
    }

    public void StartSwitch()
    {
        postSwitch.winterShadder();
    }
}
