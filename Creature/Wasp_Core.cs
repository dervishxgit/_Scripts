using UnityEngine;
using System.Collections;

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
	
	public ArrayList lKnownWaypoints  = new ArrayList();
	
	// Use this for initialization
	void Start () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		//wCore = this;
		
		//for now, we will just get our known waypoints at startup, for testing
		foreach (Transform i in dCore._lAllWaypoints) {
			//lKnownWaypoints[i] = dCore._lAllWaypoints[i] as Transform;
			//lKnownWaypoints[i] = dCore._ReturnRandomWaypoint();
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
			int numpoints = this.lKnownWaypoints.Count;
			int selection = AICORE._RandomInteger(0, numpoints);
			return lKnownWaypoints[selection] as Transform;
		} else return null;
	}
	
}
