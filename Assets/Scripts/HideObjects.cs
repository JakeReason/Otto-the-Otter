//--------------------------------------------------------------------------------
// Author: Jeremy Zoitas.
//--------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{
	// The transform of the player or what it is looking at.
	public Transform WatchTarget;

	// An array of transform used create the raycasts.
	public Transform[] RaycastPoints;

	// The layers that the transperancey should work on.
	public LayerMask OccluderMask;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend0;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend1;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend2;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend3;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend4;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend5;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] rend6;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend0;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend1;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend2;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend3;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend4;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend5;

	// Array of renderers attached to the object that the raycast hits.
	public Renderer[] tempRend6;

	// Stores the colour that it will change to.
	Color color;

	// Array of bools whether th ray cast hits anything.
	public bool[] m_bHit;

	//--------------------------------------------------------------------------------
	// Awake used for initialization.
	//--------------------------------------------------------------------------------
	void Awake()
	{
	}

	//--------------------------------------------------------------------------------
	// Update is called once per frame. Changes the objects colour if it is hiting the 
	// raycast.
	//--------------------------------------------------------------------------------
	void Update()
	{
		// Does a raycast for each transform point in the array and checks if an
		// object is being hit and if it is apart of one of the layers then change its
		// colour.
		RayCastMaterialChange();
		
		// Used to draw the raycasts.
		for (int i = 0; i < RaycastPoints.Length; ++i)
		{
			Debug.DrawRay(RaycastPoints[i].position, WatchTarget.transform.position - RaycastPoints[i].position, Color.green);
		}

	}

	//--------------------------------------------------------------------------------
	// Does a raycast for each transform point in the array and checks if an object is
	// being hit and if it is apart of one of the layers then change its colour.
	//--------------------------------------------------------------------------------
	void RayCastMaterialChange()
	{
		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  RaycastPoints[0].position,
		  WatchTarget.transform.position - RaycastPoints[0].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[0] = RayHit(hits, m_bHit[0]);
		// Gets the renderers from the ray cast and stores them.
		rend0 = RendSet(hits, m_bHit[0], rend0);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend0 = ChangeMaterial(hits, m_bHit[0], tempRend0, rend0);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits1 = Physics.RaycastAll(
		  RaycastPoints[1].position,
		  WatchTarget.transform.position - RaycastPoints[1].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[1] = RayHit(hits1, m_bHit[1]);
		// Gets the renderers from the ray cast and stores them.
		rend1 = RendSet(hits1, m_bHit[1], rend1);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend1 = ChangeMaterial(hits1, m_bHit[1], tempRend1, rend1);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits2 = Physics.RaycastAll(
		  RaycastPoints[2].position,
		  WatchTarget.transform.position - RaycastPoints[2].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[2] = RayHit(hits2, m_bHit[2]);
		// Gets the renderers from the ray cast and stores them.
		rend2 = RendSet(hits2, m_bHit[2], rend2);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend2 = ChangeMaterial(hits2, m_bHit[2], tempRend2, rend2);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits3 = Physics.RaycastAll(
		  RaycastPoints[3].position,
		  WatchTarget.transform.position - RaycastPoints[3].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[3] = RayHit(hits3, m_bHit[3]);
		// Gets the renderers from the ray cast and stores them.
		rend3 = RendSet(hits3, m_bHit[3], rend3);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend3 = ChangeMaterial(hits3, m_bHit[3], tempRend3, rend3);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits4 = Physics.RaycastAll(
		  RaycastPoints[4].position,
		  WatchTarget.transform.position - RaycastPoints[4].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[4] = RayHit(hits4, m_bHit[4]);
		// Gets the renderers from the ray cast and stores them.
		rend4 = RendSet(hits4, m_bHit[4], rend4);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend4 = ChangeMaterial(hits4, m_bHit[4], tempRend4, rend4);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits5 = Physics.RaycastAll(
		  RaycastPoints[5].position,
		  WatchTarget.transform.position - RaycastPoints[5].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[5] = RayHit(hits5, m_bHit[5]);
		// Gets the renderers from the ray cast and stores them.
		rend5 = RendSet(hits5, m_bHit[5], rend5);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend5 = ChangeMaterial(hits5, m_bHit[5], tempRend5, rend5);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits6 = Physics.RaycastAll(
		  RaycastPoints[6].position,
		  WatchTarget.transform.position - RaycastPoints[6].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		// Calls rayhit which returns if something is being hit.
		m_bHit[6] = RayHit(hits6, m_bHit[6]);
		// Gets the renderers from the ray cast and stores them.
		rend6 = RendSet(hits6, m_bHit[6], rend6);
		// Gets the current renderer and checks if it is the same as last frame
		// if not then it changes the material alpha.
		tempRend6 = ChangeMaterial(hits6, m_bHit[6], tempRend6, rend6);
	}

	//--------------------------------------------------------------------------------
	// Does a raycast and sets the hit bool to true or false.
	//
	// Returns:
	//		  Returns true or false based on what it hits.
	//--------------------------------------------------------------------------------
	bool RayHit(RaycastHit[] hits, bool bHit)
	{
		if (hits.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//--------------------------------------------------------------------------------
	// Does a raycast and sets the renderer array based on what the object has 
	// attached to it.
	//
	// Param:
	//		hits: A raycast array of what is being hit.
	//		bHit: A bool which says if an object has been hit by the raycast or not.
	//		rend: A renderer array which stores what renderers the object has on it.
	//
	// Returns:
	//		  Returns the renderer array and what it hits.
	//--------------------------------------------------------------------------------
	Renderer[] RendSet(RaycastHit[] hits, bool bHit, Renderer[] rend)
	{
		if (hits.Length > 0)
		{
			// Used to resize the array.
			rend = new Renderer[hits.Length];
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget)
				{
					if (hit.collider.gameObject.GetComponent<Renderer>())
					{
						for (int i = 0; i < rend.Length; ++i)
						{
							if (hits[i].collider.gameObject == hit.collider.gameObject && hits.Length > 1)
							{
							}
							else
							{
								// Gets the hit gameObjects renderer.
								rend[i] = hit.collider.gameObject.GetComponent<Renderer>();
							}
						}
					}
				}
			}
			return rend;
		}
		else
		{
			if (!bHit)
			{
				rend = new Renderer[hits.Length];
			}
			return rend;
		}
	}

	//--------------------------------------------------------------------------------
	// Changes the material of the object that gets hit by the raycast.
	//
	// Param:
	//		hits: A raycast array of what is being hit.
	//		bHit: A bool which says if an object has been hit by the raycast or not.
	//		rend: A renderer array which stores what renderers the object has on it.
	//
	// Returns:
	//		  Returns the temprend array and what it hits and the colour change.
	//--------------------------------------------------------------------------------
	Renderer[] ChangeMaterial(RaycastHit[] hits, bool bHit, Renderer[] tempRend, Renderer[] rend)
	{
		if (hits.Length > 0)
		{
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget)
				{
					if (hit.collider.gameObject.GetComponent<Renderer>())
					{
						for (int i = 0; i < rend.Length; ++i)
						{
							if (hits[i].collider.gameObject == hit.collider.gameObject && hits.Length > 1)
							{

							}
							else
							{
								// Changes the objects shader variables so the alpha can be changed.
								rend[i].material.SetFloat("_Mode", 3f);
								rend[i].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
								rend[i].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
								rend[i].material.SetInt("_ZWrite", 0);
								rend[i].material.DisableKeyword("_ALPHATEST_ON");
								rend[i].material.EnableKeyword("_ALPHABLEND_ON");
								rend[i].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
								rend[i].material.renderQueue = 3000;
								// Changes the aplha of the obejct.
								if (rend[i].material.color.a >= 0.5f)
								{
									color = rend[i].material.color;
									color.a -= 0.01f;
									rend[i].material.color = color;
								}
							}

						}
						for (int i = 0; i < tempRend.Length; ++i)
						{
							if (tempRend[i] != null)
							{
								if (i < rend.Length)
								{
									for (int j = 0; j < rend.Length; ++j)
									{
										if (tempRend[i] == rend[j])
										{
										}
									}
								}
								else
								{
									// Sets the shader variables back to the original.
									color = tempRend[i].material.color;
									color.a = 1.0f;
									tempRend[i].material.color = color;
									tempRend[i].material.SetFloat("_Mode", 0f);
									tempRend[i].material.renderQueue = 2001;
								}
							}
						}
					}
				}
			}
			tempRend = new Renderer[rend.Length];
			tempRend = rend;
			return tempRend;
		}
		else
		{
			// Bools used to determine if the same object is being hit by the raycast
			// or not and whether to change the aplha or not.
			bool bTempRendSame0 = false;
			bool bTempRendSame1 = false;
			bool bTempRendSame2 = false;
			bool bTempRendSame3 = false;
			bool bTempRendSame4 = false;
			bool bTempRendSame5 = false;
			bool bTempRendSame6 = false;
			if (!bHit)
			{
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend0.Length; ++j)
					{
						if (tempRend[i] == tempRend0[j])
						{
							bTempRendSame0 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend1.Length; ++j)
					{
						if (tempRend[i] == tempRend1[j])
						{
							bTempRendSame1 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend2.Length; ++j)
					{
						if (tempRend[i] == tempRend2[j])
						{
							bTempRendSame2 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend3.Length; ++j)
					{
						if (tempRend[i] == tempRend3[j])
						{
							bTempRendSame3 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend4.Length; ++j)
					{
						if (tempRend[i] == tempRend4[j])
						{
							bTempRendSame4 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend5.Length; ++j)
					{
						if (tempRend[i] == tempRend5[j])
						{
							bTempRendSame5 = true;
						}
					}
				}
				// Checks if the renderers are the same.
				for (int i = 0; i < tempRend.Length; ++i)
				{
					for (int j = 0; j < tempRend6.Length; ++j)
					{
						if (tempRend[i] == tempRend6[j])
						{
							bTempRendSame6 = true;
						}
					}
				}
				// If the renderers are the same then it changes the material back.
				if ((bTempRendSame0 && !bTempRendSame1 && !bTempRendSame2 && !bTempRendSame3 && !bTempRendSame4 && !bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && bTempRendSame1 && !bTempRendSame2 && !bTempRendSame3 && !bTempRendSame4 && !bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && !bTempRendSame1 && bTempRendSame2 && !bTempRendSame4 && !bTempRendSame3 && !bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && !bTempRendSame1 && !bTempRendSame2 && bTempRendSame3 && !bTempRendSame4 && !bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && !bTempRendSame1 && !bTempRendSame2 && !bTempRendSame3 && bTempRendSame4 && !bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && !bTempRendSame1 && !bTempRendSame2 && !bTempRendSame3 && !bTempRendSame4 && bTempRendSame5 && !bTempRendSame6) ||
					(!bTempRendSame0 && !bTempRendSame1 && !bTempRendSame2 && !bTempRendSame3 && !bTempRendSame4 && !bTempRendSame5 && bTempRendSame6))
				{
					for (int i = 0; i < tempRend.Length; ++i)
					{
						if(tempRend[i] != null)
						{
							// Changes the material back.
							color = tempRend[i].material.color;
							color.a = 1.0f;
							tempRend[i].material.color = color;
							tempRend[i].material.SetFloat("_Mode", 0f);
							tempRend[i].material.renderQueue = 2001;
							rend = new Renderer[hits.Length];
						}
					}
				}
			}
		}
		tempRend = new Renderer[rend.Length];
		tempRend = rend;
		return tempRend;
	}
}






