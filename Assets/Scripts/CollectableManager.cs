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

	[SerializeField]
	// 
	private float m_fWaitTimer = 2;

	[SerializeField]
	// 
	private Animator[] m_flowerAnimation;

	[SerializeField]
	// 
	private GameObject[] m_flowerAnimationUI;

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

	private bool m_bShowUI = false;
	private bool m_bShowFlowerUI = false;
	private bool m_bShowClamUI = false;
	private Vector3 m_v3FlowerUIOffScreenPos;
	private Vector3 m_v3ClamUIOffScreenPos;
	private Vector3 m_v3FlowerUIOriginalPos;
	private Vector3 m_v3ClamUIOriginalPos;

	[SerializeField]
	private float m_fClamMoveDistance;
	[SerializeField]
	private float m_fFlowerMoveDistance;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		m_v3FlowerUIOriginalPos = m_flowerUI.transform.position;
		m_v3ClamUIOriginalPos = m_clamUI.transform.position;
		m_audioSource = gameObject.GetComponent<AudioSource>();
		ResetClams();
		m_clamText.text = m_fClams + "/" + m_fNextLife;
		m_flowerText.text = m_fFlowersCollected + "";
		m_flowerUI.SetActive(false);
		m_clamUI.SetActive(false);
		m_flowerUI.transform.position = new Vector3(m_flowerUI.transform.position.x + -m_fFlowerMoveDistance, m_flowerUI.transform.position.y, m_flowerUI.transform.position.z);
		m_clamUI.transform.position = new Vector3(m_clamUI.transform.position.x + m_fClamMoveDistance, m_clamUI.transform.position.y, m_clamUI.transform.position.z);
		m_v3FlowerUIOffScreenPos = m_flowerUI.transform.position;
		m_v3ClamUIOffScreenPos = m_clamUI.transform.position;
		for (int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			//m_flowerAnimation[i] = m_flowerAnimationUI[i].GetComponent<Animator>();
			m_flowerAnimation[i].enabled = false;
		}
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, this updates the UI to match the collectables.
	//--------------------------------------------------------------------------------
	// TODO: may need to change ui for clam count.
	void Update()
	{
		m_fUITimer += Time.deltaTime;
		for (int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			if (XCI.GetButtonDown(XboxButton.Back) || Input.GetKeyDown(KeyCode.KeypadEnter))
			{
				m_fUITimer = 0;
				m_flowerUI.SetActive(true);
				m_clamUI.SetActive(true);
				m_bShowUI = true;
				m_flowerAnimation[i].enabled = false;
			}
			if (m_fUITimer >= m_fWaitTimer)
			{
				m_flowerUI.SetActive(false);
				m_clamUI.SetActive(false);
				//m_flowerAnimation[i].enabled = true;
			}
		}
		if ((m_bShowUI || m_bShowClamUI || m_bShowFlowerUI) && (m_fUITimer <= m_fWaitTimer / 2))
		{
			m_clamUI.transform.position = Vector3.MoveTowards(m_clamUI.transform.position, m_v3ClamUIOriginalPos, 10);
			m_flowerUI.transform.position = Vector3.MoveTowards(m_flowerUI.transform.position, m_v3FlowerUIOriginalPos, 15);
		}
		if ((m_bShowUI || m_bShowClamUI || m_bShowFlowerUI) && (m_fUITimer >= m_fWaitTimer / 2))
		{
			m_clamUI.transform.position = Vector3.MoveTowards(m_clamUI.transform.position, m_v3ClamUIOffScreenPos, 10);
			m_flowerUI.transform.position = Vector3.MoveTowards(m_flowerUI.transform.position, m_v3FlowerUIOffScreenPos, 10);
		}
		if(m_fUITimer >= m_fWaitTimer)
		{
			m_bShowUI = false;
			m_bShowClamUI = false;
			m_bShowFlowerUI = false;
			m_flowerUI.SetActive(false);
			m_clamUI.SetActive(false);
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
		m_clamText.text = m_fClams + "/" + m_fNextLife;
		m_fUITimer = 0;
		m_bShowClamUI = true;
		m_clamUI.SetActive(true);
		if (m_fClams >= m_fNextLife)
		{
			m_fLives += 1.0f;
			m_audioSource.PlayOneShot(m_newLifeAudioClip);
			ResetClams();
		}
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
		for(int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			m_flowerAnimation[i].enabled = false;
		}
		m_fFlowersCollected += 1;
		m_flowerAnimation[nFlowerToCollect].enabled = true;
		m_audioSource.PlayOneShot(m_flowerPickAudioClip);
		m_collectedFlowerImages[nFlowerToCollect].enabled = true;
		m_notCollectedflowerImages[nFlowerToCollect].enabled = false;
		m_flowerText.text = m_fFlowersCollected + "";
		m_fUITimer = 0;
		m_flowerUI.SetActive(true);
		m_bShowFlowerUI = true;
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
