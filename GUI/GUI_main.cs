using UnityEngine;
using System.Collections;

public class GUI_main : MonoBehaviour {
	
	public Datacore _Datacore;
	
	// Use this for initialization
	void Start () {
		_Datacore = gameObject.GetComponent<Datacore>() as Datacore;
	}
	
	// Update is called once per frame
	void Update () {
		if(_Datacore.bTest == true) {Debug.Log("testing link confirmed");}
		//Debug.Log(_Datacore.bTest);
		//_Datacore.testFunc();
	}
}
