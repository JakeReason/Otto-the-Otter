// http://chrisrolfs.com/hide-objects-blocking-player-view/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{
	public Transform WatchTarget;
	public LayerMask OccluderMask;
	public Material HiderMaterial;
	public Material HitMaterial;
	public float duration = 2.0F;
	public Renderer[] rend;
	private Dictionary<Transform, Material> _LastTransforms;
	float lerp = 0.0f;
	Color color;
	int m_fHitCount = 0;

	// Normalized local position.
	public Vector3 dollyDir;

	public float m_fDistance = 5;
	public float m_fSmooth = 10;

	[HideInInspector]
	// Used to change the aplha of walls in another script.
	public float m_fChangeTime = 0.0f;

	public float m_fCamDistance = 10;

	void Start()
	{
		// Sets the dolly direction.
		dollyDir = transform.localPosition.normalized;
		_LastTransforms = new Dictionary<Transform, Material>();
	}

	void Update()
	{
		// Sets the new position for the camera.
		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * m_fDistance, m_fSmooth * Time.deltaTime);
		UpdateChangeTime();
		ResetMaterials();
		RayCastMaterialChange();
	}

	void UpdateChangeTime()
	{
		// Used for raycast.
		RaycastHit Hit;
		// Sets forward vector to the transforms forward.
		Vector3 forward = transform.TransformDirection(Vector3.forward);

		// Starts increasing change time.
		m_fChangeTime += Time.deltaTime;
		// Makes sure change time does not go above 1.
		if (m_fChangeTime >= 1.0f)
		{
			m_fChangeTime = 1.0f;
		}

		// Checks if the camera has something in front of it.
		if (Physics.Raycast(transform.position, forward, out Hit, m_fCamDistance - 0.8f))
		{
		}
		else
		{
			// If not then set the change time to 0.
			m_fChangeTime = 0.0f;
		}
	}
	void ResetMaterials()
	{
		// Reset and clear all the previous objects and materials.
		if (_LastTransforms.Count > 0)
		{
			foreach (Transform t in _LastTransforms.Keys)
			{
				t.GetComponent<MeshRenderer>().material = _LastTransforms[t];
			}
			_LastTransforms.Clear();
		}
	}

	void RayCastMaterialChange()
	{
		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  transform.position,
		  WatchTarget.transform.position - transform.position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		//Debug.DrawRay(transform.position, WatchTarget.transform.position - transform.position);
		// Loop through all overlapping objects and lerp between materials.
		if (hits.Length > 0)
		{
			//rend = new Renderer[hits.Length];
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget /*&& hit.collider.transform.root != WatchTarget*/)
				{
					// lerp equals the change time from the camera collision so it goes from visible to half invisible.
					lerp = m_fChangeTime;
					if (hit.collider.gameObject.GetComponent<MeshRenderer>())
						_LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<MeshRenderer>().material);
					// Gets the hit gameObjects renderer.
					if (hit.collider.gameObject.GetComponent<Renderer>())
					{
						rend[m_fHitCount] = hit.collider.gameObject.GetComponent<Renderer>();
						rend[m_fHitCount].material.SetFloat("_Mode", 3f);
						rend[m_fHitCount].material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
						rend[m_fHitCount].material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
						rend[m_fHitCount].material.SetInt("_ZWrite", 0);
						rend[m_fHitCount].material.DisableKeyword("_ALPHATEST_ON");
						rend[m_fHitCount].material.EnableKeyword("_ALPHABLEND_ON");
						rend[m_fHitCount].material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
						rend[m_fHitCount].material.renderQueue = 3000;
						// Lerps between the original material and the hidden one.
						if (rend[m_fHitCount].material.color.a >= 0.5f)
						{
							color = rend[m_fHitCount].material.color;
							color.a -= 0.01f;
							rend[m_fHitCount].material.color = color;
						}
					}

					if (rend[m_fHitCount] && hit.collider.gameObject.GetComponent<MeshRenderer>() != rend[m_fHitCount])
					{
						color = rend[m_fHitCount].material.color;
						color.a = 1.0f;
						rend[m_fHitCount].material.color = color;
						rend[m_fHitCount].material.SetFloat("_Mode", 0f);
						rend[m_fHitCount].material.renderQueue = 2001;
					}
					m_fHitCount++;
				}
				m_fHitCount = hits.Length;
			}
		}
		else
		{
			for (int i = 0; i < rend.Length; ++i)
			{

				if (rend[i])
				{
					color = rend[i].material.color;
					color.a = 1.0f;
					rend[i].material.color = color;
					rend[i].material.SetFloat("_Mode", 0f);
					rend[i].material.renderQueue = 2001;
				}
			}
		}
	}
}