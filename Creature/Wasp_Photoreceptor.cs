using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wasp_Photoreceptor : MonoBehaviour {
	
	/*
	 * Wasp Photoreceptor (c) 2013 - Steve Miller
	 * 
	 * Keep this as simple as possible.
	 * Update WaspCore with the color we can see
	 */ 
	
	public Datacore dCore;
	public Wasp_Core wCore;
	
	public GameObject waspEye01;
	public GameObject waspEye02;
	
	
	public GameObject lastSeenObject;
	public RaycastHit currentHitInfo;
	
	public Color _cSeenColor;
	//create and invert layer mask so that raycasts will only collide on color
		LayerMask _layerMask;
	
	//Settings
	float fVisRayDistance = 240.0f;
	
	// Use this for initialization
	void Start () {
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core>();
		
		_layerMask = ~LayerMask.NameToLayer("Color");
	}
	
	// Update is called once per frame
	void Update () {
		wCore._UpdateCurrentColor(_cSeenColor = _GetColorFromRaycast(transform.position, transform.forward, out currentHitInfo));
		
		SetEyesToSeenColor();
	}
	
	Color _GetColorFromRaycast(Vector3 pos, Vector3 dir, out RaycastHit hitInfo) {
		Color outColor = Color.black;
		
		if(Physics.Raycast( pos, dir, out hitInfo, fVisRayDistance, _layerMask)) {
			outColor = hitInfo.collider.transform.gameObject.renderer.material.color;
		} 
		
		return outColor;
	}
	
	void SetEyeColor(Color c, GameObject eye) {
		eye.renderer.material.color = c;	
	}
	
	void SetEyesToSeenColor() {
		SetEyeColor(_cSeenColor, waspEye01);
		SetEyeColor(_cSeenColor, waspEye02);
	}
	
}
