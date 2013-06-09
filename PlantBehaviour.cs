using UnityEngine;
using System.Collections;

public class PlantBehaviour : MonoBehaviour {
	
    //make git see me

    /*
     * Plant will grow, bloom and die, progressing through states pretty much linearly,
     * according to available resources. Plants take in rain and sunshine.
     * 
     * Plants will give out numbers to the resource behavior
     * 
     * When resources are depleted, the plant will change its color/state
     */

	public int statePlant = 0;
	const int statePlantSeed	= 1,
			  statePlantGrowing	= 2,
			  statePlantWaiting = 3,
			  statePlantBlooming= 4,
			  statePlantDying 	= 5;
    bool bstatePlantTransitioning = false;

    bool bDidRain = false;
    int iRainCounter = 0;
	
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

    static void RunPlantController(PlantBehaviour plant)
    {
        //decide which state to be in and transistion to,
        //only if not transitioning
        if (!plant.bstatePlantTransitioning) {
            switch (plant.statePlant)
            {
                case statePlantSeed:

                    break;

                case statePlantWaiting:

                    break;

                case statePlantGrowing:

                    break;

                case statePlantBlooming:

                    break;

                case statePlantDying:

                    break;
            }
        }
        
    }

    static void RunPlantStateMachine(PlantBehaviour plant)
    {
        //whatever state plant is in, take action
        switch (plant.statePlant)
        {
            case statePlantSeed:

                break;

            case statePlantWaiting:

                break;

            case statePlantGrowing:

                break;

            case statePlantBlooming:

                break;

            case statePlantDying:

                break;
        }
    }

    void transistionToGrowing(int prevState)
    {

    }
	//Rain is an event called by message
	public void _Rain() {
		//Debug.Log(this.ToString() + "plant received rain");
		//Rain gives energy
		
	}
}
