using UnityEngine;
using System.Collections;

public class Datacore : MonoBehaviour {
	
	public bool bDisplayAllMenus = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ToggleAllMenus(bool b) {
		bDisplayAllMenus = b;	
	}
	
	//Orientation
	static public void _MoveForward(GameObject obj, float fMult) {
		//move forward based on a multiplier
		obj.transform.Translate(fMult * Vector3.forward, Space.Self);
	}
	
	static public void _MoveForward(MonoBehaviour mono, float fMult) {
		//move forward based on a multiplier
		mono.transform.Translate(fMult * Vector3.forward, Space.Self);
	}
	
	static public Vector3 _GetForwardVector(GameObject obj) {
		return obj.transform.TransformDirection(Vector3.forward);	
	}
	static public Vector3 _GetForwardVector(MonoBehaviour mono) {
		return mono.transform.TransformDirection(Vector3.forward);	
	}
	
	static public Vector3 _GetRearWardVector(GameObject obj) {
		return obj.transform.TransformDirection((-1) * Vector3.forward);
	}
	
	static public Vector3 _GetRightVector(GameObject obj) {
		return obj.transform.TransformDirection(Vector3.right);
	}
	
	static public Vector3 _GetLeftVector(GameObject obj) {
		return obj.transform.TransformDirection((-1) * Vector3.right);
	}
	
	static public Vector3 _GetDownwardVector(GameObject obj) {
		return obj.transform.TransformDirection(-Vector3.up);
	}
	
	static public void _Yaw(GameObject obj, float fRotation) {
		//rotates obj around world up vector
		Vector3 axis = fRotation * Vector3.up;
		obj.transform.Rotate(axis, Space.Self);
	}
	
	static public void _Yaw(MonoBehaviour mono, float fRotation) {
		//rotates obj around world up vector
		Vector3 axis = fRotation * Vector3.up;
		mono.transform.Rotate(axis, Space.Self);
	}
	
	static public void _Pitch(GameObject obj, float fRotation) {
		//rotates obj around world right vector
		Vector3 axis = fRotation * Vector3.right;
		obj.transform.Rotate(axis, Space.Self);
	}
	
	static public void _Pitch(MonoBehaviour mono, float fRotation) {
		//rotates obj around world right vector
		Vector3 axis = fRotation * Vector3.right;
		mono.transform.Rotate(axis, Space.Self);
	}
	
	static public void _Roll(GameObject obj, float fRotation) {
		//rotates obj around world forward vector
		Vector3 axis = fRotation * Vector3.forward;
		obj.transform.Rotate(axis, Space.Self);
	}
	
	static public void _Roll(MonoBehaviour mono, float fRotation) {
		//rotates obj around world forward vector
		Vector3 axis = fRotation * Vector3.forward;
		mono.transform.Rotate(axis, Space.Self);
	}
}
