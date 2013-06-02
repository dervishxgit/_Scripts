using UnityEngine;
using System.Collections;

public class PlantBehaviour : MonoBehaviour {
	
	void Awake() {
		_RegisterPlant(this);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Register
	static void _RegisterPlant(PlantBehaviour plant) {
		Datacore._RegisterPlant(plant);
	}
	
	//received message from resource behaviour
	public void _DepleteResource() {
		
	}
	
	//Rain is an event called by message
	public void _Rain() {
		Debug.Log(this.ToString() + "plant received rain");
	}
}
