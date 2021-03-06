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

public class Wasp_Core : MonoBehaviour
{
	
	//References
	public Datacore dCore;
	//private Wasp_Core wCore;
	public GameObject waspRoot;
	public Wasp_VirtualMind wMind;
	public Wasp_Controller wController;
	public GameObject waspGeo;
	public GameObject waspColorSensor;
	public GameObject waspChemoSensor;
	
	public Wasp_ChemoTransmitter waspChemoTransmitter;
	
	public GameObject chemoBehaviorPrefab;
	
	/*
	 * Presets (settings to be compiled in to conditions and other calibrations)
	 */
	//-remember that CONDITIONS will be accessed at runtime, these values are not intented to be used except to set conditions
	//Energy (real)
	float fEnergy 	= 	100.0f,
	fEnergyMin 		= 	10.0f,
	fEnergyMax		=	100.0f;
	public static string 
		_cEnergyString = "Energy";
	//Hunger (fuzz)
	float ffHunger	=	0.0f,
	ffHungerMin		=	0.0f,
	ffHungerMax		=	1.0f;
	public static string
		_cHungerString = "Hunger";
	//HiveFood
	float fLastKnownHiveFoodAvg = 1.0f,
	fLastKnownHiveFoodMin = 0.0f,
	fLastKnownHiveFoodMax = 1.0f;
	public static string
		_cHiveFoodString = "HiveFood";
	//TimeOfDay(real)
	float fCurrentTimeHours = 0.0f,
	fStartDayHours = 0.0f, //dawn
	fEndDayHours = 16.0f;  //dusk
	//fEndDayHours = 1.0f;
	public static string
		_cTimeOfDayString = "TimeOfDay";
	//Rain
	public float fRain = 0.0f; //this is an exception (BAD!), will be accessed for assessing amount of rain
	float fMinRain = 0.0f;
	float fMaxRain = 5.0f; //tolerates rain for this amount of time
	float fTimeSinceRain = 0.0f;
	float fRainDecayStart = 3.0f;
	public static string
		_cRainString = "Rain";
	
	//State of mind (from Virtual Mind)
	public string stateOfMind = "none";
	
	//Waypoints
	public Hive_ myHive;
	
	public Transform destinationFinal; 	//final destination
	public Transform destinationNext;		//next waypoint
	public Transform destinationPrev;		//previous waypoint
	public GameObject targetObject;		//if we are orienting or moving to something
	//these lists, and functions for them, may have to be moved to the creature's core
	private List<Transform> lPathToDestination;	//list of waypoints, ordered to our destination
	//private float fWaypointRefreshInterval = 5.0f;
	public List<Transform> lKnownWaypoints = new List<Transform> ();
	
	/////////////////////////////////////////////////////////////////////////////////
	/* SENSES
	 */ 
	public List<Condition_> _lAllConditions = new List<Condition_>();
	//Innate Physical
	//-Health, Energy level (hunger, sleep)
	//-Controller specific senses
	//Conditions
	public Condition_ _cEnergy, _cHunger;
	
	//TODO: conditions here are used elementally. we will refine the organization soon
	
	//External
	public Condition_ _cTimeOfThisDay;
	public Condition_ _cKnownFoodNearby;
	public Condition_ _cRain;
	
	//Hive
	public Condition_ _cHiveFood;
	
	//ColorSense
	public GameObject lastSeenObject;
	public Color currentColor;
	//special placement of chemocolor here
	public Color currentChemoColor;
	public void _UpdateCurrentColor(Color c) {
		currentColor = c;
	}
	
	public List<Recommendation_> lRecommendations = new List<Recommendation_>();
	public Recommendation_ findFood = new Recommendation_();
	public Recommendation_ seekShelter = new Recommendation_();
	
	public Question_ shouldIFindFood; //use answer to inform recommendation
	public Question_ shouldISeekShelter;
	
	//External
	//ChemoSense
	
	public Chemo_ currentChemo;
	public List<Chemo_> _lChemos = new List<Chemo_>();
	public void _UpdateCurrentChemo(Chemo_ chem) {
		//called by the chemoreceptor
		//we will check if it is  a new chemo
		//if new, add; else refresh
		
		if( _lChemos.Contains(chem) ) {
			chem._Refresh();
		} else {
			_lChemos.Add(chem);
		}
		
		currentChemoColor = chem.chemoColor;
	}
	void _RunChemos() {
		if(_lChemos != null && _lChemos.Count > 0) {
			foreach(Chemo_ chem in _lChemos) {
				bool expired;
				chem._Run(Time.deltaTime, out expired);
				if(expired) {
					_lChemos.Remove(chem);
				}
			}
		}
	}
////////////////////////////////////////////////////////////////////////////////	
	// Use this for initialization
	
	void Awake () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag ("CORE").GetComponent<Datacore> () as Datacore;
		//wController = gameObject.GetComponent<Wasp_Controller>();
		//Register our wasp
		dCore._RegisterWasp (this);
		//FOR NOW- self register with first hive found in area
        if (GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive_>() != null) {
            myHive = GameObject.FindGameObjectWithTag("Hive").GetComponent<Hive_>();
            myHive._WaspJoin(this);
        }
		
		
		
