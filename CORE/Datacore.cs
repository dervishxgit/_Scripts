using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Datacore : MonoBehaviour
{
	//temp test section
	//maybe we need floatwrappers on both sides
	float ftemp01;
	float ftemp02;
	List<_Condition_> listOfConditions = new List<_Condition_>();
	_Condition_ con01;
	_Condition_ con02;
	
	//Major Settings
	public bool bDisplayAllMenus = false;
	public float fWorldTimeScale = 2.0f;
	
	//Level list
	public static List<string> _AllLevels_ = new List<string> ();
	
	//Wasp Movement calibration settings
	
	
	//Stored World Waypoints
	public List<Transform> _lAllWaypoints = new List<Transform> ();
	
	//Wasps in World
	public List<Wasp_Core> _lAllWasps = new List<Wasp_Core> ();
	
	public void _RegisterWasp (Wasp_Core wasp)
	{
		_lAllWasps.Add (wasp);
	}
	
	public void _UnregisterWasp (Wasp_Core wasp)
	{
		_lAllWasps.Remove (wasp);
	}
	
	// Use this for initialization
	void Start ()
	{
		//test levels list
		//Debug.Log("calling scenes");
		//Debug.Log(_GetAllScenes());
		_PrintAllScenes ();
		
		//test of conditions creation
		ftemp01 = 1.0f;
		ftemp02 = 500.0f;
		con01 = new _Condition_("first condition", ref ftemp01, ref listOfConditions);
		con02 = new _Condition_("second condition", ref ftemp02, ref listOfConditions);
		
		foreach(_Condition_ con in listOfConditions) {
			Debug.Log("con name: " + con._GetName() + "con value: " + con._GetValue());
		}
		
		string name  = _Condition_._GetConditionByName("second condition", listOfConditions).name;
		Debug.Log(name);
	}
	
	// Update is called once per frame
	void Update ()
	{
		UnityEngine.Time.timeScale = fWorldTimeScale;
		//fWorldTimeScale += 0.1f;
		
		//test condition update
		con01.fValue += 0.1f;
		
		Debug.Log("con01 updated: " + con01._GetValue());
	}
	
	void ToggleAllMenus (bool b)
	{
		bDisplayAllMenus = b;	
	}
	
	//Levels
	public List<string> _GetAllScenes ()
	{
		List<string> strings = new List<string> ();
		
		for (int i = 0; i < _ListOfLevels_.levels.Length; i++) {
			strings.Add (_ListOfLevels_.levels [i]);
		}
		
		return strings;
	}
	
//	public string[] _GetAllScenes() {
//		//ReadSceneNames sceneNames = gameObject.GetComponent<ReadSceneNames>();
//		string[] scenes = new string[ReadSceneNames._AllScenes_.Length];
//		scenes = ReadSceneNames._AllScenes_;
//		return scenes;
//	}
	
	public void _PrintAllScenes ()
	{
//		foreach (string s in _AllLevels_) {
//			Debug.Log(s);
//		}
		
		for (int i = 0; i < _ListOfLevels_.levels.Length; i++) {
			Debug.Log (_ListOfLevels_.levels [i]);
		}
	}
	
	
	//Waypoints
	public void _AddWaypoint (Transform point)
	{
		//_lAllWaypoints.Add(point);
		if (_lAllWaypoints != null) {
			//Debug.Log("going to add waypoint to datacore");
			_lAllWaypoints.Add (point);
		}
		
	}
	
	public void _RemoveWaypoint (Transform point)
	{
		_lAllWaypoints.Remove (point);
	}
	
	public Transform _GetWaypoint (int point)
	{
		return _lAllWaypoints [point];
	}
	
	public Transform _ReturnRandomWaypoint ()
	{
		//count waypoints
		//choose random number from available, return waypoint at index
		if (_lAllWaypoints != null) {
			int numpoints = this._lAllWaypoints.Count;
			int selection = AICORE._RandomInteger (0, numpoints);
			return _lAllWaypoints [selection];
		} else
			return null;
	}
	
	//Orientation
	static public void _MoveForward (GameObject obj, float fMult)
	{
		//move forward based on a multiplier
		obj.transform.Translate (fMult * Vector3.forward, Space.Self);
	}
	
	static public void _MoveForward (MonoBehaviour mono, float fMult)
	{
		//move forward based on a multiplier
		mono.transform.Translate (fMult * Vector3.forward, Space.Self);
	}
	
	static public void _MoveUp (GameObject obj, float fMult)
	{
		obj.transform.Translate (fMult * Vector3.up, Space.Self);
	}
	
	static public void _MoveDown (GameObject obj, float fMult)
	{
		obj.transform.Translate (-fMult * Vector3.up, Space.Self);
	}
	
	static public Vector3 _GetForwardVector (GameObject obj)
	{
		return obj.transform.TransformDirection (Vector3.forward);	
	}

	static public Vector3 _GetForwardVector (MonoBehaviour mono)
	{
		return mono.transform.TransformDirection (Vector3.forward);	
	}
	
	static public Vector3 _GetRearWardVector (GameObject obj)
	{
		return obj.transform.TransformDirection ((-1) * Vector3.forward);
	}
	
	static public Vector3 _GetRightVector (GameObject obj)
	{
		return obj.transform.TransformDirection (Vector3.right);
	}
	
	static public Vector3 _GetLeftVector (GameObject obj)
	{
		return obj.transform.TransformDirection ((-1) * Vector3.right);
	}
	
	static public Vector3 _GetDownwardVector (GameObject obj)
	{
		return obj.transform.TransformDirection (-Vector3.up);
	}
	
	static public void _Yaw (GameObject obj, float fRotation)
	{
		//rotates obj around world up vector
		Vector3 axis = fRotation * Vector3.up;
		obj.transform.Rotate (axis, Space.Self);
	}
	
	static public void _Yaw (MonoBehaviour mono, float fRotation)
	{
		//rotates obj around world up vector
		Vector3 axis = fRotation * Vector3.up;
		mono.transform.Rotate (axis, Space.Self);
	}
	
	static public void _Pitch (GameObject obj, float fRotation)
	{
		//rotates obj around world right vector
		Vector3 axis = fRotation * Vector3.right;
		obj.transform.Rotate (axis, Space.Self);
	}
	
	static public void _Pitch (MonoBehaviour mono, float fRotation)
	{
		//rotates obj around world right vector
		Vector3 axis = fRotation * Vector3.right;
		mono.transform.Rotate (axis, Space.Self);
	}
	
	static public void _Roll (GameObject obj, float fRotation)
	{
		//rotates obj around world forward vector
		Vector3 axis = fRotation * Vector3.forward;
		obj.transform.Rotate (axis, Space.Self);
	}
	
	static public void _Roll (MonoBehaviour mono, float fRotation)
	{
		//rotates obj around world forward vector
		Vector3 axis = fRotation * Vector3.forward;
		mono.transform.Rotate (axis, Space.Self);
	}
	
	//additional orientation functions imported from RULECORE
	/*
	 * TODO: section must be updated with calibrations
	 * 		seektarget needs to be updated to be fully 3D
	 */ 
	static public void _RotateYaw (Component bot, float fTurnRate)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		bot.transform.Rotate (fTurnRate * Vector3.up);
	}
	
	static public void _RotatePitch (Component bot, float fTurnRate)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		bot.transform.Rotate (fTurnRate * Vector3.right, Space.World);		
	}
	
	static public void _RotateRoll (Component bot, float fTurnRate)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		bot.transform.Rotate (fTurnRate * Vector3.forward, Space.Self);
	}
	
	static public void _MoveForward (Component bot, float fVelocity)
	{
		if (fVelocity > 0.75f)
			fVelocity = 0.75f;
		if (fVelocity < -0.75f)
			fVelocity = -0.75f;
		bot.transform.Translate (fVelocity * Vector3.forward, Space.Self);
	}
	
	//Overloads of above orientations that adhere to the global timescale
	static public void _RotateYaw (Component bot, float fTurnRate, bool bUseTimeScale)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		
		if(bUseTimeScale) {
			bot.transform.Rotate (fTurnRate * Vector3.up * Time.deltaTime);
		} else {
			bot.transform.Rotate (fTurnRate * Vector3.up);
		}
		
	}
	
	static public void _RotatePitch (Component bot, float fTurnRate, bool bUseTimeScale)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		
		if (bUseTimeScale) {
			bot.transform.Rotate (fTurnRate * Vector3.right * Time.deltaTime, Space.World);		
		} else {
			bot.transform.Rotate (fTurnRate * Vector3.right, Space.World);	
		}
				
	}
	
	static public void _RotateRoll (Component bot, float fTurnRate, bool bUseTimeScale)
	{
		if (fTurnRate > 6.0f)
			fTurnRate = 6.0f;
		if (fTurnRate < -6.0f)
			fTurnRate = -6.0f;
		
		if (bUseTimeScale) {
			bot.transform.Rotate (fTurnRate * Vector3.forward * Time.deltaTime, Space.Self);
		} else {
			bot.transform.Rotate (fTurnRate * Vector3.forward, Space.Self);
		}
		
	}
	
	static public void _MoveForward (Component bot, float fVelocity, bool bUseTimeScale)
	{
		if (fVelocity > 0.75f)
			fVelocity = 0.75f;
		if (fVelocity < -0.75f)
			fVelocity = -0.75f;
		
		if (bUseTimeScale) {
			bot.transform.Translate (fVelocity * Vector3.forward * Time.deltaTime, Space.Self);
		} else {
			bot.transform.Translate (fVelocity * Vector3.forward, Space.Self);
		}
		
	}
	
	// _SeekTarget3D : Seeks out the indicated target and returns true when reached (adjusted from original version below, uses settings supplied above)
	static public bool _SeekTarget3D (Component bot, Vector3 target, float fMaxVelocity, bool bUseTimeScale)
	{
		float fTargetDistance;
		float zIsTargetBehindMe, zIsTargetInFrontOfMe, zIsTargetToMyLeft, zIsTargetToMyRight, zIsTargetAboveMe, zIsTargetBelowMe;
		AICORE._GetSpatialAwareness3D (bot, target, out fTargetDistance, 
			out zIsTargetBehindMe, out zIsTargetInFrontOfMe, 
			out zIsTargetToMyLeft, out zIsTargetToMyRight, 
			out zIsTargetAboveMe, out zIsTargetBelowMe);
		
		// Detect whether TARGET is sufficiently in front
		if (zIsTargetInFrontOfMe > 0.99) {
			// Satisfactally facing target	
			// No need to turn
		} else {
			// Should we turn right or left?
			if (zIsTargetToMyRight > zIsTargetToMyLeft) {
				// Turn right
				float fTurnRate;
				if (zIsTargetBehindMe > zIsTargetToMyRight) {
					fTurnRate = AICORE._Defuzzify (zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify (zIsTargetToMyRight, 0.0f, 6.0f);
				}
				_RotateYaw (bot, fTurnRate, bUseTimeScale);
			} else if (zIsTargetToMyLeft > zIsTargetToMyRight) {
				// Turn left
				float fTurnRate;
				if (zIsTargetBehindMe > zIsTargetToMyLeft) {
					fTurnRate = AICORE._Defuzzify (zIsTargetBehindMe, 0.0f, 6.0f);					
				} else {
					fTurnRate = AICORE._Defuzzify (zIsTargetToMyLeft, 0.0f, 6.0f);
				}
				_RotateYaw (bot, -fTurnRate, bUseTimeScale);
			}
			
			// Should we pitch up or down?
			if (zIsTargetAboveMe > zIsTargetBelowMe) {
				//pitch up
				float fPitchRate;
				if (zIsTargetBehindMe > zIsTargetAboveMe) {
					fPitchRate = AICORE._Defuzzify (zIsTargetBehindMe, 0.0f, 6.0f);
				} else {
					fPitchRate = AICORE._Defuzzify (zIsTargetAboveMe, 0.0f, 6.0f);
				}
				_RotatePitch (bot, fPitchRate, bUseTimeScale);
			} else if (zIsTargetBelowMe > zIsTargetAboveMe) {
				//pitch down
				float fPitchRate;
				if (zIsTargetBehindMe > zIsTargetBelowMe) {
					fPitchRate = AICORE._Defuzzify (zIsTargetBehindMe, 0.0f, 6.0f);
				} else {
					fPitchRate = AICORE._Defuzzify (zIsTargetBelowMe, 0.0f, 6.0f);
				}
				_RotatePitch (bot, -fPitchRate, bUseTimeScale);
			}
		}
					
		if (fMaxVelocity > 0.0f) {
			// Only drive forward when facing nearly toward target	
			if (zIsTargetInFrontOfMe > 0.7) {
				// Only drive forward if we're far enough from target
				if (fTargetDistance >= 1.00f) {
					float fVelocity = AICORE._Defuzzify (zIsTargetInFrontOfMe, 0.0f, fMaxVelocity);
					_MoveForward (bot, fVelocity, bUseTimeScale);
				}
			}
			
			// Return whether target is reached
			return fTargetDistance < 3.00f;
		} else {
			// Return whether we're facing the target
			// Also include whether target is reached because when
			// we're very close to the target we get weird look at information
			return zIsTargetInFrontOfMe > 0.9f || fTargetDistance < 2.00f;
		}
		
	}
}
