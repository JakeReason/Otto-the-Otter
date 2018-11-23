//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas. NOT BEING USED
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField]
	// A list which holds clips which are used to play in order or randomly.
	private List<AudioClip> m_musicClips;

	[SerializeField]
	// A bool which sets the playlist to random.
	private bool m_bRandomPlay = false;

	// Audio scource used to play audio from the clip list.
	private AudioSource m_audioSource;

	// Used to keep track of the clips being played.
	private int m_nCurrentClipIndex = 0;

	//--------------------------------------------------------------------------------
	// Start used for initialization.
	//--------------------------------------------------------------------------------
	void Start()
	{
		// Gets refference to the audio source.
		m_audioSource = GetComponent<AudioSource>();
		// Sete loop to false.
		m_audioSource.loop = false;
	}

	//--------------------------------------------------------------------------------
	// Update is called once every frame. Plays audio clips based on the list and 
	// plays the next clip or can play random clips.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Checks if audio is playing.
		if (!m_audioSource.isPlaying)
		{
			// Used to track the next clip.
			AudioClip nextClip;
			// If the random playlist is true.
			if(m_bRandomPlay)
			{
				// Plays a random clip.
				nextClip = GetRandomClip();
			}
			else
			{
				// Plays the next song in the list.
				nextClip = GetNextClip();
			}
			// Sets the current clip index to the next clip.
			m_nCurrentClipIndex = m_musicClips.IndexOf(nextClip);
			// Sets the audio clip.
			m_audioSource.clip = nextClip;
			// Plays the audio clip.
			m_audioSource.Play();
		}
	}

	//--------------------------------------------------------------------------------
	// Gets a random clip to play.
	//--------------------------------------------------------------------------------
	private AudioClip GetRandomClip()
	{
		return m_musicClips[Random.Range(0, m_musicClips.Count)];
	}

	//--------------------------------------------------------------------------------
	// Gets the next clip to play.
	//--------------------------------------------------------------------------------
	private AudioClip GetNextClip()
	{
		return m_musicClips[(m_nCurrentClipIndex + 1) % m_musicClips.Count];
	}

	//--------------------------------------------------------------------------------
	// Awake used for initialization. Used to not destroy the object on load.
	//--------------------------------------------------------------------------------
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
