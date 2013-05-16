using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class _ListOfLevels_
{
	//List<string> levels = new List<string>();
	
	public static string[] levels = {
		"one", "testCreatureMany"	
	};
};

public static class _WorldTime_ {
	
	public static double seconds,
	minutes,
	hours,
	days,
	months,
	years;
	
	public const float
		numMinutesPerDay = 1440.0f;
	
	public static void _UpdateTime( float time_secs ) {
		seconds += (double)time_secs;
		minutes = _GetMinutes();
		hours = _GetHours();
		days = _GetDays();
		months = _GetMonths();
		years = _GetYears();
	}
	
	public static double _GetSeconds() {
		return seconds;
	}
	
	public static float _GetSecondsR() {
		return Mathf.Round( (float) seconds );
	}
	
	public static float _GetSecondsRM() {
		return Mathf.Round( (float) seconds % 60 );
	}
	
	public static double _GetMinutes() {
		return seconds * (1.0/60.0);
	}
	
	public static float _GetMinutesR() {
		return Mathf.Round( (float)seconds * (1.0f/60.0f) );
	}
	
	public static float _GetMinutesRM() {
		return Mathf.Round( (float)seconds * (1.0f/60.0f) % 60);
	}
	
	public static double _GetHours() {
		return seconds * (1.0/3600.0);
	}
	
	public static float _GetHoursR() {
		return Mathf.Round( (float)seconds * (1.0f/3600.0f) );
	}
	
	public static float _GetHoursRM() {
		return Mathf.Round( (float)seconds * (1.0f/3600.0f) % 24 );
	}
	
	public static double _GetDays() {
		return seconds * (1.0/86400.0);
	}
	
	public static float _GetDaysR() {
		return Mathf.Round( (float)seconds * (1.0f/86400.0f) );
	}
	
	public static float _GetDaysRM() {
		return Mathf.Round( (float)seconds * (1.0f/86400.0f) % 30 );
	}
	
	public static double _GetMonths() {
		return seconds * (1.0/2592000.0);
	}
	
	public static float _GetMonthsR() {
		return Mathf.Round( (float)seconds * (1.0f/2592000.0f) );
	}
	
	public static float _GetMonthsRM() {
		return Mathf.Round( (float)seconds * (1.0f/2592000.0f) % 12 );
	}
	
	public static double _GetYears() {
		return seconds * (1.0/31104000.0);
	}
	
	public static float _GetYearsR() {
		return Mathf.Round( (float)seconds * (1.0f/31104000.0f) );
	}
	
	public static float _ReturnTotalTime() {
		return 0.0f;
	}
	
};

public class Action_
{
	string actionName;
	string actionType;
};
	
public class Role_
{
	/*
	 * Role should contain differing behavior matrices that will define behavior
	 * ideally, these matrices will be standardized by name so they can be 'expected' to be called
	 */ 
	string sName;
	
	
	
	List<Action_> _lActions = new List<Action_> ();
	List<Question_> _lQuestions = new List<Question_>();
};

public class FloatWrapper_ {
	public float f {
		get {return f;}
		set {f = value;}
	}
	
};

//public class Condition_
//{
//	/*
//	 * Conditions will exist in the wasp core, accessed by virtual mind
//	*/	
//	public string name;
//	public float fValue;
//	//possible calibration per condition
//	public float min, max;
//	
//	public float _FuzzOutMax() {
//		return AICORE._IsItMax(fValue, min, max);
//	}
//	
//	public float _FuzzOutMin() {
//		return AICORE._IsItMin(fValue, min, max);
//	}
//	
//	public Condition_ (string n, float mapValue)
//	{
//		this._SetCondition (n, mapValue);
//	}
//	
//	public Condition_ (string n, float mapValue, float minvalue, float maxvalue)
//	{
//		this._SetCondition (n, mapValue);
//		this._SetMinMax (minvalue, maxvalue);
//	}
//	
//	public Condition_ (string n, float mapValue, List<Condition_> lConditions)
//	{
//		//version that adds self to the list provided during build
//		this._SetCondition (n, mapValue);
//		lConditions.Add(this);
//	}
//	
//	public Condition_ (string n, float mapValue, float minvalue, float maxvalue, List<Condition_> lConditions)
//	{
//		//version that adds self to the list provided during build
//		this._SetCondition (n, mapValue);
//		this._SetMinMax(minvalue, maxvalue);
//		lConditions.Add(this);
//	}
//	
//	public void _SetName (string n)
//	{
//		this.name = n;
//	}
//	
//	public string _GetName ()
//	{
//		return this.name;
//	}
//	
//	public void _MapValue (float mapValue)
//	{
//		//should associate this value to watch another
//		this.fValue = mapValue;
//	}
//	
//	public float _GetValue ()
//	{
//		return fValue;
//	}
//	
//	public void _SetMinMax(float fmin, float fmax ) {
//		this.min = fmin;
//		this.max = fmax;
//	}
//	
//	public void _SetCondition (string n, float mapValue)
//	{
//		this._SetName (n);
//		this._MapValue (mapValue);
//	}
//	
//	public static Condition_ _GetConditionByName (string n, List<Condition_> lConditions)
//	{
//		Condition_ resultCondition = lConditions.Find (
//			delegate(Condition_ con) {
//			return con.name == n;
//		}
//		);
//		
//		if (resultCondition != null) {
//			return resultCondition;
//		} else {
//			return null;
//		}
//	}
//	
//};

