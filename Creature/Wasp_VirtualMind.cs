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
	public bool _bContemplating = false;
	
	public List<Recommendation_> lRecommendations = new List<Recommendation_>();
	Recommendation_ findFood = new Recommendation_();
	
	Question_ shouldIFindFood;
	
	
	
	// Use this for initialization
	void Start () {
		//map
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
		wCore = gameObject.GetComponent<Wasp_Core>();
		
		BuildQuestions();
		BuildRecommendations();
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(_bAwake) {
		case false:
			break;
		case true:
			switch(_bContemplating) {
			case true:
				break;
			case false:
				StartCoroutine( _Contemplate() );
				break;
			}
			break;
		}
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
	IEnumerator _Contemplate() {
		BeginContemplate();
		yield return null;
		RunMindController();
		yield return null;
		EndContemplate();
	}
	
	void BeginContemplate() {
		_bContemplating = true;
		GetRecommendations();
	}
	
	void BuildQuestions() {
		shouldIFindFood = new Question_();
		shouldIFindFood.aConditions = new Condition_[3];
	}
	
	void BuildRecommendations() {
//			Recommendation_ rtest = new Recommendation_();
//		rtest.testfloat = 2;
//		lRecommendations.Add(rtest);
		
		//FOOD
		findFood.quest = shouldIFindFood;
		findFood.ans = new Answer_();
		lRecommendations.Add(findFood);
		
	}
	
	void GetRecommendations() {
		//Food
		
	}
	
	void EndContemplate() {
		_bContemplating = false;
	}
	
	void RunMindController() {
		
	}
	
	/*Act
	 * 
	 * 
	 */
	
	//once decision has been made, we can output that decision to datacore, use it to complete an objective
	//the mind can probably be put to sleep once it has made a decision, will be woken up by stimulus
	Action_ _outputAction() {
		Action_  act = new Action_();
		return act;
	}
}
