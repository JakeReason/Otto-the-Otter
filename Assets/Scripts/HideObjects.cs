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
	public Renderer rend;
	private Dictionary<Transform, Material> _LastTransforms;
	float lerp = 0.0f;
	Color color;

	void Start()
	{
		_LastTransforms = new Dictionary<Transform, Material>();
	}

	void Update()
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

		// Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  transform.position,
		  WatchTarget.transform.position - transform.position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);
		Debug.DrawRay(transform.position, WatchTarget.transform.position - transform.position);
		// Loop through all overlapping objects and lerp between materials.
		if (hits.Length > 0)
		{
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget /*&& hit.collider.transform.root != WatchTarget*/)
				{
					// lerp equals the change time from the camera collision so it goes from visible to half invisible.
					lerp = GetComponent<CameraCollision>().m_fChangeTime;
					_LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<MeshRenderer>().material);
					// Gets the hit gameObjects renderer.
					rend = hit.collider.gameObject.GetComponent<Renderer>();
					rend.material.SetFloat("_Mode", 3f);
					rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
					rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					rend.material.SetInt("_ZWrite", 0);
					rend.material.DisableKeyword("_ALPHATEST_ON");
					rend.material.EnableKeyword("_ALPHABLEND_ON");
					rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					rend.material.renderQueue = 3000;
					// Lerps between the original material and the hidden one.
					if (rend.material.color.a >= 0.5f)
					{
						color = rend.material.color;
						color.a -= 0.01f;
						rend.material.color = color;
					}
				}
				
			}
		}
		else
		{
			if(rend)
			{
				color = rend.material.color;
				color.a = 1.0f;
				rend.material.color = color;
				rend.material.SetFloat("_Mode", 0f);
				rend.material.renderQueue = 2001;
			}
		}
	}
}