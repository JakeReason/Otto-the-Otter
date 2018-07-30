// http://chrisrolfs.com/hide-objects-blocking-player-view/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideObjects : MonoBehaviour
{

	public Transform WatchTarget;
	public LayerMask OccluderMask;
	//This is the material with the Transparent/Diffuse With Shadow shader
	public Material HiderMaterial;
	public Material WallMaterial;
	public float duration = 2.0F;
	public Renderer rend;
	private Dictionary<Transform, Material> _LastTransforms;
	float lerp = 0.0f;
	void Start()
	{
		_LastTransforms = new Dictionary<Transform, Material>();
	}

	void Update()
	{
		
		//reset and clear all the previous objects
		if (_LastTransforms.Count > 0)
		{
			foreach (Transform t in _LastTransforms.Keys)
			{
				t.GetComponent<MeshRenderer>().material = _LastTransforms[t];
				rend.material = WallMaterial;
			}
			_LastTransforms.Clear();
		}

		//Cast a ray from this object's transform to the watch target's transform.
		RaycastHit[] hits = Physics.RaycastAll(
		  transform.position,
		  WatchTarget.transform.position - transform.position,
		  Vector3.Distance(WatchTarget.transform.position, transform.position),
		  OccluderMask
		);

		//Loop through all overlapping objects and disable their mesh renderer
		if (hits.Length > 0)
		{
			foreach (RaycastHit hit in hits)
			{
				if (hit.collider.gameObject.transform != WatchTarget && hit.collider.transform.root != WatchTarget)
				{
					lerp = GetComponent<CameraCollision2>().m_fChangeTime;
					_LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<MeshRenderer>().material);
					rend = hit.collider.gameObject.GetComponent<Renderer>();
					rend.material.Lerp(WallMaterial, HiderMaterial, lerp);
				}
			}
		}
	}
}