//backup of Condition class
public class Condition_
{
	/*
	 * Conditions will exist in the wasp core, accessed by virtual mind
	*/	
	public string name;
	public float fValue;
	//possible calibration per condition
	public float min, max;
	
	public float _FuzzOutMax() {
		return AICORE._IsItMax(fValue, min, max);
	}
	
	public float _FuzzOutMin() {
		return AICORE._IsItMin(fValue, min, max);
	}
	
	public Condition_ (string n, ref float mapValue)
	{
		this._SetCondition (n, ref mapValue);
	}
	
	public Condition_ (string n, ref float mapValue, ref float minvalue, ref float maxvalue)
	{
		this._SetCondition (n, ref mapValue);
		this._SetMinMax (ref minvalue, ref maxvalue);
	}
	
	public Condition_ (string n, ref float mapValue, ref List<Condition_> lConditions)
	{
		//version that adds self to the list provided during build
		this._SetCondition (n, ref mapValue);
		lConditions.Add(this);
	}
	
	public Condition_ (string n, ref float mapValue, ref float minvalue, ref float maxvalue, ref List<Condition_> lConditions)
	{
		//version that adds self to the list provided during build
		this._SetCondition (n, ref mapValue);
		this._SetMinMax(ref minvalue, ref maxvalue);
		lConditions.Add(this);
	}
	
	public Condition_ (string n,  float mapValue,  float minvalue,  float maxvalue, ref List<Condition_> lConditions)
	{
		//version that adds self to the list provided during build
		this._SetCondition (n, ref mapValue);
		this._SetMinMax(ref minvalue, ref maxvalue);
		lConditions.Add(this);
	}
	
	public void _SetName (string n)
	{
		this.name = n;
	}
	
	public string _GetName ()
	{
		return this.name;
	}
	
	public void _MapValue (ref float mapValue)
	{
		//should associate this value to watch another
		this.fValue = mapValue;
	}
	
	public float _GetValue ()
	{
		return fValue;
	}
	
	public void _SetMinMax(ref float fmin, ref float fmax ) {
		this.min = fmin;
		this.max = fmax;
	}
	
	public void _SetCondition (string n, ref float mapValue)
	{
		this._SetName (n);
		this._MapValue (ref mapValue);
	}
	
	public static Condition_ _GetConditionByName (string n, List<Condition_> lConditions)
	{
		Condition_ resultCondition = lConditions.Find (
			delegate(Condition_ con) {
			return con.name == n;
		}
		);
		
		if (resultCondition != null) {
			return resultCondition;
		} else {
			return null;
		}
	}
	
};

public class BM_ {
	float[] mat;
};

//public static _BM_ operator +(_BM_ bm1, _BM_ bm2) {}
	


public class BM_2 :   BM_ {
	public float[] mat;
	
	BM_2() {
		mat = new float[4] {
			0.0f,
			0.0f,
			0.0f,
			0.0f
		};
	}
};

public class BM_3 {
	public float[] mat;
	
	BM_3() {
		mat = new float[8] {
			0.0f,
			0.0f,
			0.0f,
			0.0f,
			0.0f,
			0.0f,
			0.0f,
			0.0f
		};
	}
}



public class Answer_ {
	public float fAns; //fuzzy out
	public bool bThresh; //did fAns exceed the threshold we provided?
	public bool bRand; //result of coin flip
	
	public delegate Answer_ _Get();
	
};

public class Recommendation_ {
	public float testfloat;
	public Question_ quest;
	public Answer_ ans;
	
	public delegate Answer_ _Get();
};

public class Question_
{
	public string q;
	
	public List<Condition_> lConditions = new List<Condition_>();
	
	public Condition_[] aConditions;
	
