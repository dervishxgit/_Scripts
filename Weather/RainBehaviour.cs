using UnityEngine;
using System.Collections;

public class RainBehaviour : MonoBehaviour {

	//Settings
	public float fRainDropAmount = 1.0f; //amount delivered per plant per cycle
	
	public float fRainInterval = 1.0f;
	
	public float fRainTotalThisCycle = 10.0f,
		 fRainAmountThreshold = 10.0f,
		  fRainUnitPerSecond = 1.0f;
	
	public int stateRainCycle = 0;
	const int rainStateWaiting = 0,
			  rainStateRaining = 1;
	
	public bool bOutRainMessage = false; //did rain send a message
	
	public Skybox skybox;
	
	public Material skymat_overcast01,
					skymat_sunny01;
	
	
	Datacore dCore;
	
	void Awake () {
		Datacore._RegisterRain(this);
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
		skybox = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Skybox>();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//test
		//if(bTestRain) {
			//_Rain();
		//}
		
		RunStateController();
		_RunCycle();
	}
	
	void _RunCycle() {
		
		switch(stateRainCycle) {
		case rainStateRaining:
			fRainTotalThisCycle -= fRainUnitPerSecond * Time.deltaTime;
			if(!bOutRainMessage) {
				_Rain(true);
			}
			
			break;
			
		case rainStateWaiting:
			fRainTotalThisCycle += fRainUnitPerSecond * Time.deltaTime;
			if(!bOutRainMessage) {
				_Rain(false);
			}
			break;
		}
	}
	
	void RunStateController() {
		
		switch(stateRainCycle) {
		case rainStateRaining:
			
			//is rain depleted?
			//if so go to wait
			if(fRainTotalThisCycle <= 0.0f) {
				transitionToWaiting();
			}
			break;
			
		case rainStateWaiting:
			if(fRainTotalThisCycle >= fRainAmountThreshold) {
				transitionToRaining();
			}
			break;
		}
	}
	
	void transitionToWaiting() {
		//set new state
		stateRainCycle = rainStateWaiting;
		//manip weather bonus
		
		skybox.material = skymat_sunny01;
		
		//remove rain effect if present
		GameObject maincam = GameObject.FindGameObjectWithTag("MainCamera");
		maincam.SendMessage("_DestroyRainEffect",SendMessageOptions.DontRequireReceiver);
		
		//when we transition state, reset the message flag
		bOutRainMessage = false;
	}
	
	void transitionToRaining() {
		stateRainCycle = rainStateRaining;
		
		skybox.material = skymat_overcast01;
		
		//spawn rain particles
		GameObject maincam = GameObject.FindGameObjectWithTag("MainCamera");
		maincam.SendMessage("_SpawnRainEffect", SendMessageOptions.DontRequireReceiver);
		
		//when we transition state, reset the message flag
		bOutRainMessage = false;
	}
	
	void _Rain(bool bRaining) {
//		Debug.Log("Rain broadcasting");
		//BroadcastMessage("_Rain", SendMessageOptions.DontRequireReceiver);
		if( bRaining) {
			dCore._Rain();
		}
		
		//bTestRain = false;
	}
}
