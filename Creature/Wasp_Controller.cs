using UnityEngine;
using System.Collections;

//Wasp Controller (c) 2013 - Steve Miller
/*
 * Objective of wasp controller is to direct the physical orientation and location of the wasp creature/geometry
 * 
 * Controller should have two modes: 
 * 		auto: default mode, AI controlled
 * 		override: direct control mode
 * 
 * By default, controller will check to see if it has an existing waypoint, supplied by the mind/core,
 * if it is ready to go, it will begin the movement operations to reach the location.
 * If it does not have an existing waypoint it will cast about for one and pick, usually at random
 * 
 */
[System.Serializable]
public class Wasp_Controller : MonoBehaviour {
	
	//Control mode
	public bool bAuto = true;				//AI controlled?
	public bool bOverride = false;			//Override controller (user control)
	//public bool bReadyToGo = true;
	//public bool bShouldIGo = false;
	
	//References
	public Datacore dCore;					//map to datacore
	public Wasp_Core wCore;					//this creature's core
	public GameObject waspRoot;				//creature's root object
	public GameObject waspGeo;				//creature's root geometry	
	
	//Waypoints
	public Transform destinationFinal; 	//final destination
	public Transform destinationNext;		//next waypoint
	public Transform destinationPrev;		//previous waypoint
	public GameObject targetObject;		//if we are orienting or moving to something
	//these lists, and functions for them, may have to be moved to the creature's core
	private ArrayList lWaypoints;			//list of all waypoints we've decided to store
	private ArrayList lPathToDestination;	//list of waypoints, ordered to our destination
	
	public struct FuzzyTarget {
		public	GameObject gObject;
		public Transform trans;
		public float InFrontMe, BehindMe, BelowMe, AboveMe, LeftMe, RightMe;
		public float distance;
	}
	
	public FuzzyTarget FT;
	
	//Settings: Movement, Rotation, Thresholds
	public float fMoveRate = 1.0f;
	public float fRotationRate = 5.0f;
	public float fFacingTolerance = 0.1f;
	public float fForwardThreshold = 0.98f; //are we sufficiently towards target?
	//States
	public int ControllerState = 0;
	const int stateControllerWaiting = 0;
	const int stateControllerSeeking = 1;
	const int stateControllerMoving = 2;
	
	public string MoveState = "default";
	const string stateMoveStanding = "Standing";
	const string stateMoveWalking = "Walking";
	const string stateMoveTakeOff = "TakeOff";
	const string stateMoveFlying = "Flying";
	const string stateMoveHovering = "Hovering";
	const string stateMoveLanding = "Landing";
	
	// Use this for initialization
	void Start () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		wCore = gameObject.GetComponent<Wasp_Core>() as Wasp_Core;
		waspRoot = wCore.waspRoot;
		waspGeo = wCore.waspGeo;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * our loop for now will have two paths, one for ai control, one for user control
		 */ 
		
		if(bAuto) {
			//Run AI controller routines:
			RunControllerAuto();
		}
		else if (bOverride) {
			//Run user input routines (to be implemented later)
		}
	}
	
	//Control mode functions
	void _ToggleAIControl(bool b) {
		bAuto = b;	
	}
	
	void _ToggleOverride(bool b) {
		bOverride = b;	
	}
	
