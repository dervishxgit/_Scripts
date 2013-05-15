using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hive_ : MonoBehaviour {
	
	bool _bUpdatingHive = false;
	
	public List<Wasp_Core> _memberWasps = new List<Wasp_Core>();
	
	public GameObject waspQueen;
	
	class HiveFood {
		public float minFood = 0.0f, 
		maxFood = 10.0f,
		totalFood = 1.0f, 
		avgFood = 0.1f;
		
		float outFood = 1.0f, inFood = 1.0f;//mult
		
		public float _GetAvgFood() {
			return avgFood = totalFood/maxFood;
		}
	};
	
	HiveFood _hvFood = new HiveFood();
	
	public float _GetHiveFoodAvg() {
		return _hvFood._GetAvgFood();
	}
	
	public float _GetHiveFoodMin() {
		return _hvFood.minFood;
	}
	
	public float _GetHiveFoodMax() {
		return _hvFood.maxFood;
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!_bUpdatingHive) {
			StartCoroutine(_UpdateHive());
		}
		
	}
	
	IEnumerator _UpdateHive () {
		_bUpdatingHive = true;
		_hvFood.minFood = _memberWasps.Count/10.0f;
		_hvFood.maxFood = _memberWasps.Count;
		_bUpdatingHive = false;
		yield return null;
		//update avgs
		_bUpdatingHive = true;
		_hvFood.avgFood = _hvFood.totalFood / _hvFood.maxFood;
		_bUpdatingHive = false;
		yield return null;
	}
	
	public void _WaspJoin(Wasp_Core wasp) {
		_memberWasps.Add(wasp);
	}
}
