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
		StartCoroutine( CalcTimeOfDayColor() );
	}
	
	void UpdateBackGroundColor(float overtimesecs) {
		//Color newcol = new Color();
		
		mainCamera.backgroundColor = desiredCameraBackGroundColor;
	}
	
	public void _SetBackGroundColor(Color col) {
		desiredCameraBackGroundColor = col;
		UpdateBackGroundColor(1.0f);
	}
	
	IEnumerator CalcTimeOfDayColor() {
		yield return null;
		//Debug.Log ("should see this once a minute");
		//142 185 255
		Color defaultColor = new Color((142.0f/255.0f), (185.0f/255.0f), 1.0f);
		
//		defaultColor *= AICORE._IsItMax(_WorldTime_._GetMinutesR(), 0.0f, _WorldTime_.numMinutesPerDay);
		if(_WorldTime_._GetHoursRM() < 1.0f) {
			defaultColor *= AICORE._IsItMax(_WorldTime_._GetHoursRM(), 0.0f, 1.0f);
		} else if (_WorldTime_._GetHoursRM() > 12.0f) {
			defaultColor *= AICORE._IsItMin(_WorldTime_._GetHoursRM(), 12.0f, 16.0f);
		}
		
		
		_SetBackGroundColor(defaultColor);
		yield return null;
		
	}
}
