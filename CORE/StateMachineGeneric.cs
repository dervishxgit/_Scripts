using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineGeneric : MonoBehaviour {
	
	public class _Transition_ {
		_State_  next;
	};
	
	public class _State_ {
		public string sName;
		
		public List<_Transition_> _lTransitions = new List<_Transition_>();
		
		void _SetName(string name) {sName = name;}
		
		bool _Initialize() {
			
			
			return true;
		}
	};
	
	public List<_State_> _lStates = new List<_State_>();
	
	
	public void _InitializeStates(List<_State_> lstates) {
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
