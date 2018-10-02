using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class MusicManager : MonoBehaviour
{
	[SerializeField]
	private List<AudioClip> m_musicClips;
	[SerializeField]
	private bool m_bRandomPlay = false;

	private AudioSource m_audioSource;
	private int m_nCurrentClipIndex = 0;

	// Use this for initialization
	void Start()
	{
		m_audioSource = GetComponent<AudioSource>();
		m_audioSource.loop = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(!m_audioSource.isPlaying)
		{
			AudioClip nextClip;
			if(m_bRandomPlay)
			{
				nextClip = GetRandomClip();
			}
			else
			{
				nextClip = GetNextClip();
			}
			//m_nCurrentClipIndex = ArrayUtility.IndexOf(m_musicClips, nextClip);
			m_nCurrentClipIndex = m_musicClips.IndexOf(nextClip);
			m_audioSource.clip = nextClip;
			m_audioSource.Play();
		}
	}

	private AudioClip GetRandomClip()
	{
		return m_musicClips[Random.Range(0, m_musicClips.Count)];
	}

	private AudioClip GetNextClip()
	{
		return m_musicClips[(m_nCurrentClipIndex + 1) % m_musicClips.Count];
	}
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
