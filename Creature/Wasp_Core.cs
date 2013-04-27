using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Wasp_Core.cs (c) - Steve Miller 2013
 * 
 * the wasp core is the central code repository and interface to the wider world
 * wasp data is stored and accessed here, other scripts will map to this
 * core wasp utility functions should also be stored here
 * 
 * note on state machines and interaction:
 * other scripts will, when required, access functions, but should not modify data here directly
 * other scripts in wasp creature will not directly modify any other scripts, including the virtual mind
 * example: the virtual mind will use functions in the datacore to update information,
 * the most important output of the virtual mind will be the output of its state machine (what it wants to "do")
 * this output state will direct the datacore interface to make changes to the other scripts where necessary
 */ 

public class Wasp_Core : MonoBehaviour {
	
	//References
	public Datacore dCore;
	//private Wasp_Core wCore;
	public GameObject waspRoot;
	public GameObject waspGeo;
	
	//Waypoints
	public Transform destinationFinal; 	//final destination
	public Transform destinationNext;		//next waypoint
	public Transform destinationPrev;		//previous waypoint
	public GameObject targetObject;		//if we are orienting or moving to something
	//these lists, and functions for them, may have to be moved to the creature's core
	private ArrayList lPathToDestination;	//list of waypoints, ordered to our destination
	private float fWaypointRefreshInterval = 5.0f;
	
	public List<Transform> lKnownWaypoints  = new List<Transform>();
	
	// Use this for initialization
	void Start () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		
		//Register our wasp
		dCore._RegisterWasp( this );
		
		//for now, we will just get our known waypoints at startup, for testing
		foreach (Transform i in dCore._lAllWaypoints) {
			_AddKnownWaypoint(i);
		}
		
		destinationNext = _ReturnRandomKnownWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Waypoints
	//concept: list of waypoints known to this wasp, retrieved from master list in datacore
	public void _AddKnownWaypoint(Transform point) {
		lKnownWaypoints.Add(point);
	}
	
	public void _RemoveKnownWaypoint(Transform point) {
		lKnownWaypoints.Remove(point);
	}
	
	public Transform _ReturnRandomKnownWaypoint() {
		//count waypoints
		//choose random number from available, return waypoint at index
		if(lKnownWaypoints != null) {
			if(lKnownWaypoints.Count > 0) {
				int numpoints = this.lKnownWaypoints.Count;
				int selection = AICORE._RandomInteger(0, numpoints);
				return lKnownWaypoints[selection] as Transform;
			} else return null;
		} else return null;
	}
	
	public void _SetDestinationNext(Transform trans) {
		destinationNext = trans;
	}
	
	public void _GetNextRandomWaypoint() {
		destinationNext = _ReturnRandomKnownWaypoint();
	}
	
}
