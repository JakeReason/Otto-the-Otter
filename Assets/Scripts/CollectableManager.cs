//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class CollectableManager : MonoBehaviour
{
	[SerializeField]
	// Amount of Clams collected.
	float m_fClams;

	[SerializeField]
	// Amount of Clams collected.
	float m_fFlowersCollected;

	[SerializeField]
	// Amount of Lives collected.
	float m_fLives;

	[SerializeField]
	// Amount untill next Life.
	float m_fNextLife;

	[SerializeField]
	// Used to update the UI counter for the Clams.
	private Text m_clamText;

	[SerializeField]
	// Used to update the UI counter for the Clams.
	private Text m_flowerText;

	[SerializeField]
	// Used to change the family member UI image.
	private Image[] m_notCollectedflowerImages;

	[SerializeField]
	// Used to change the flower UI image.
	private Image[] m_collectedFlowerImages;

	[SerializeField]
	// 
	private GameObject m_flowerUI;

	[SerializeField]
	// 
	private GameObject m_clamUI;

	[SerializeField]
	// 
	private float m_fUITimer;

	// An array of GameObjects which will keep track of the flowers.
	//public GameObject[] m_flowers;

	public Transform m_checkPoint;

	[SerializeField]
	private AudioClip m_flowerPickAudioClip;
	[SerializeField]
	private AudioClip m_clamPickAudioClip;
	[SerializeField]
	private AudioClip m_newLifeAudioClip;
	[SerializeField]
	private AudioClip m_checkPointAudioClip;

	private AudioSource m_audioSource;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		m_audioSource = gameObject.GetComponent<AudioSource>();
		ResetClams();
		UpdateUI();
		m_flowerUI.SetActive(false);
		m_clamUI.SetActive(false);
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, this updates the UI to match the collectables.
	//--------------------------------------------------------------------------------
	// TODO: may need to change ui for clam count.
	void Update()
	{
		m_fUITimer += Time.deltaTime;
		if (XCI.GetButtonDown(XboxButton.Back))
		{
			m_fUITimer = 0;
			m_flowerUI.SetActive(true);
			m_clamUI.SetActive(true);
		}
		if(m_fUITimer >= 2)
		{
			m_flowerUI.SetActive(false);
			m_clamUI.SetActive(false);
		}
	}

	public void UpdateUI()
	{
		// Changes the ui here when that gets around to bein done.
		m_clamText.text = m_fClams + "/" + m_fNextLife;
		m_flowerText.text = m_fFlowersCollected + "";

		m_fUITimer = 0;
		m_flowerUI.SetActive(true);
		m_clamUI.SetActive(true);

		if (m_fClams >= m_fNextLife)
		{
			m_fLives += 1.0f;
			m_audioSource.PlayOneShot(m_newLifeAudioClip);
			ResetClams();
		}
	}

	//--------------------------------------------------------------------------------
	// Used to add Clams.
	//
	// Param:
	//		AmountAdded: set amount of Clams to add from the player's score.
	//--------------------------------------------------------------------------------
	public void AddClams(int AmountAdded)
	{
		// Adds an amount to the clam score.
		m_fClams += AmountAdded;
		m_audioSource.PlayOneShot(m_clamPickAudioClip);
	}

	//--------------------------------------------------------------------------------
	// Used to remove Clams.
	//
	// Param:
	//		AmountRemoved: set amount of Clams to remove from the player's score.
	//--------------------------------------------------------------------------------
	public void RemoveClams(int AmountRemoved)
	{
		// Removes an amount from the clam score.
		m_fClams -= AmountRemoved;
	}

	//--------------------------------------------------------------------------------
	// Used to reset all collectables back to 0.
	//--------------------------------------------------------------------------------
	public void ResetClams()
	{
		// Resets the clam count.
		m_fClams = 0;
	}

	public void RemoveLife()
	{
		m_fLives -= 1;
	}

	//--------------------------------------------------------------------------------
	// Used to add flowers to the collectable manager and change the flower
	// image to found.
	//
	// Param:
	//		nFlowerToCollect: used to determine which has been found.
	//--------------------------------------------------------------------------------
	public void AddFlower(int nFlowerToCollect)
	{
		m_fFlowersCollected += 1;
		m_audioSource.PlayOneShot(m_flowerPickAudioClip);
		m_collectedFlowerImages[nFlowerToCollect].enabled = true;
		m_notCollectedflowerImages[nFlowerToCollect].enabled = false;
	}

	//--------------------------------------------------------------------------------
	// Used to set which flowers the image should display for the level.
	//
	// Param:
	//		nFlowerToFind: used to set which flower image is displayed.
	//--------------------------------------------------------------------------------
	public void ResetFlowersToFind(int nFlowerToFind)
	{
		for (int i = 0; i < m_collectedFlowerImages.Length; ++i)
		{
			m_notCollectedflowerImages[i].enabled = true;
			m_collectedFlowerImages[i].enabled = false;
		}
	}

	public void SetFlowerToFind(int nFlowerToFind)
	{
		m_notCollectedflowerImages[nFlowerToFind].enabled = true;
		m_collectedFlowerImages[nFlowerToFind].enabled = false;
	}

	public void SetCurrentCheckPoint(Transform checkPoint)
	{
		m_checkPoint = checkPoint;
		m_audioSource.PlayOneShot(m_checkPointAudioClip);
	}

	public float GetLives()
	{
		return m_fLives;
	}

	public Transform GetCheckPoint()
	{
		return m_checkPoint;
	}
}
