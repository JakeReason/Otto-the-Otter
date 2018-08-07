using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
	[SerializeField]
	int m_nSticks;
	[SerializeField]
	private Text m_text;
	[SerializeField]
	private Image m_image;
	[SerializeField]
	private Sprite m_fatherNotFoundImage;
	[SerializeField]
	private Sprite m_fatherFoundImage;
	[SerializeField]
	private Sprite m_motherNotFoundImage;
	[SerializeField]
	private Sprite m_motherFoundImage;
	[SerializeField]
	private Sprite m_grandfatherNotFoundImage;
	[SerializeField]
	private Sprite m_grandfatherFoundImage;
	[SerializeField]
	private Sprite m_youngerbrotherNotFoundImage;
	[SerializeField]
	private Sprite m_youngerbrotherFoundImage;

	GameObject[] m_familyMembers = new GameObject[4];

	// Use this for initialization
	void Awake ()
	{
		ResetCollectables();
	}
	
	// Update is called once per frame
	// TODO: may need to change ui for stick count.
	void Update ()
	{
		// Changes the ui here when that gets around to bein done.
		m_text.text = "Sticks:" + m_nSticks;
	}

	public void AddSticks(int AmountAdded)
	{
		// Adds an amount to the stick score.
		m_nSticks += AmountAdded;
	}

	public void RemoveSticks(int AmountRemoved)
	{
		// Removes an amount from the stick score.
		m_nSticks -= AmountRemoved;
	}

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
	// TODO: Add ui changes for all family members.
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
