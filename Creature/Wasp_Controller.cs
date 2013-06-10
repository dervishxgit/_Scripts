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
	
	public GameObject sensorElevationLocation;
	
	public class Sensor_ {
		//set
		public float fRayDistance,
					fMinDistance,
					fMaxDistance,
					fDesiredDistance;
		
		public RaycastHit hitInfo;
		
		public bool bHitting;
		
		public Transform transAttach; //transform attached to
		
		static public void _InitializeSensor(Sensor_ sensor,
			float raydistance, float mindistance, float maxdistance,
			float desiredDistance, Transform attach
			) {
			sensor.fRayDistance = raydistance;
			sensor.fMinDistance = mindistance;
			sensor.fMaxDistance = maxdistance;
			sensor.fDesiredDistance = desiredDistance;
			sensor.hitInfo = new RaycastHit();
			sensor.bHitting = false;
			sensor.transAttach = attach;
		}
		
		static public bool _MaintainElevationReading(Sensor_ sen, out float fFuzzDistance, int checkMinMaxEqual) {
			//cast ray (down)
			Vector3 raydir = Vector3.down;
			Vector3 raypos = sen.transAttach.position;
			Ray rray = new Ray(raypos, raydir);
			fFuzzDistance = 0.0f;
			if( Physics.Raycast( rray, out sen.hitInfo, sen.fRayDistance) ) {
				//if ray hit, make sure distance is within parameters
				
				switch(checkMinMaxEqual) {
				case 0:
					//check min
					fFuzzDistance = AICORE._IsItMin(sen.hitInfo.distance, sen.fMinDistance, sen.fMaxDistance);
					break;
					
				case 1:
					//check max
					fFuzzDistance = AICORE._IsItMax(sen.hitInfo.distance, sen.fMinDistance, sen.fMaxDistance);
					break;
					
				case 2:
					//check equal, later
					break;
				}
				return true;
			} else return false;
			
			//set out of fuzz distance to is min or max on range
		}
	};
	
	public Sensor_ sensorElevation = new Sensor_();
	//set
	float fSenElevRayDistance = 6.0f,
		  fSenElevMinDistance = 0.25f,
		  fSenElevMaxDistance = 10.0f,
		  fSenElevDesiredDistance = 1.0f;
	float fElevTranslateMult = 30.0f;
	
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
	const string stateMoveAvoiding = "Avoiding";
	
	bool bLanded = false, bLanding = false;
	
	public bool bHoldingPattern = false;
	public Vector3 nextHoldingPosition;
	public int iHoldingCounter = 0; //how many times wasp has held this pattern
	public const int iHoldTimes = 5;      //max number of hold positions
	public float fHoldWait = 1.5f;  //time to wait between hold positions
	
	
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
		
		Sensor_._InitializeSensor(sensorElevation, fSenElevRayDistance,
			fSenElevMinDistance, fSenElevMaxDistance, fSenElevDesiredDistance,
			sensorElevationLocation.transform);
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
			float felevdistance = 0.0f;
			
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
				
				//test and bad for final night:
				//just place chemo at location for whatever we were looking at
				
				wCore.waspChemoTransmitter._SpawnChemoBehavior(wCore.chemoBehaviorPrefab, new Chemo_(), 
					wCore.currentColor, 10.0f);
				
				//version dups color of target
//				wCore.waspChemoTransmitter._SpawnChemoBehavior(wCore.chemoBehaviorPrefab, new Chemo_(), 
//					wCore.destinationNext.transform.root.gameObject.GetComponentInChildren<Renderer>().renderer.material.color, 10.0f);
				
				MoveState = stateMoveFlying;
				
				break;
			case "Walking":
				break;
			case "Hovering":
				//initiate holding pattern
				//new test
