using UnityEngine;
using System.Collections;

public class Waypoint_Behavior : MonoBehaviour {
	
	public Datacore dCore;
	
	public Transform testtrans;
	
	// Use this for initialization
	void Start () {
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		
		testtrans = gameObject.transform;
		
		dCore._AddWaypoint(testtrans);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void _selfDestroy() {
		dCore._RemoveWaypoint(gameObject.transform);
		Destroy(gameObject, 1);
	}
}
