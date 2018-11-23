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
	public float m_fFlowersCollected;

	[SerializeField]
	// Amount of Lives collected.
	float m_fLives;

	[SerializeField]
	// Amount untill next Life.
	float m_fNextLife;

	[SerializeField]
	// Used to update the UI counter for the Clams.
	private Text m_lifeText;

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
	// Used to change the UI of the collectable.
	private GameObject m_flowerUI;

	[SerializeField]
	// Used to change the UI of the collectable.
	private GameObject m_clamUI;

	[SerializeField]
	// Used to change the UI of the collectable.
	private GameObject m_lifeUI;

	[SerializeField]
	// Used to move the UI over time.
	private float m_fUITimer;

	[SerializeField]
	// The amount of time to wait before the UI moves back.
	private float m_fWaitTimer = 2;

	[SerializeField]
	// Used to animate the flower UI when picked up.
	private Animator[] m_flowerAnimation;

	[SerializeField]
	// GameObject which has the flower animations on them.
	private GameObject[] m_flowerAnimationUI;

	// Used to store what checkpoint transform to respawn at.
	public Transform m_checkPoint;

	// Audio scource used to play a sound when picked up.
	private AudioSource m_audioSource;

	// A bool used to determine if the UI should be shown.
	private bool m_bShowUI = false;

	// A bool used to determine if the UI should be shown.
	private bool m_bShowFlowerUI = false;

	// A bool used to determine if the UI should be shown.
	private bool m_bShowClamUI = false;

	// A bool used to determine if the UI should be shown.
	private bool m_bShowLifeUI = false;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3FlowerUIOffScreenPos;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3ClamUIOffScreenPos;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3LifeUIOffScreenPos;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3FlowerUIOriginalPos;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3ClamUIOriginalPos;

	// An offset of the UI Used to move it off screen.
	public Vector3 m_v3LifeUIOriginalPos;

	[SerializeField]
	// Float used to move the UI a certain distance.
	private float m_fClamMoveDistance;

	[SerializeField]
	// Float used to move the UI a certain distance.
	private float m_fFlowerMoveDistance;

	[SerializeField]
	// Float used to move the UI a certain distance.
	private float m_fLifeMoveDistance;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
		// Sets the UI original position.
		m_v3FlowerUIOriginalPos = m_flowerUI.transform.position;
		// Sets the UI original position.
		m_v3ClamUIOriginalPos = m_clamUI.transform.position;
		// Sets the UI original position.
		m_v3LifeUIOriginalPos = m_lifeUI.transform.position;
		// Gets reference to the audio source attached to the gameobject.
		m_audioSource = gameObject.GetComponent<AudioSource>();
		// Resets the clams.
		ResetClams();
		// Sets up the UI text.
		m_lifeText.text = m_fLives + "";
		// Sets up the UI text.
		m_clamText.text = m_fClams + "/" + m_fNextLife;
		// Sets up the UI text.
		m_flowerText.text = m_fFlowersCollected + "";
		// Sets the UI to false.
		m_flowerUI.SetActive(false);
		// Sets the UI to false.
		m_clamUI.SetActive(false);
		// Sets the UI to false.
		m_lifeUI.SetActive(false);
		// Sets the UI position of the screen at the start.
		m_flowerUI.transform.position = new Vector3(m_flowerUI.transform.position.x + -m_fFlowerMoveDistance, m_flowerUI.transform.position.y, m_flowerUI.transform.position.z);
		// Sets the UI position of the screen at the start.
		m_clamUI.transform.position = new Vector3(m_clamUI.transform.position.x + m_fClamMoveDistance, m_clamUI.transform.position.y, m_clamUI.transform.position.z);
		// Sets the UI position of the screen at the start.
		m_lifeUI.transform.position = new Vector3(m_lifeUI.transform.position.x + m_fLifeMoveDistance, m_lifeUI.transform.position.y, m_lifeUI.transform.position.z);
		// Sets the offscreen position to a variable for easier use.
		m_v3FlowerUIOffScreenPos = m_flowerUI.transform.position;
		// Sets the offscreen position to a variable for easier use.
		m_v3ClamUIOffScreenPos = m_clamUI.transform.position;
		// Sets the offscreen position to a variable for easier use.
		m_v3LifeUIOffScreenPos = m_lifeUI.transform.position;
		// Sets the UI animations to false.
		for (int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			m_flowerAnimation[i].enabled = false;
		}
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, this updates the UI to match the collectables.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Increases the UI timer.
		m_fUITimer += Time.deltaTime;
		// Updates the life text UI.
		m_lifeText.text = m_fLives + "";
		
		for (int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			// Checks if any of the following buttons have been pressed.
			if (XCI.GetButtonDown(XboxButton.Back) || Input.GetKeyDown(KeyCode.Tab) || XCI.GetButtonDown(XboxButton.Start) || Input.GetKeyDown(KeyCode.Escape))
			{
				// Sets UI timer to zero.
				m_fUITimer = 0;
				// Sets the UI to true.
				m_flowerUI.SetActive(true);
				// Sets the UI to true.
				m_clamUI.SetActive(true);
				// Sets the UI to true.
				m_lifeUI.SetActive(true);
				// Sets the UI to true.
				m_bShowUI = true;
				// Makes sure the animations dont play.
				m_flowerAnimation[i].enabled = false;
			}
			// If the UI timer is greater or equal to wait time then it Sets the UI to true.
			if (m_fUITimer >= m_fWaitTimer)
			{
				m_flowerUI.SetActive(false);
				m_clamUI.SetActive(false);
				m_lifeUI.SetActive(false);
			}
		}
		// Checks if the show UI is true and if the timer is less then the wait time divided by 2
		if ((m_bShowUI || m_bShowClamUI || m_bShowFlowerUI || m_bShowLifeUI) && (m_fUITimer <= m_fWaitTimer / 2))
		{
			// Moves the UI to the original position.
			m_clamUI.transform.position = Vector3.MoveTowards(m_clamUI.transform.position, m_v3ClamUIOriginalPos, 10);
			// Moves the UI to the original position.
			m_flowerUI.transform.position = Vector3.MoveTowards(m_flowerUI.transform.position, m_v3FlowerUIOriginalPos, 15);
			// Moves the UI to the original position.
			m_lifeUI.transform.position = Vector3.MoveTowards(m_lifeUI.transform.position, m_v3LifeUIOriginalPos, 10);
		}
		// Checks if the show UI is true and if the timer is greater then the wait time divided by 2
		if ((m_bShowUI || m_bShowClamUI || m_bShowFlowerUI || m_bShowLifeUI) && (m_fUITimer >= m_fWaitTimer / 2))
		{
			// Moves the UI to the offscreen position.
			m_clamUI.transform.position = Vector3.MoveTowards(m_clamUI.transform.position, m_v3ClamUIOffScreenPos, 10);
			// Moves the UI to the offscreen position.
			m_flowerUI.transform.position = Vector3.MoveTowards(m_flowerUI.transform.position, m_v3FlowerUIOffScreenPos, 10);
			// Moves the UI to the offscreen position.
			m_lifeUI.transform.position = Vector3.MoveTowards(m_lifeUI.transform.position, m_v3LifeUIOffScreenPos, 10);
		}
		// Checks if the timer is greater then the wait time.
		if (m_fUITimer >= m_fWaitTimer)
		{
			// Sets Show UI to false.
			m_bShowUI = false;
			// Sets Show UI to false.
			m_bShowClamUI = false;
			// Sets Show UI to false.
			m_bShowFlowerUI = false;
			// Sets Show UI to false.
			m_bShowLifeUI = false;
			// Sets the UI to false.
			m_flowerUI.SetActive(false);
			// Sets the UI to false.
			m_clamUI.SetActive(false);
			// Sets the UI to false.
			m_lifeUI.SetActive(false);
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
		// Updates the clam text UI.
		m_clamText.text = m_fClams + "/" + m_fNextLife;
		// Sets the UI timer to zero.
		m_fUITimer = 0;
		// Sets the Show UI to ture.
		m_bShowClamUI = true;
		// Sets the UI to ture.
		m_clamUI.SetActive(true);
		// Sets the Show UI to ture.
		m_bShowLifeUI = true;
		// Sets the UI to ture.
		m_lifeUI.SetActive(true);
		// Checks if the clams score is higher then the next life number. 
		if (m_fClams >= m_fNextLife)
		{
			// Adds a life when a certain amount of clams are gathered.
			m_fLives += 1.0f;
			// Updates the life UI text.
			m_lifeText.text = m_fLives + "";
			// Plays the sound of a new life.
			m_audioSource.Play();
			// Restes the clams.
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

	//--------------------------------------------------------------------------------
	// Removes a life.
	//--------------------------------------------------------------------------------
	public void RemoveLife()
	{
		// Removes a life from the player.
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
		// Disables the flower animations.
		for(int i = 0; i < m_flowerAnimation.Length; ++i)
		{
			if(m_flowerAnimation[i])
				m_flowerAnimation[i].enabled = false;
		}
		// Adds a flower.
		m_fFlowersCollected += 1;
		// Plays the current flower animation that has been picked up.
		m_flowerAnimation[nFlowerToCollect].enabled = true;
		// Changes the image UI of the flower which has been collected.
		m_collectedFlowerImages[nFlowerToCollect].enabled = true;
		// Turns off the old image which was displaying the flower.
		m_notCollectedflowerImages[nFlowerToCollect].enabled = false;
		// Updates the flower UI text.
		m_flowerText.text = m_fFlowersCollected + "";
		// Sets the Timer to zero.
		m_fUITimer = 0;
		// Sets the UI to true.
		m_flowerUI.SetActive(true);
		// Sets the show UI to true.
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

	//--------------------------------------------------------------------------------
	// Sets the flower which the player should find in the level.
	//
	//	Param:
	//		 nFlowerToFind: used to set which flower image is displayed.
	//--------------------------------------------------------------------------------
	public void SetFlowerToFind(int nFlowerToFind)
	{
		m_notCollectedflowerImages[nFlowerToFind].enabled = true;
		m_collectedFlowerImages[nFlowerToFind].enabled = false;
	}

	//--------------------------------------------------------------------------------
	// Sets the current checkpoint.
	//
	// Param:
	//		checkpoint: transform of the new checkpoint.
	//--------------------------------------------------------------------------------
	public void SetCurrentCheckPoint(Transform checkPoint)
	{
		m_checkPoint = checkPoint;
	}

	//--------------------------------------------------------------------------------
	// Gets the current amount of lives.
	//
	// Returns:
	//		  Returns the amount of lives.
	//--------------------------------------------------------------------------------
	public float GetLives()
	{
		return m_fLives;
	}

	//--------------------------------------------------------------------------------
	// Gets the current checkpoint.
	//
	// Returns:
	//		  Returns the current checkpoint which the player will spawn at.
	//--------------------------------------------------------------------------------
	public Transform GetCheckPoint()
	{
		return m_checkPoint;
	}

	//--------------------------------------------------------------------------------
	// Adds a life and shows the UI and updates it.
	//--------------------------------------------------------------------------------
	public void AddLife()
	{
		// Adds a life.
		m_fLives += 1.0f;
		// Updates the life text UI.
		m_lifeText.text = m_fLives + "";
		// Sets the timer to zero.
		m_fUITimer = 0;
		// Sets the UI to true.
		m_lifeUI.SetActive(true);
		// Sets the Show UI to true.
		m_bShowLifeUI = true;
	}
}
