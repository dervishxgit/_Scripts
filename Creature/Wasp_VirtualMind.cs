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
	
	// Use this for initialization
	void Start () {
		//map
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
		wCore = gameObject.GetComponent<Wasp_Core>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Perceive
	public void _UpdateSenses() {
		
	}
}
