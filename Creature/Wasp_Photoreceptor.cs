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
	
	//Settings
	float fVisRayDistance = 240.0f;
	
	// Use this for initialization
	void Start () {
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	Color _GetColorFromRaycast(Vector3 dir) {
		Color outColor = Color.black;
		
		
		
		return outColor;
	}
	
}
