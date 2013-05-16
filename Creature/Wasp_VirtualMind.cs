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
	
	Question_ shouldIFindFood; //use answer to inform recommendation
	
	// Use this for initialization
	void Start () {
		//map
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>();
		wCore = gameObject.GetComponent<Wasp_Core>();
		
		//BuildConditions();
		
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
	
//	void BuildConditions() {
//		//Manually
//		
//	}
	
	/*Contemplate
	 * 
	 * 
	 */
	
	//once we have our data in hand, run the state machine controller for the mind until it makes a decision
	IEnumerator _Contemplate() {
		BeginContemplate();
		yield return null;
		//StartCoroutine(RunMindController());
		RunMindController();
		yield return null;
		EndContemplate();
	}
	
	void BeginContemplate() {
		_bContemplating = true;
		GetRecommendations();
	}
	
	void BuildQuestions() {
		//Food
		shouldIFindFood = new Question_();
		
		
	}
	
	void BuildRecommendations() {
//			Recommendation_ rtest = new Recommendation_();
//		rtest.testfloat = 2;
//		lRecommendations.Add(rtest);
		
		//FOOD
		findFood.quest = shouldIFindFood;
		findFood.ans = new Answer_();
		lRecommendations.Add(findFood);
		
		//answer food question
		//compute anser for time and energy
		shouldIFindFood.aConditions = new Condition_[2];
		shouldIFindFood.aConditions[0] = Condition_._GetConditionByName(Wasp_Core._cEnergyString, wCore._lAllConditions);
		shouldIFindFood.aConditions[1] = Condition_._GetConditionByName(Wasp_Core._cTimeOfDayString, wCore._lAllConditions);
		float[] tempcon01 = new float[2];
		tempcon01[0] = shouldIFindFood.aConditions[0]._FuzzOutMax();
		tempcon01[1] = shouldIFindFood.aConditions[1]._FuzzOutMin();
		float answer01TimeEnergy = Question_._AnswerQuestion_f(Wasp_VirtualMind.waspRoleBasic.bmFindFood_Time_Energy(),
			tempcon01);
		Debug.Log(answer01TimeEnergy);
		
		//compute answer for hivefood and hunger
		shouldIFindFood.aConditions = new Condition_[2];
		shouldIFindFood.aConditions[1] = Condition_._GetConditionByName(Wasp_Core._cHiveFoodString, wCore._lAllConditions);
		shouldIFindFood.aConditions[0] = Condition_._GetConditionByName(Wasp_Core._cHungerString, wCore._lAllConditions);
		float[] tempcon02 = new float[2];
		tempcon02[1] = shouldIFindFood.aConditions[1]._FuzzOutMax();
		tempcon02[0] = shouldIFindFood.aConditions[0]._FuzzOutMax();
		float answer02HiveFoodHunger = Question_._AnswerQuestion_f(waspRoleBasic.bmFindFood_HiveFood_Hunger(),
			tempcon02);
		Debug.Log(answer02HiveFoodHunger);
		
		//final output for food question
		float[] tempcon03 = new float[2];
		tempcon03[1] = answer02HiveFoodHunger;
		tempcon03[0] = answer01TimeEnergy;
		findFood.ans.fAns = Question_._AnswerQuestion_f(waspRoleBasic.bmFindFoodFinal_HiveFoodHunger_TimeEnergy(),
			tempcon03);
		Debug.Log(findFood.ans.fAns);
	}
	
	void GetRecommendations() {
		//Food
		
	}
	
	void EndContemplate() {
		_bContemplating = false;
	}
	
	IEnumerator RunMindController_Co() {
		
		yield return null;
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
	
	static class waspRoleBasic {
		public static float[] bmFindFood_Time_Energy() {
			 float[] rf = new float[4];
			rf[0] = 0.0f;
			rf[1] = 0.1f;
			rf[2] = 0.1f;
			rf[3] = 1.0f;
			
			return rf;
		}
		
		public static float[] bmFindFood_HiveFood_Hunger() {
			float[] rf = new float[4];
			rf[0] = 1.0f;
			rf[1] = 1.0f;
			rf[2] = 0.0f;
			rf[3] = 0.2f;
			
			return rf;
		}
			
		public static float[] bmFindFoodFinal_HiveFoodHunger_TimeEnergy() {
				float[] rf = new float[4];
				rf[0] = 0.0f;
			rf[1] = 0.2f;
			rf[2] = 0.3f;
			rf[3] = 1.0f;
			
			return rf;
			}
	};
}
