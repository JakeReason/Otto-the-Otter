// http://chrisrolfs.com/hide-objects-blocking-player-view/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{
	public Transform WatchTarget;
	public LayerMask OccluderMask;
	public Renderer[] rend;
	public Renderer[] tempRend;
	Color color;
	[HideInInspector]
	// Used to change the aplha of walls in another script.
	public float m_fChangeTime = 0.0f;
	public float m_fCamDistance = 10;
	Camera m_camera;

	void Start()
	{
		m_camera = Camera.main;
	}

	void Update()
	{
		// Sets the new position for the camera.
		//transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * m_fDistance, m_fSmooth * Time.deltaTime);
		RayCastMaterialChange();
		Debug.DrawRay(m_camera.transform.position, WatchTarget.transform.position - transform.position, Color.green);
	}

	void RayCastMaterialChange()
	{
		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  m_camera.transform.position,
		  WatchTarget.transform.position - transform.position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		tempRend = new Renderer[rend.Length];
		tempRend = rend;
		// Loop through all overlapping objects and lerp between materials.
		// rend element 0 equals the first it object and never changes.
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
									for(int j = 0; j < rend.Length; ++j)
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
		}
		else
		{
			for (int i = 0; i < tempRend.Length; ++i)
			{
				if (tempRend[i])
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