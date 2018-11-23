//--------------------------------------------------------------------------------
// Author: Jake Reason.
//--------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotShadow : MonoBehaviour {

	public GameObject spotShadow;
	public float maxSize = 1.0f;
	public float maxDistance = 10.0f;
	public float offset = 0.05f;
	public LayerMask meshLayer;

	private GameObject spotShadowInstance;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray downRay = new Ray (transform.position, Vector3.down); 
		if (spotShadowInstance == null) 
		{
			spotShadowInstance = Instantiate (spotShadow) as GameObject;
		}

		if (Physics.Raycast (downRay, out hit, maxDistance, meshLayer)) 
		{
			if (hit.collider.gameObject.GetComponent<MeshRenderer> ()) {
				spotShadowInstance.SetActive (true);

				spotShadowInstance.transform.position = hit.point + hit.normal * offset; 
				spotShadowInstance.transform.rotation = Quaternion.LookRotation (-hit.normal);

				float currentSize = Mathf.Lerp (maxSize, 0.0f, hit.distance / maxDistance);
				spotShadowInstance.transform.localScale = Vector3.one * currentSize;
			}
		}
		else		 
		{
			if (spotShadowInstance == null) 
			{
				spotShadowInstance = Instantiate (spotShadow) as GameObject;
			}
			spotShadowInstance.SetActive (false);	
		}
	}
}
