using UnityEngine;
using System.Collections;

public class AutoFocusBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Datacore._SetFocusTarget(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
