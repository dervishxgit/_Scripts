using UnityEngine;
using System.Collections;

public class RainBehaviour : MonoBehaviour {
	
	//Settings
	float fRainDropAmount = 1.0f; //amount delivered per plant per cycle
	
	float fRainInterval = 1.0f;
	
	float fRainTotalThisCycle = 100.0f,
		  fRainUnitPerSecond = 1.0f;
	
	int stateRainCycle = 0;
	const int rainStateWaiting = 0,
			  rainStateRaining = 1;
	
	bool bTestRain = true;
	
	Datacore dCore;
	
	void Awake () {
		Datacore._RegisterRain(this);
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//test
		//if(bTestRain) {
			_Rain();
		//}
	}
	
	void _RunCycle() {
		//test
		_Rain();
	}
	
	void _Rain() {
		Debug.Log("Rain broadcasting");
		//BroadcastMessage("_Rain", SendMessageOptions.DontRequireReceiver);
		dCore._Rain();
		bTestRain = false;
	}
}
