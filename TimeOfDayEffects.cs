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
		Debug.Log ("should see this once a minute");
		Color defaultColor = Color.blue;
		
//		defaultColor *= AICORE._IsItMax(_WorldTime_._GetMinutesR(), 0.0f, _WorldTime_.numMinutesPerDay);
		
		defaultColor *= AICORE._IsItMax(_WorldTime_._GetMinutesRM(), 0.0f, 60.0f);
		
		_SetBackGroundColor(defaultColor);
		yield return new WaitForSeconds(60.0f);
		
	}
}