//				bHoldingPattern = Datacore._SeekTarget3D( this, nextHoldingPosition, 
//					2.0f, bOrientToWorld, bUseTimeScaleForMovement ) ;
//				
////				felevdistance = 0.0f;
////				if(Sensor_._MaintainElevationReading(sensorElevation, out felevdistance, 0) ) {
////					//move away/up
////					float transAmount = AICORE._Defuzzify( 1 - felevdistance, sensorElevation.fMinDistance, sensorElevation.fMaxDistance );
////					transform.Translate(Vector3.up * fElevTranslateMult *  Time.deltaTime);
////				}
//				//temp force state change
//				if(bHoldingPattern) {
//					Debug.Log("reached");
//					wCore.destinationNext.transform.root.gameObject.BroadcastMessage("tempEat", SendMessageOptions.DontRequireReceiver);
//					wCore.SendMessage("_NotifyReachedTarget", true, SendMessageOptions.DontRequireReceiver);
//					//test
//					MoveState = stateMoveFlying;
//				}
				
				break;
			case "Flying":
				//Orient
				//	if not facing, face
				//MoveTo
				
				
				
				//new test
				bAtTarget = Datacore._SeekTarget3D( this, wCore.destinationNext.position, 
					2.0f, bOrientToWorld, bUseTimeScaleForMovement ) ;
				
				felevdistance = 0.0f;
				if(Sensor_._MaintainElevationReading(sensorElevation, out felevdistance, 0) ) {
					//move away/up
					float transAmount = AICORE._Defuzzify( 1 - felevdistance, sensorElevation.fMinDistance, sensorElevation.fMaxDistance );
					transform.Translate(Vector3.up * fElevTranslateMult *  Time.deltaTime);
				}
				//temp force state change
				if(bAtTarget) {
					Debug.Log("reached");
					wCore.destinationNext.transform.root.gameObject.BroadcastMessage("tempEat", SendMessageOptions.DontRequireReceiver);
					wCore.SendMessage("_NotifyReachedTarget", true, SendMessageOptions.DontRequireReceiver);
					//test
					nextHoldingPosition = _GetNextHoldingPosition();
					//MoveState = stateMoveHovering;
					MoveState = stateMoveLanding;
				}
					
				break;
				
			case "Avoiding":
//				felevdistance = 0.0f;
//				if(Sensor_._MaintainElevationReading(sensorElevation, out felevdistance, 0) ) {
//					//move away/up
//					transform.Translate(Vector3.up * fElevTranslateMult *  Time.deltaTime);
//				}
//				//back to flying
//				MoveState = stateMoveFlying;
				break;
				
			}
		}
	}
	
	private void RunControllerStateMachine() {
		//test short-circuit elevation test to go to avoiding state
//		float fuzzelevDistance = 0.0f;
//		if(Sensor_._MaintainElevationReading(sensorElevation, out fuzzelevDistance, 0) ) {
//			MoveState = stateMoveAvoiding;
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
				
			} 
			else if(bAtTarget) {
				//ControllerState = stateControllerSeeking;
				MoveState = stateMoveHovering;
			}
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
	
	public static Vector3 _GetNextHoldingPosition() {
		Vector3 rvec = new Vector3();
		rvec = Random.insideUnitSphere;
		return rvec;
	}
	
	public static IEnumerator EnterHoldingPattern_CO(Wasp_Core wasp, Transform target) {
		wasp.wController.bHoldingPattern = true;
		
		float fwait = 1.5f;
		int numtimes = 5;
		//get random transform points around target
		
		Vector3 vecTarget = target.position;
		
		for(int i = 0; i < numtimes; i++) {
			Debug.Log("holding pattern + " + i);
			Vector3 newposition = Random.insideUnitSphere;
			Datacore._SeekTarget3D(wasp, newposition, 1.0f, wasp.wController.bUseTimeScaleForMovement);
			//wasp.transform.Translate(newposition);
			//wasp.StartCoroutine(_AnimateToTarget(wasp, target, 0.25f));
			yield return new WaitForSeconds(fwait);
		}
		
		wasp.wController.bHoldingPattern = false;
		yield return null;
	}
	
	public static IEnumerator _AnimateToTarget(Wasp_Core wasp, Transform target, float distThreshold) {
		Vector3 vectarget = target.position - wasp.transform.position;
		if(vectarget.magnitude > distThreshold) {
			wasp.transform.Translate(vectarget * Time.deltaTime);
		}
		yield return new WaitForEndOfFrame();
		
	}
	
	
}
