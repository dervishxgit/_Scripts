using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _TestClass_
{
	
};

public class _Action_
{
		
};
	
public class _Role_
{
	string sName;
	List<_Action_> _lActions = new List<_Action_> ();
};

public class _Chemo_
{
	public Color chemoColor = Color.black;
	float fInitialMemTime, fCurrentMemTime;
	float fMemDecayRate = 1.0f;
	float fDefaultInitialMemTime = 30.0f; //default seconds for Chemo to persist in memory
		
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
		fCurrentMemTime = fCurrentMemTime - amount;// * fMemDecayRate;
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
	
	public float _GetCurrentMemTime() {
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
