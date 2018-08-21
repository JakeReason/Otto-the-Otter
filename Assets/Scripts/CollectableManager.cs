//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private Text m_text;

	[SerializeField]
	// Used to change the family member UI image.
	private Image m_flowerImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite[] m_flowerSprites;

	// An array of 4 GameObjects which will keep track of the family members.
	public GameObject[] m_flowers;

	public Transform m_checkPoint;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		ResetClams();
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, this updates the UI to match the collectables.
	//--------------------------------------------------------------------------------
	// TODO: may need to change ui for stick count.
	void Update ()
	{
		// Changes the ui here when that gets around to bein done.
		m_text.text = "Clams:" + m_fClams;
		if(m_fClams >= m_fNextLife)
		{
			m_fLives += 1.0f;
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
		// Adds an amount to the stick score.
		m_fClams += AmountAdded;
	}

	//--------------------------------------------------------------------------------
	// Used to remove Clams.
	//
	// Param:
	//		AmountRemoved: set amount of Clams to remove from the player's score.
	//--------------------------------------------------------------------------------
	public void RemoveClams(int AmountRemoved)
	{
		// Removes an amount from the stick score.
		m_fClams -= AmountRemoved;
	}

	//--------------------------------------------------------------------------------
	// Used to reset all collectables back to 0.
	//--------------------------------------------------------------------------------
	public void ResetClams()
	{
		// Resets the stick count.
		m_fClams = 0;
	}

	public void RemoveLife()
	{
		m_fLives -= 1;
	}

	//--------------------------------------------------------------------------------
	// Used to add family members the collectable manager and change the family member
	// image to found.
	//
	// Param:
	//		FamilyMember: used to determine which family member has been found.
	//--------------------------------------------------------------------------------
	public void AddFlower(int nFlowerToCollect)
	{
		m_fFlowersCollected += 1;
		m_flowerImage.sprite = m_flowerSprites[nFlowerToCollect];
	}

	//--------------------------------------------------------------------------------
	// Used to set which family member the image should display for the level.
	//
	// Param:
	//		FamilMember: used to set which family member image is displayed.
	//--------------------------------------------------------------------------------
	public void SetCurrentFlowerToFind(int nFlowerToFind)
	{
		m_flowerImage.sprite = m_flowerSprites[nFlowerToFind];
	}

	public void SetCurrentCheckPoint(Transform nCheckPoint)
	{
		m_checkPoint = nCheckPoint;
	}

	public float GetLives()
	{
		return m_fLives;
	}
}