	//public _BehaviorMatrix_;
	
//	function bool AnswerQuestion(
//	array<float> matBehavior,
//	optional float fCondition0,
//	optional float fCondition1,
//	optional float fCondition2
//	) {
//		local bool bAnswer;
//		local float z;
//		local array<float> conditions;
//		local float arrayfloat;
//
//		//add values to array (MUST BE IN CORRECT ORDER! :: )
//		conditions.AddItem(fCondition0);
//		conditions.AddItem(fCondition1);
//		if(fCondition2 > 0.0) {
//		conditions.AddItem(fCondition2);
//		}
//		//run behavior matrix output
//		if(matBehavior.length == 8 && conditions.length == 3) {
//			z = AIlib.static._BehaviorMatrix_3( matBehavior, conditions ); 
//		} else if (matBehavior.length == 4 && conditions.length == 2) {
//			z = AIlib.static._BehaviorMatrix_2( matBehavior, conditions );
//		} else {
//			`log("Answer unsuccessful due to incorrectly sized behavior matrix or incorrect number of conditions for matrix");
//			
//			`log("behavior matrix: ");
//			foreach matBehavior(arrayfloat) {
//				`log(" "$arrayfloat$", ");
//			}
//			`log("conditions: ");
//			foreach conditions(arrayfloat) {
//				`log(" "$arrayfloat$", ");
//			}
//			return false;
//		}
//
//		//return weighted random yes or no
//		bAnswer = AIlib.static._RandomProbability( z );
//		return bAnswer;
//}
	
	//version takes array of conditions and behavior matrix, returns float
	public static float _AnswerQuestion_f( float[] B, float[] C ) {
		//will call ailib depending one sizes values provided
		if( B.Length == 4 && C.Length == 2) {
			return AICORE._BehaviorMatrix2(B, C);
		}
		else if ( B.Length == 8 && C.Length == 3) {
			return AICORE._BehaviorMatrix3(B, C);
		}
		else {
			Debug.Log("Improperly sized behavior matrix of number of conditions.");
			return -1.0f;
		}
	}
	
	public static bool _AnswerQuestion_b( float[] B, float [] C ) {
		float z;
		
		if( B.Length == 4 && C.Length == 2) {
			z = AICORE._BehaviorMatrix2(B, C);
		}
		else if ( B.Length == 8 && C.Length == 3) {
			z =  AICORE._BehaviorMatrix3(B, C);
		}
		else {
			z = 0.0f;
			Debug.Log("Improperly sized behavior matrix of number of conditions.");
		}
		
		return AICORE._RandomProbability( z );
		
	}
	
	//version takes 2 conditions raw and answers using behavior matrix
	public static float _AnswerQuestion_2Con(
		float[] B, Condition_[] C
		) {
		
		
		//not yet formalized
		return 0.0f;
	}
	
	//version takes 3 conditions raw and answers using behavior matrix
	public static float _AnswerQuestion_3Con(
		
		) {
		//not yet formalized
		return 0.0f;
	}
	
	public static Answer_ _AnswerQuestion(Wasp_Core wasp, Question_ quest, float threshold) {
		Answer_ rAns = new Answer_();
		
		return rAns;
	}
};

public class Chemo_
{
	public Color chemoColor = Color.black;
	public float fInitialMemTime, fCurrentMemTime;
	float fMemDecayRate = 1.0f;
	float fDefaultInitialMemTime = 30.0f; //default seconds for Chemo to persist in memory
	
	//bool bIsDecaying = true; //not using
	
	public Chemo_ ()
	{
		fInitialMemTime = fCurrentMemTime = fDefaultInitialMemTime;
	}
		
	public Chemo_ (Color color)
	{
		fInitialMemTime = fCurrentMemTime = fDefaultInitialMemTime;
		chemoColor = color;
	}
		
	public Chemo_ (Chemo_ chem)
	{
		chemoColor = chem.chemoColor;
		fInitialMemTime = chem.fInitialMemTime;
		fCurrentMemTime = chem.fCurrentMemTime;
	}
		
	bool _Decay (float amount)
	{
		fCurrentMemTime -= amount * fMemDecayRate;
		
		//test
		chemoColor *= 1.0f - amount;
		
		return fCurrentMemTime <= 0.0f;
	}
		
	public void _Run (float fTime, out bool bExpired)
	{
		bExpired = _Decay (fTime);
	}
		
	public void _Refresh ()
	{
		fCurrentMemTime = fInitialMemTime;
	}
	
	public float _GetCurrentMemTime ()
	{
		return fCurrentMemTime;
	}
		
		
};