		//Build Conditions
		BuildConditions();
	}
	
	void Start ()
	{
		//for now, we will just get our known waypoints at startup, for testing
		foreach (Transform i in dCore._lAllWaypoints) {
			_AddKnownWaypoint (i);
		}
		
		destinationNext = _ReturnRandomKnownWaypoint ();
		
		wController._SetGoToNext(true);
//		//Build Conditions
//		BuildConditions();
		
		
		
		//map sensors
		//--sensors and transmitters mapped by default in prefab
		
		//test of chemo transmit
		//waspChemoTransmitter._SpawnChemoBehavior(chemoBehaviorPrefab, new Chemo_(), Color.blue, 10.0f);
		
		wController.MoveState = "Flying";
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//-there will be many things we will end up doing in update
		
		//Chemos
		//_RunChemos();
		
		//test state of mind
		//Debug.Log(stateOfMind);
		
		if(!wMind._bAwake) {
			wMind._UpdateSenses();
		}
		
		
		//test of propogation to controller
		if(stateOfMind == "FindFood") {
			//wController.ControllerState = 2;
			wController._SetGoToNext(true);
		} 
		else if(stateOfMind == "SeekShelter") {
			_SetDestinationNext(myHive.transform);
			//wController._SetGoToNext(true);
		}
		
		else {
			//wController.ControllerState = 0;
		}
		
		_cTimeOfThisDay.fValue = (float)_WorldTime_._GetMinutesRM();
		
		DecayRain(Time.deltaTime);
	}
	
	//Waypoints
	//concept: list of waypoints known to this wasp, retrieved from master list in datacore
	public void _AddKnownWaypoint (Transform point)
	{
		lKnownWaypoints.Add (point);
	}
	
	public void _RemoveKnownWaypoint (Transform point)
	{
		lKnownWaypoints.Remove (point);
	}
	
	public Transform _ReturnRandomKnownWaypoint ()
	{
		//count waypoints
		//choose random number from available, return waypoint at index
		if (lKnownWaypoints != null) {
			if (lKnownWaypoints.Count > 0) {
				int numpoints = this.lKnownWaypoints.Count;
				int selection = AICORE._RandomInteger (0, numpoints);
				return lKnownWaypoints [selection] as Transform;
			} else
				return null;
		} else
			return null;
	}
	
	public void _SetDestinationNext (Transform trans)
	{
		destinationNext = trans;
	}
	
	public void _GetNextRandomWaypoint ()
	{
		destinationNext = _ReturnRandomKnownWaypoint ();
	}
	
	public void _NotifyReachedTarget(bool reached) {
		//when we reached the target, ask for a new target
//		Debug.Log("notify reached target");
		//for now, test for at hive, get next random if so
		if( _AtHive(myHive) ) {
			//Debug.Log("notify reached target set next random");
			_GetNextRandomWaypoint();
		} else {
			//Debug.Log("notify reached target set next hive");
			_SetDestinationNext(myHive.transform);
		} 
	}
	
	public void _JoinHive(Hive_ hive) {
		hive._WaspJoin(this);
		myHive = hive;
	}
	
	public bool _AtHive(Hive_ hive) {
		bool r;
		Vector3 vecToHive  = hive.transform.root.transform.position - gameObject.transform.root.transform.position;
		float dist = vecToHive.magnitude;
		r = dist < 2.5f;
		return r;
	}
	
	public void _Rain() {
		//TODO rain notify
		//mind will know what to do if there is rain
		//core should just increase the impression of rain
		fRain += 1.0f;
		//Debug.Log("wasp rain counter" + fRain);
		if(fRain > fMaxRain) fRain = fMaxRain;
		
		_cRain.fValue = fRain;
		
		fTimeSinceRain = 0.0f;
		
	}
	
	void DecayRain(float amount) {
		TimeSinceRain();
		//fTimeSinceRain += Time.deltaTime;
		
		if(fTimeSinceRain > fRainDecayStart) {
			fRain -= amount;
			if(fRain < fMinRain) fRain = fMinRain;
		}
		
		_cRain.fValue = fRain;
	}
	
	void TimeSinceRain () {
		fTimeSinceRain += Time.deltaTime;
	}
	
	//From mind
	void SetMindOutString(string s) {
		stateOfMind = s;
	}
	
	//Conditions
	void BuildConditions() {
		//Energy
		_cEnergy = new Condition_(_cEnergyString,  ref fEnergy,  ref fEnergyMin,
			ref fEnergyMax,  ref _lAllConditions);
		//Hunger
		_cHunger = new Condition_("Hunger", ref ffHunger, ref ffHungerMin,
			ref ffHungerMax, ref _lAllConditions);
		//Hive Food
		fLastKnownHiveFoodAvg = myHive._GetHiveFoodAvg();
		fLastKnownHiveFoodMin = myHive._GetHiveFoodMin();
		fLastKnownHiveFoodMax = myHive._GetHiveFoodMax();
		_cHiveFood = new Condition_("HiveFood", ref fLastKnownHiveFoodAvg,
			ref fLastKnownHiveFoodMin, ref fLastKnownHiveFoodMax, ref _lAllConditions);
		
		//Debug.Log("en: " + _cEnergy.fValue + "hun: " + _cHunger.fValue + "hvf: " + _cHiveFood.fValue);
		
		//Time of Day
		//TODO: change this to use light from day. for now we will judge the hours
		fCurrentTimeHours = _WorldTime_._GetHoursRM();
		_cTimeOfThisDay = new Condition_("TimeOfDay", ref fCurrentTimeHours, ref fStartDayHours,
			ref fEndDayHours, ref _lAllConditions);
		
		//Rain
		_cRain = new Condition_(_cRainString, ref fRain, ref fMinRain, ref fMaxRain,
			ref _lAllConditions);
	}
	
	
	
}
