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
	
	public _Condition_ (string n, ref float mapValue)
	{
		this._SetCondition (n, ref mapValue);
	}
	
	public _Condition_ (string n, ref float mapValue, ref List<_Condition_> lConditions)
	{
		//version that adds self to the list provided during build
		this._SetCondition (n, ref mapValue);
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
	
};

public class _Chemo_
{
	public Color chemoColor = Color.black;
	public float fInitialMemTime, fCurrentMemTime;
	float fMemDecayRate = 1.0f;
	float fDefaultInitialMemTime = 3.0f; //default seconds for Chemo to persist in memory
	
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
