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
	public bool bOrientToWorld = true;
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
	
	bool bLanded = false, bLanding = false;
	
	public bool bAtTarget = false;
	bool bGoToNext = false;
	public void _SetGoToNext(bool go) {
		bGoToNext = go;
	}
	
	// Use this for initialization
	void Awake () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		wCore = gameObject.GetComponent<Wasp_Core>() as Wasp_Core;
		waspRoot = wCore.waspRoot;
		waspGeo = wCore.waspGeo;
		
		//create fuzzytarget
		this.FT = new FuzzyTarget();
	}
	
	void Start () {
//		//establish connection to creature core and global datacore
//		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
//		wCore = gameObject.GetComponent<Wasp_Core>() as Wasp_Core;
//		waspRoot = wCore.waspRoot;
//		waspGeo = wCore.waspGeo;
//		
//		//create fuzzytarget
//		this.FT = new FuzzyTarget();
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
//			if(wCore.stateOfMind == "FindFood") {
//				//test for now, just pick a new
//				if( wCore._AtHive(wCore.myHive) ) {
//					wCore._GetNextRandomWaypoint();
//				} else {
//					wCore.destinationNext = wCore.myHive.transform;
//				}
//				
//				ControllerState = stateControllerMoving;
//			}
			
//			if(wCore.destinationNext != null && bGoToNext) {
//				ControllerState = stateControllerMoving;
//			}
			
			
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
				//Debug.Log("takeoff hit");
//				MoveState = stateMoveFlying;
//				ControllerState = stateControllerSeeking;
				break;
			case "Landing":
				//StartCoroutine(_LandOnTarget(this.wCore, this.wCore.destinationNext));
				//Debug.Log("landing hit");
//				bool bFinished = _LandOnTarget(this.wCore, this.wCore.destinationNext);
//				if(bFinished) {
//					MoveState = stateMoveTakeOff;
//				}
//				if(!bLanded && !bLanding) {
//					StartCoroutine(_LandOnTarget_CO(this.wCore, this.wCore.destinationNext) );
//				} else {
//					MoveState = stateMoveTakeOff;
//				}
				
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
				bAtTarget = Datacore._SeekTarget3D( this, wCore.destinationNext.position, 
					2.0f, bOrientToWorld, bUseTimeScaleForMovement ) ;
				//temp force state change
				if(bAtTarget) {
					//Debug.Log("reached");
					wCore.destinationNext.transform.root.gameObject.BroadcastMessage("tempEat", SendMessageOptions.DontRequireReceiver);
					wCore.SendMessage("_NotifyReachedTarget", true, SendMessageOptions.DontRequireReceiver);
					//MoveState = stateMoveLanding;
					//Debug.Log("wasp at target");
					//ControllerState = stateControllerSeeking;
					
				}
					
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
		switch(ControllerState) {
		case stateControllerSeeking:
			if(_CheckValidWaypoint(wCore.destinationNext) && bGoToNext) {
				//Debug.Log("controller should go to next state");
				ControllerState = stateControllerMoving;
			}
			break;
		case stateControllerMoving:
			if(bGoToNext) {
				
			} else if(bAtTarget) ControllerState = stateControllerSeeking;
			break;
		}
		
	}
	
	//state functions
	public string _GetMoveState() {
		return MoveState;
	}
	
	//Movement Functions
	
	//Landing and Takeoff
	public static float fLandDistance, fTakeOffDistance, fLandSpeed, fTakeOffSpeed;
	public static float fUpVectorDotTolerance = 0.8f;
	public static float fDelayAfterLand = 1.0f;
	
		public static bool _LandOnTarget(Wasp_Core wasp, Transform target) {
		//check and set landing target
		bool bFinished = false;
		if(wasp != null && target != null) {
			//can we ask for the best triangle and land on that?
			//Vector3 closestnorm;
			//Mesh mesh = target.root.GetComponent<MeshFilter>().mesh;
			//Vector3[] normals = mesh.normals;
			//get most upright normal
			//int i = 0;
			//Vector3 oldnorm = new Vector3(0.0f,0.0f,0.0f), newnorm;
			//closestnorm = oldnorm;
//			while( i < normals.Length) {
//				newnorm = normals[i];
//				if(Vector3.Dot(newnorm, Vector3.up) > fUpVectorDotTolerance){
//					if( Vector3.Dot(newnorm, Vector3.up) > Vector3.Dot(oldnorm, Vector3.up) ) {
//						oldnorm = newnorm;
//						closestnorm = newnorm;
//					}
//				}
//			}
			
			//approach location and orient to normal
			FuzzyTarget landingTarget = new FuzzyTarget();
			landingTarget.trans = target;
			landingTarget.gObject = target.gameObject;
			
			if( !Datacore._SeekTarget3D(wasp, landingTarget.trans.position, fLandSpeed, wasp.wController.bUseTimeScaleForMovement) ) {
				bFinished = false;
			}
			//yield return new WaitForSeconds(fDelayAfterLand);
			bFinished = true;
			return bFinished;
			
		} else {
			Debug.Log("wasp could not begin landing operation: " + wasp.ToString() + "+target: " + target.ToString());
			//yield return null;
			return false;
		}
		
		
		//final descent and stop
		
	}
	
	public static IEnumerator _LandOnTarget_CO(Wasp_Core wasp, Transform target) {
		//check and set landing target
		//bFinished = false;
		wasp.wController.bLanding = true;
		if(wasp != null && target != null) {
			//can we ask for the best triangle and land on that?
			Vector3 closestnorm;
			Mesh mesh = target.root.GetComponent<MeshFilter>().mesh;
			Vector3[] normals = mesh.normals;
			//get most upright normal
			int i = 0;
			Vector3 oldnorm = new Vector3(0.0f,0.0f,0.0f), newnorm;
			closestnorm = oldnorm;
			while( i < normals.Length) {
				newnorm = normals[i];
				if(Vector3.Dot(newnorm, Vector3.up) > fUpVectorDotTolerance){
					if( Vector3.Dot(newnorm, Vector3.up) > Vector3.Dot(oldnorm, Vector3.up) ) {
						oldnorm = newnorm;
						closestnorm = newnorm;
					}
				}
			}
			
			//approach location and orient to normal
			FuzzyTarget landingTarget = new FuzzyTarget();
			landingTarget.trans = target;
			landingTarget.gObject = target.gameObject;
			
			if( !Datacore._SeekTarget3D(wasp, landingTarget.trans.position, fLandSpeed, true) ) {
				//bFinished = false;
			}
			yield return new WaitForSeconds(fDelayAfterLand);
			wasp.wController.bLanded = true;
			
			
		} else {
			Debug.Log("wasp could not begin landing operation: " + wasp.ToString() + "+target: " + target.ToString());
			yield return null;
		}
		
		
		//final descent and stop
		
	}
	
	public static void _TakeOff(Wasp_Core wasp) {
		
	}

	
}
