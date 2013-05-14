using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wasp_VirtualMind : MonoBehaviour {
	
	/* Wasp Virtual Mind (c) 2013 - Steve Miller
	 * 
	 * Mind concept (very much a self contained machine)
	 * 
	 * Perceive 
	 * -environment conditions
	 * -self conditions (health energy)
	 * -signals
	 * 
	 * Contemplate
	 * -evaluate conditions according to context
	 * -deal with immediate / short circuit conditions like danger
	 * -process signals from wasps, weight messages according to role
	 * 
	 * Act
	 * -react (short circuit)
	 * -signal output
	 * -execute action (movement or otherwise)
	 * 
	 * Minds have roles, we need to be able to influence behavior according to some predefined rules in the social
	 * hierarchy. this functionality will come later however
	 * 
	 */ 
	
	
	public Datacore dCore;
	public Wasp_Core wCore;
	
	public int _iReadyState = -1;
	
	public bool _bAwake = false;
	
	// Use this for initialization
	void Start () {
		//map
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
		wCore = gameObject.GetComponent<Wasp_Core>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Wake() {
		_bAwake = true;
	}
	
	void Sleep() {
		_bAwake = false;
	}
	
	/*Perceive
	 * 
	 * 
	 */ 
	
	//update senses might be a good function to call from outside to wake the mind state machine
	public void _UpdateSenses() {
		Wake();
	}
	
	/*Contemplate
	 * 
	 * 
	 */
	
	//once we have our data in hand, run the state machine controller for the mind until it makes a decision
	
	/*Act
	 * 
	 * 
	 */
	
	//once decision has been made, we can output that decision to datacore, use it to complete an objective
	//the mind can probably be put to sleep once it has made a decision, will be woken up by stimulus
	
}
