using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostReciever : MonoBehaviour {
    public GameObject winterZone;
    private EnterTheSnow winterZoneScript;

    private void Start()
    {
        winterZoneScript = winterZone.GetComponent<EnterTheSnow>();
    }

    public void SwitchBackToCam()
    {
        winterZoneScript.backToCamera();
        winterZoneScript.winterShadder();
    }




}
