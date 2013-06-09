using UnityEngine;
using System.Collections;

public class Waypoint_Behavior : MonoBehaviour {
	
	public Datacore dCore;
	

	
	// Use this for initialization
	void Awake () {
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		
		dCore._AddWaypoint(gameObject.transform);
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void _selfDestroy() {
		dCore._RemoveWaypoint(gameObject.transform);
		Destroy(gameObject, 1);
	}
	
	
}
