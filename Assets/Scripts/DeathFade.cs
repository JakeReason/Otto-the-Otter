//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
	// Float used to wait a certan time before changing the fade.
	public float m_fWaitTime;

	// A timer which is used for fading in and out.
	float m_fTimer;

	// Used to change the colour of an image.
	Color col;

	// Used to check if fading in.
	bool m_bDoFadeIn;

	// Used to check if fading out.
	bool m_bDoFadeOut;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		m_fTimer = 1;
		DoFadeOut();
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. Fades an image in and out based on the 
	// bools.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Checks if fade in is true.
		if(m_bDoFadeIn)
		{
			// Increases the timer.
			m_fTimer += Time.deltaTime;
			// If the timer is over 2 seconds then change the image colour.
			if (m_fTimer <= 2)
			{
				// Changes the image colour to fade in.
				col = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_fTimer);
				GetComponent<Image>().color = col;
			}
			// If the timer is greater then the wait time then set fade in to false.
			else if(m_fTimer >= m_fWaitTime)
			{
				m_bDoFadeIn = false;
			}
		}
		// Checks if fade out is true.
		else if (m_bDoFadeOut)
		{
			// Decreases the timer.
			m_fTimer -= Time.deltaTime;
			// Checks if the timer is greater then zero.
			if (m_fTimer > 0)
			{
				// Changes the image colour to fade out.
				col = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_fTimer);
				GetComponent<Image>().color = col;
			}
			// If the timer is less then or equal to zero then set fade out to false.
			else if (m_fTimer <= 0)
			{
				m_bDoFadeOut = false;
			}
		}
	}

	//--------------------------------------------------------------------------------
	// Sets the fade in bool to true.
	//--------------------------------------------------------------------------------
	public void DoFadeIn()
	{
		m_bDoFadeIn = true;
	}

	//--------------------------------------------------------------------------------
	// Sets the fade out bool to true.
	//--------------------------------------------------------------------------------
	public void DoFadeOut()
	{
		m_bDoFadeOut = true;
	}
}
