// http://chrisrolfs.com/hide-objects-blocking-player-view/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{
	public Transform WatchTarget;
	public Transform[] RaycastPoints;
	public LayerMask OccluderMask;
	public Renderer[] rend0;
	public Renderer[] rend1;
	public Renderer[] rend2;
	public Renderer[] rend3;
	public Renderer[] rend4;
	public Renderer[] rend5;
	public Renderer[] rend6;
	public Renderer[] tempRend0;
	public Renderer[] tempRend1;
	public Renderer[] tempRend2;
	public Renderer[] tempRend3;
	public Renderer[] tempRend4;
	public Renderer[] tempRend5;
	public Renderer[] tempRend6;
	Color color;
	[HideInInspector]
	// Used to change the aplha of walls in another script.
	public float m_fChangeTime = 0.0f;
	public float m_fCamDistance = 10;
	Camera m_camera;
	public bool[] m_bHit;

	void Start()
	{
		m_camera = Camera.main;
	}

	void Update()
	{
		
		RayCastMaterialChange();
	
		for (int i = 0; i < RaycastPoints.Length; ++i)
		{
			Debug.DrawRay(RaycastPoints[i].position, WatchTarget.transform.position - RaycastPoints[i].position, Color.green);
		}

	}

	void RayCastMaterialChange()
	{
		// Think of a way to put this in a function which can add any amount of raycasts and renderers etc,
		// may need to create moore arrays for example a bbol array in the changemat finction.
		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  RaycastPoints[0].position,
		  WatchTarget.transform.position - RaycastPoints[0].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		m_bHit[0] = RayHit(hits, m_bHit[0]);
		rend0 = RendSet(hits, m_bHit[0], rend0);
		tempRend0 = ChangeMaterial(hits, m_bHit[0], tempRend0, rend0);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits1 = Physics.RaycastAll(
		  RaycastPoints[1].position,
		  WatchTarget.transform.position - RaycastPoints[1].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[1] = RayHit(hits1, m_bHit[1]);
		rend1 = RendSet(hits1, m_bHit[1], rend1);
		tempRend1 = ChangeMaterial(hits1, m_bHit[1], tempRend1, rend1);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits2 = Physics.RaycastAll(
		  RaycastPoints[2].position,
		  WatchTarget.transform.position - RaycastPoints[2].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[2] = RayHit(hits2, m_bHit[2]);
		rend2 = RendSet(hits2, m_bHit[2], rend2);
		tempRend2 = ChangeMaterial(hits2, m_bHit[2], tempRend2, rend2);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits3 = Physics.RaycastAll(
		  RaycastPoints[3].position,
		  WatchTarget.transform.position - RaycastPoints[3].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[3] = RayHit(hits3, m_bHit[3]);
		rend3 = RendSet(hits3, m_bHit[3], rend3);
		tempRend3 = ChangeMaterial(hits3, m_bHit[3], tempRend3, rend3);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits4 = Physics.RaycastAll(
		  RaycastPoints[4].position,
		  WatchTarget.transform.position - RaycastPoints[4].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[4] = RayHit(hits4, m_bHit[4]);
		rend4 = RendSet(hits4, m_bHit[4], rend4);
		tempRend4 = ChangeMaterial(hits4, m_bHit[4], tempRend4, rend4);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits5 = Physics.RaycastAll(
		  RaycastPoints[5].position,
		  WatchTarget.transform.position - RaycastPoints[5].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[5] = RayHit(hits5, m_bHit[5]);
		rend5 = RendSet(hits5, m_bHit[5], rend5);
		tempRend5 = ChangeMaterial(hits5, m_bHit[5], tempRend5, rend5);

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits6 = Physics.RaycastAll(
		  RaycastPoints[6].position,
		  WatchTarget.transform.position - RaycastPoints[6].position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		m_bHit[6] = RayHit(hits6, m_bHit[6]);
		rend6 = RendSet(hits6, m_bHit[6], rend6);
		tempRend6 = ChangeMaterial(hits6, m_bHit[6], tempRend6, rend6);
	}

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
	Renderer[] RendSet(RaycastHit[] hits, bool bHit, Renderer[] rend)
	{
		if (hits.Length > 0)
		{
			rend = new Renderer[hits.Length];
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget)
				{
					//rend = new Renderer[hits.Length];
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
			//May need to get the raycasts info and check bool to see if one of the rays are the same as the other if not then it should set the aplha back also may need more temprend and rednersrers.
			if (!bHit)
			{
				rend = new Renderer[hits.Length];
			}
			return rend;
		}
	}
	Renderer[] ChangeMaterial(RaycastHit[] hits, bool bHit, Renderer[] tempRend, Renderer[] rend)
	{
		// Loop through all overlapping objects and lerp between materials.
		// rend element 0 equals the first it object and never changes.
		if (hits.Length > 0)
		{
			//rend = new Renderer[hits.Length];
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget)
				{
					//rend = new Renderer[hits.Length];
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
								//rend[i] = hit.collider.gameObject.GetComponent<Renderer>();
								rend[i].material.SetFloat("_Mode", 3f);
								rend[i].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
								rend[i].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
								rend[i].material.SetInt("_ZWrite", 0);
								rend[i].material.DisableKeyword("_ALPHATEST_ON");
								rend[i].material.EnableKeyword("_ALPHABLEND_ON");
								rend[i].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
								rend[i].material.renderQueue = 3000;
								// Lerps between the original material and the hidden one.
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
			bool bTempRendSame0 = false;
			bool bTempRendSame1 = false;
			bool bTempRendSame2 = false;
			bool bTempRendSame3 = false;
			bool bTempRendSame4 = false;
			bool bTempRendSame5 = false;
			bool bTempRendSame6 = false;
			if (!bHit)
			{
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
				#region
				//if (tempRend[i])
				//{
				//	int tempRendCount0 = tempRend0.Length;
				//	int tempRendCount1 = tempRend1.Length;
				//	int tempRendCount2 = tempRend2.Length;
				//	int tempRendCount3 = tempRend3.Length;
				//	int tempRendCount4 = tempRend4.Length;
				//	int tempRendCount5 = tempRend5.Length;
				//	int tempRendCount6 = tempRend6.Length;
				//	if ((tempRend[i] == tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] == tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] == tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] == tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] == tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] == tempRend5[tempRendCount5] &&
				//	tempRend[i] != tempRend6[tempRendCount6]) ||
				//	(tempRend[i] != tempRend0[tempRendCount0] &&
				//	tempRend[i] != tempRend1[tempRendCount1] &&
				//	tempRend[i] != tempRend2[tempRendCount2] &&
				//	tempRend[i] != tempRend3[tempRendCount3] &&
				//	tempRend[i] != tempRend4[tempRendCount4] &&
				//	tempRend[i] != tempRend5[tempRendCount5] &&
				//	tempRend[i] == tempRend6[tempRendCount6]))
				//{
				#endregion

				if ((bTempRendSame0 &&
					!bTempRendSame1 &&
					!bTempRendSame2 &&
					!bTempRendSame3 &&
					!bTempRendSame4 &&
					!bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					bTempRendSame1 &&
					!bTempRendSame2 &&
					!bTempRendSame3 &&
					!bTempRendSame4 &&
					!bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					!bTempRendSame1 &&
					bTempRendSame2 &&
					!bTempRendSame3 &&
					!bTempRendSame4 &&
					!bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					!bTempRendSame1 &&
					!bTempRendSame2 &&
					bTempRendSame3 &&
					!bTempRendSame4 &&
					!bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					!bTempRendSame1 &&
					!bTempRendSame2 &&
					!bTempRendSame3 &&
					bTempRendSame4 &&
					!bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					!bTempRendSame1 &&
					!bTempRendSame2 &&
					!bTempRendSame3 &&
					!bTempRendSame4 &&
					bTempRendSame5 &&
					!bTempRendSame6) ||
					(!bTempRendSame0 &&
					!bTempRendSame1 &&
					!bTempRendSame2 &&
					!bTempRendSame3 &&
					!bTempRendSame4 &&
					!bTempRendSame5 &&
					bTempRendSame6))
				{
					for (int i = 0; i < tempRend.Length; ++i)
					{
						if(tempRend[i] != null)
						{
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






