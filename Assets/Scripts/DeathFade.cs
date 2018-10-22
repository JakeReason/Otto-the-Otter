using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathFade : MonoBehaviour
{
	public float m_fWaitTime;
	float m_fTimer;
	public float alpha = 0;
	Color col;
	bool m_bDoFadeIn;
	bool m_bDoFadeOut;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(m_bDoFadeIn)
		{
			m_fTimer += Time.deltaTime;
			if (m_fTimer <= 2)
			{
				col = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_fTimer);
				GetComponent<Image>().color = col;
			}
			else if(m_fTimer >= m_fWaitTime)
			{
				m_bDoFadeIn = false;
			}
		}
		else if(m_bDoFadeOut)
		{
			m_fTimer -= Time.deltaTime;
			if (m_fTimer > 0)
			{
				col = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, m_fTimer);
				GetComponent<Image>().color = col;
			}
			else if (m_fTimer <= 0)
			{
				m_bDoFadeIn = false;
			}
		}
	}

	public void DoFadeIn()
	{
		m_bDoFadeIn = true;
	}

	public void DoFadeOut()
	{
		m_bDoFadeOut = true;
	}
}
