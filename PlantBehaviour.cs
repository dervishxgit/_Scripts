using UnityEngine;
using System.Collections;

public class PlantBehaviour : MonoBehaviour {
	
	public int statePlant = 0;
	const int statePlantSeed	= 1,
			  statePlantGrowing	= 2,
			  statePlantWaiting = 3,
			  statePlantBlooming= 4,
			  statePlantDying 	= 5;
	
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
		//Debug.Log(this.ToString() + "plant received rain");
		//Rain gives energy
		
	}
}
