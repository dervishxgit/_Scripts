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
	
	public class FuzzyTarget {
		public	GameObject gObject;
		public Transform trans;
		public float InFrontMe, BehindMe, BelowMe, AboveMe, LeftMe, RightMe;
		public float distance;
	};
	
	public FuzzyTarget FT;
	
	//Settings: Movement, Rotation, Thresholds
	public bool bUseTimeScaleForMovement = true;
//	public float fMoveRate = 1.0f;
//	public float fRotationRate = 5.0f;
//	public float fFacingTolerance = 0.1f;
//	public float fForwardThreshold = 0.98f; //are we sufficiently towards target?
//	public float fForwardMovementSpeed = 5.0f;
//	public float fTargetDistanceThreshold = 0.5f;
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
		
		//create fuzzytarget
		this.FT = new FuzzyTarget();
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
		wCore.destinationFinal = trans;	
	}
	public bool _CheckValidWaypoint(Transform trans) {
		bool valid = false;
		if (trans != null) {
			valid = true;
		}
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
		//_CheckValidWaypoint(destinationFinal);
		//_CheckValidWaypoint(destinationNext);
		if(ControllerState == stateControllerWaiting) {
			return;
		}
		else if (ControllerState == stateControllerSeeking) {
			//looking for next
			
			//test for now, just pick a new
			wCore._GetNextRandomWaypoint();
			
			ControllerState = stateControllerMoving;
			
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
				
				//proto
//				bool facing = _CheckFacing(wCore.destinationNext);
//				if(facing) {
//					Debug.Log ("facing object");
//					_MoveTo(wCore.destinationNext);
//				} 
//				else {
//					_Face(this.FT);
//				}
				
				//new test
				bool reached = Datacore._SeekTarget3D( this, wCore.destinationNext.position, 
					2.0f, bUseTimeScaleForMovement ) ;
				//temp force state change
				if(reached) ControllerState = stateControllerSeeking;
					
				break;
				
			}
		}
	}
	
	private void RunControllerStateMachine() {
		//for now we just check if we reached our destination, goto seeking
//		if( _ReachedTarget(wCore.destinationNext) ) {
//			ControllerState = stateControllerSeeking;
//		} else if( _CheckValidWaypoint(wCore.destinationNext) ) {
//			ControllerState = stateControllerMoving;
//		}
	}
	
	//state functions
	public string _GetMoveState() {
		return MoveState;
	}
	
	//Movement Functions
//	bool _CheckFacing(Transform target) {
//		//takes target, assigns it to current fuzzytarget and outputs values
//		bool facing = false;
//		this.FT.trans = target;
//		facing = _CheckFacing(this.FT);		
//		return facing;
//	}
//	
//	bool _CheckFacing(FuzzyTarget target) {
//		
//		bool facing = false;
//		
//		AICORE._GetSpatialAwareness3D(waspRoot.transform,
//			target.trans, out target.distance,
//			out target.BehindMe, out target.InFrontMe,
//			out target.LeftMe, out target.RightMe,
//			out target.AboveMe, out target.BelowMe);
//		
//		//this.FT = target;
//		if (target.InFrontMe > fForwardThreshold) {facing = true;}
//		
//		return facing;
//	}
//	
//	public bool _ReachedTarget(Transform target) {
//		if(AICORE._GetTargetDistance(waspRoot, target.gameObject) < fTargetDistanceThreshold) {
//			return true;
//		} else return false;
//	}
//	
//	void _MoveTo(Transform target) {
//		//simple move
//		if(!_ReachedTarget(target) ) {
//			Debug.Log("should be trying to reach");
//			Datacore._MoveForward(waspRoot, Time.deltaTime * fForwardMovementSpeed);
//
//		} else {
//			Debug.Log("should set new");
//		}
//	}
//	
//	void _Face(FuzzyTarget target) {
//		//this version depends on having a fuzzy target
//		
//		if(target.RightMe > target.LeftMe)
//		{Datacore._Yaw(waspRoot, target.RightMe * Time.deltaTime * fRotationRate);}
//		else if( target.RightMe < target.LeftMe)
//		{Datacore._Yaw(waspRoot, -target.LeftMe * Time.deltaTime * fRotationRate);}
//		
//		_ElevateTo(target.trans);
//		
//		
//	}
//	
//	void _ElevateTo(Transform target) {
//		float targetY = target.transform.position.y;
//		float to  = AICORE._IsItMax(-1 * (waspRoot.transform.position.y - targetY), 0.1f, 5.0f);
//		Datacore._MoveUp(waspRoot, to * Time.deltaTime * fForwardMovementSpeed);
//	}
	
}
