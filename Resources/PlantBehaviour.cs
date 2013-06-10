using UnityEngine;
using System.Collections;

public class PlantBehaviour : MonoBehaviour
{
	
	//make git see me

	/*
     * Plant will grow, bloom and die, progressing through states pretty much linearly,
     * according to available resources. Plants take in rain and sunshine.
     * 
     * Plants will give out numbers to the resource behavior
     * 
     * When resources are depleted, the plant will change its color/state
     */
	
	public FlowerHeadBehaviour flowerHead; //for orienting to sun, landing
	//-flowerhead will be sent simple messages about petal position
	
	public GameObject plantStalk;
	public int statePlant = 3;
	const int statePlantSeed = 1,
			  statePlantGrowing = 2,
			  statePlantWaiting = 3,
			  statePlantBlooming = 4,
			  statePlantDying = 5;
	bool bstatePlantTransitioning = false;
	bool bRaining = false;
	bool bDidRain = false;
	int iRainCounter = 0;
	float fWaitAfterRain = 3.0f;
	public Vector3 posStart,
			posEnd; //start and end of grow
	float endY = 1.0f; //height of plant
	
	void Awake ()
	{
		_RegisterPlant (this);
	}
	
	// Use this for initialization
	void Start ()
	{
		posStart = plantStalk.transform.position;
		posEnd = plantStalk.transform.position;
		posEnd.y += endY;
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		RunPlantController (this);
		RunPlantStateMachine (this);
	}
	
	//Register
	static void _RegisterPlant (PlantBehaviour plant)
	{
		Datacore._RegisterPlant (plant);
	}
	
	static void _UnRegisterPlant (PlantBehaviour plant)
	{
		Datacore._UnregisterPlant (plant);
	}
	
	//received message from resource behaviour
	public void _DepleteResource ()
	{
		
	}

	static void RunPlantController (PlantBehaviour plant)
	{
		//decide which state to be in and transistion to,
		//only if not transitioning
		if (!plant.bstatePlantTransitioning) {
			switch (plant.statePlant) {
			case statePlantSeed:

				break;

			case statePlantWaiting:
				if (plant.bDidRain && !plant.bRaining) {
					plant.transistionToGrowing ();
				}
				break;

			case statePlantGrowing:
				//if raining stop growing
				if (plant.bRaining) {
					plant.transitionToWaiting ();
				} else {
					Vector3 toEnd = plant.posEnd - plant.transform.position;
					float mag = toEnd.magnitude;
					if (mag > 0.1f) {
						plant.statePlant = statePlantGrowing;
					} else {
						//plant.statePlant = statePlantBlooming;
						plant.transitionToBlooming();
					}
				}
				//grow towards target
				
				
				
				break;

			case statePlantBlooming:

				break;

			case statePlantDying:

				break;
			}
		}
        
	}

	static void RunPlantStateMachine (PlantBehaviour plant)
	{
		//whatever state plant is in, take action
		switch (plant.statePlant) {
		case statePlantSeed:

			break;

		case statePlantWaiting:
			
			break;

		case statePlantGrowing:
			//grow towards target
			plant.transform.Translate (Vector3.up * 0.25f * Time.deltaTime);
			break;

		case statePlantBlooming:

			break;

		case statePlantDying:

			break;
		}
	}

	void transistionToGrowing (int prevState)
	{

	}
	
	void transistionToGrowing ()
	{
		statePlant = statePlantGrowing;
	}
	
	void transitionToWaiting ()
	{
		BroadcastMessage("_PetalsClose", SendMessageOptions.DontRequireReceiver);
		statePlant = statePlantWaiting;
	}
	
	void transitionToBlooming ()
	{
		BroadcastMessage("_PetalsOpen", SendMessageOptions.DontRequireReceiver);
		BroadcastMessage("_FaceSun", SendMessageOptions.DontRequireReceiver);
		statePlant = statePlantBlooming;
	}
	//Rain is an event called by message
	public void _Rain ()
	{
		//Debug.Log(this.ToString() + "plant received rain");
		//Rain gives energy
		if (!bDidRain) {
			StartCoroutine (SetDidRain ());
		}
		if (!bRaining) {
			StartCoroutine (SetRaining ());
		}
		
	}
	
	IEnumerator SetDidRain ()
	{
		bDidRain = true;
		yield return new WaitForSeconds(3600.0f);//plants will lose memory of rain
		bDidRain = false;
	}
			
	IEnumerator SetRaining ()
	{
		bRaining = true;
		yield return new WaitForSeconds(fWaitAfterRain);
		bRaining = false;			
	}
	
	void OnDestroy ()
	{
		_UnRegisterPlant (this);
	}
}