//	void _ToggleReadyToGo(bool b) {
//		bReadyToGo = b;
//	}
//	
//	void _ToggleShouldIGo(bool b) {
//		bShouldIGo = b;
//	}
	
	//WayPoint functions
	public void _SetDestinationFinal(Transform trans) {
		destinationFinal = trans;	
	}
	public bool _CheckValidWaypoint(Transform trans) {
		bool valid = false;
		
		return valid;
	}
	
	//	MAIN AUTO CONTROLLER FUNCTION
	private void RunControllerAuto() {
		RunControllerStateMachine();
		/*
		 * Auto control routine:
		 * check if final destination is still valid, will involve future check for whether controller SHOULD go
		 * 
		 * check if next waypoint in path is valid
		 * 
		 * logic:
		 * 		Waiting: controller will wait
		 * 		Seeking: Controller will actively search for a nearby or known waypoint on its own
		 * 		Moving: controller has a destination and is in the process of going
		 * 
		 */ 
		_CheckValidWaypoint(destinationFinal);
		_CheckValidWaypoint(destinationNext);
		if(ControllerState == stateControllerWaiting) {
			return;
		}
		else if (ControllerState == stateControllerSeeking) {
			return;
		}
		else if(ControllerState == stateControllerMoving) {
			/*
			 * There will be slightly different ways of getting around, 
			 * depending on whether the wasp has landed or is flying, 
			 * and where the destination is,
			 * generically: orient to target (and/or ground), moveto target by increment
			 */ 
			switch (_GetMoveState()) {
			case "Standing":
				break;
			case "TakeOff":
				break;
			case "Landing":
				break;
			case "Walking":
				break;
			case "Hovering":
				break;
			case "Flying":
				//Orient
				//	if not facing, face
				//MoveTo
				bool facing = _CheckFacing(destinationNext);
				if(facing) {
					Debug.Log ("facing object");
					_MoveTo(destinationNext);
				} else {_Face();}
				break;
				
			}
		}
	}
	
	private void RunControllerStateMachine() {
		
	}
	
	//state functions
	public string _GetMoveState() {
		return MoveState;
	}
	
	//Movement Functions
	bool _CheckFacing(Transform target) {
		//takes target, assigns it to current fuzzytarget and outputs values
		bool facing = false;
		FT.trans = target;
		facing = _CheckFacing(FT);		
		return facing;
	}
	
	bool _CheckFacing(FuzzyTarget ft) {
		bool facing = false;
		AICORE._GetSpatialAwareness3D(waspRoot.transform,
			ft.trans, out ft.distance,
			out ft.BehindMe, out ft.InFrontMe,
			out ft.LeftMe, out ft.RightMe,
			out ft.AboveMe, out ft.BelowMe);
		Debug.Log (FT.AboveMe);
		Debug.Log (FT.BelowMe);
		//Debug.Log (FT);
		//facing = ( AICORE._AreFloatsEqual(ft.abov AICORE._AreFloatsEqual(ft.RightMe, ft.LeftMe, fFacingTolerance) );
		//facing = ( AICORE._AreFloatsEqual(ft.InFrontMe, 1.0f, fFacingTolerance) );
		Debug.Log (ft.InFrontMe);
		if (ft.InFrontMe > fForwardThreshold) {facing = true;}
		return facing;
	}
	
	void _MoveTo(Transform target) {
		//simple move
	}
	
	void _Face() {
		//this version depends on having a fuzzy target
		//Debug.Log (FT.AboveMe);
		//Debug.Log (FT.BelowMe);
		if(FT.AboveMe > FT.BelowMe) 
		{
			Debug.Log ("target is above");
			Datacore._Pitch (waspRoot, FT.AboveMe * Time.deltaTime * fRotationRate);
		}
		else if (FT.AboveMe < FT.BelowMe) 
		{
			Debug.Log("target is below");
			Datacore._Pitch(waspRoot, -FT.BelowMe * Time.deltaTime * fRotationRate);
		}
		
//		if(FT.RightMe > FT.LeftMe)
//		{Datacore._Yaw(waspRoot, FT.RightMe * Time.deltaTime * fRotationRate);}
//		else if( FT.RightMe < FT.LeftMe)
//		{Datacore._Yaw(waspRoot, -FT.RightMe * Time.deltaTime * fRotationRate);}
		
		//Debug.Log ("will call yaw....");
		float test = FT.RightMe * Time.deltaTime * fRotationRate;
		//Debug.Log (test);
		//Datacore._Yaw(waspRoot,0.1f * Time.deltaTime * fRotationRate);
		//need to roll until oriented up?
		
		
	}
	
}
