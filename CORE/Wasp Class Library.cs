using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class _ListOfLevels_
{
	//List<string> levels = new List<string>();
	
	public static string[] levels = {
		"one", "two"	
	};
};

public static class WorldTime {
	
	public static double seconds,
	minutes,
	hours,
	days,
	months,
	years;
	
	public static void _UpdateTime( float time_secs ) {
		seconds += time_secs;
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

public class _Action_
{
		
};
	
public class _Role_
{
	string sName;
	List<_Action_> _lActions = new List<_Action_> ();
};

public class _FloatWrapper_ {
	public float f;
};

public class _Condition_
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
	
	public _Condition_ (string n, ref float mapValue)
	{
		this._SetCondition (n, ref mapValue);
	}
	
	public _Condition_ (string n, ref float mapValue, ref float minvalue, ref float maxvalue)
	{
		this._SetCondition (n, ref mapValue);
		this._SetMinMax (ref minvalue, ref maxvalue);
	}
	
	public _Condition_ (string n, ref float mapValue, ref List<_Condition_> lConditions)
	{
		//version that adds self to the list provided during build
		this._SetCondition (n, ref mapValue);
		lConditions.Add(this);
	}
	
	public _Condition_ (string n, ref float mapValue, ref float minvalue, ref float maxvalue, ref List<_Condition_> lConditions)
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
	
	public static _Condition_ _GetConditionByName (string n, List<_Condition_> lConditions)
	{
		_Condition_ resultCondition = lConditions.Find (
			delegate(_Condition_ con) {
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

public class _Question_
{
	public List<_Condition_> lConditions = new List<_Condition_>();
	
	public float _AnswerQuestion() {
		//not yet formalized
		return 0.0f;
	}
};

public class _Chemo_
{
	public Color chemoColor = Color.black;
	public float fInitialMemTime, fCurrentMemTime;
	float fMemDecayRate = 1.0f;
	float fDefaultInitialMemTime = 30.0f; //default seconds for Chemo to persist in memory
	
	//bool bIsDecaying = true; //not using
	
	public _Chemo_ ()
	{
		fInitialMemTime = fCurrentMemTime = fDefaultInitialMemTime;
	}
		
	public _Chemo_ (Color color)
	{
		fInitialMemTime = fCurrentMemTime = fDefaultInitialMemTime;
		chemoColor = color;
	}
		
	public _Chemo_ (_Chemo_ chem)
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

//public class WaspClassLibrary : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}
