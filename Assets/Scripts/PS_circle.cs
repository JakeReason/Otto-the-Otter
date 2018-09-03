using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_circle : MonoBehaviour {

	public int frequency = 1;//repeat rate
	public float resoultion = 20;// amount of keys on the curved curve
	public float amplitude =1.0f;//min / max hieght of curve
	public float Zvalue = 0f; // for speed

	// Use this for initialization
	void Start () 
	{
		CreateCirlce ();	
	}

	void CreateCirlce()
	{
		ParticleSystem PS = GetComponent<ParticleSystem> ();
		var vel = PS.velocityOverLifetime;
		vel.enabled = true;
		vel.space = ParticleSystemSimulationSpace.Local;
		PS.startSpeed = 1f;
		vel.z = new ParticleSystem.MinMaxCurve (10.0f, Zvalue);

		AnimationCurve curveX = new AnimationCurve (); //create a new curve
		for(int i = 0; i<resoultion;i++)
		{
			float newtime = (i / (resoultion - 1));
			float myvalue = amplitude * Mathf.Sin (newtime * (frequency*2) * Mathf.PI);

			curveX.AddKey (newtime, myvalue);
		}
		vel.x = new ParticleSystem.MinMaxCurve (10.0f, curveX);


		AnimationCurve curveY = new AnimationCurve (); //create a new curve
		for(int i = 0; i<resoultion;i++)
		{
			float newtime = (i / (resoultion - 1));
			float myvalue = amplitude * Mathf.Cos (newtime * (frequency*2) * Mathf.PI);

			curveY.AddKey (newtime, myvalue);
		}
		vel.y = new ParticleSystem.MinMaxCurve (10.0f, curveY);
	}


}
