using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeOfDayEffects : MonoBehaviour {
	
	public Camera mainCamera;
	public List<Camera> _lCams = new List<Camera>();
	
	public Color desiredCameraBackGroundColor = new Color();
	
	// Use this for initialization
	void Awake () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void UpdateBackGroundColor(float overtimesecs) {
		
	}
	
	public void _SetBackGroundColor(Color col) {
		desiredCameraBackGroundColor = col;
	}
}
