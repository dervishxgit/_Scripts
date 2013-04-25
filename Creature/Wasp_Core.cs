using UnityEngine;
using System.Collections;

public class Wasp_Core : MonoBehaviour {
	
	//References
	public Datacore dCore;
	private Wasp_Core wCore;
	public GameObject waspRoot;
	public GameObject waspGeo;
	
	public ArrayList lKnownWaypoints;
	
	// Use this for initialization
	void Start () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		wCore = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Waypoints
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
