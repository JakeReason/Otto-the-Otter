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
	// Amount of sticks collected.
	int m_nSticks;

	[SerializeField]
	// Used to update the UI counter for the sticks.
	private Text m_text;

	[SerializeField]
	// Used to change the family member UI image.
	private Image m_image;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_fatherNotFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_fatherFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_motherNotFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_motherFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_grandfatherNotFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_grandfatherFoundImage;

	[SerializeField]
	// Used to change the family member UI sprite.
	private Sprite m_youngerbrotherNotFoundImage;

	[SerializeField]
	private Sprite m_youngerbrotherFoundImage;

	// An array of 4 GameObjects which will keep track of the family members.
	GameObject[] m_familyMembers = new GameObject[4];

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake ()
	{
		ResetCollectables();
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame, this updates the UI to match the collectables.
	//--------------------------------------------------------------------------------
	// TODO: may need to change ui for stick count.
	void Update ()
	{
		// Changes the ui here when that gets around to bein done.
		m_text.text = "Sticks:" + m_nSticks;
	}

	//--------------------------------------------------------------------------------
	// Used to add sticks.
	//
	// Param:
	//		AmountAdded: set amount of sticks to add from the player's score.
	//--------------------------------------------------------------------------------
	public void AddSticks(int AmountAdded)
	{
		// Adds an amount to the stick score.
		m_nSticks += AmountAdded;
	}

	//--------------------------------------------------------------------------------
	// Used to remove sticks.
	//
	// Param:
	//		AmountRemoved: set amount of sticks to remove from the player's score.
	//--------------------------------------------------------------------------------
	public void RemoveSticks(int AmountRemoved)
	{
		// Removes an amount from the stick score.
		m_nSticks -= AmountRemoved;
	}

	//--------------------------------------------------------------------------------
	// Used to reset all collectables back to 0.
	//--------------------------------------------------------------------------------
	public void ResetCollectables()
	{
		// Resets the stick count.
		m_nSticks = 0;
		// Resets the family members.
		for(int i = 0; i < m_familyMembers.Length; ++i)
		{
			m_familyMembers[i] = null;
		}
	}

	//--------------------------------------------------------------------------------
	// Used to add family members the collectable manager and change the family member
	// image to found.
	//
	// Param:
	//		FamilyMember: used to determine which family member has been found.
	//--------------------------------------------------------------------------------
	public void AddFamilyMember(GameObject FamilyMember)
	{
		// If the Father has been collected then store it in the array.
		// Also change the image from the not collect to collected.
		if (FamilyMember.tag == "Father")
		{
			m_familyMembers[0] = FamilyMember;
			m_image.sprite = m_fatherFoundImage;
		}
		// If the Mother has been collected then store it in the array.
		// Also change the image from the not collect to collected.
		if (FamilyMember.tag == "Mother")
		{
			m_familyMembers[1] = FamilyMember;
			m_image.sprite = m_motherFoundImage;
		}
		// If the Grandfather has been collected then store it in the array.
		// Also change the image from the not collect to collected.
		if (FamilyMember.tag == "Grandfather")
		{
			m_familyMembers[2] = FamilyMember;
			m_image.sprite = m_grandfatherFoundImage;
		}
		// If the Younger Brother has been collected then store it in the array.
		// Also change the image from the not collect to collected.
		if (FamilyMember.tag == "Younger Brother")
		{
			m_familyMembers[3] = FamilyMember;
			m_image.sprite = m_youngerbrotherFoundImage;
		}
	}

	//--------------------------------------------------------------------------------
	// Used to set which family member the image should display for the level.
	//
	// Param:
	//		FamilMember: used to set which family member image is displayed.
	//--------------------------------------------------------------------------------
	public void SetFamilyMemberToFind(int FamilyMember)
	{
		// Sets the image to the father which indicates what family member to find.
		if (FamilyMember == 1)
		{
			m_image.sprite = m_fatherNotFoundImage;
		}
		// Sets the image to the mother which indicates what family member to find.
		if (FamilyMember == 2)
		{
			m_image.sprite = m_motherNotFoundImage;
		}
		// Sets the image to the grandfather which indicates what family member to find.
		if (FamilyMember == 3)
		{
			m_image.sprite = m_grandfatherNotFoundImage;
		}
		// Sets the image to the younger brother which indicates what family member to find.
		if (FamilyMember == 4)
		{
			m_image.sprite = m_youngerbrotherNotFoundImage;
		}
	}
}
