using UnityEngine;
using System.Collections;

public class Datacore : MonoBehaviour {
	
	public bool bDisplayAllMenus = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ToggleAllMenus(bool b) {
		bDisplayAllMenus = b;	
	}
}
