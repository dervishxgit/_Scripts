using UnityEngine;
using System.Collections;

public class UserMotorBehaviour : MonoBehaviour {
	
	float zspeed;
	float yspeed;
	float xspeed;
			
	// Use this for initialization
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButton("Shift") ) {
			Datacore.fUserControlMult += Datacore.fUserControlMultStep * Time.deltaTime;
//			Debug.Log(Datacore.fUserControlMult);
		} else {
			
			Datacore.fUserControlMult = 1.0f;
//			Debug.Log (Datacore.fUserControlMult);
		}
		
		zspeed = Datacore.fUserControlNavZ * Input.GetAxis("NavZ")* Datacore.fUserControlMult * Time.deltaTime;
		yspeed = Datacore.fUserControlNavY * Input.GetAxis("NavY")* Datacore.fUserControlMult * Time.deltaTime;
		xspeed = Datacore.fUserControlNavX * Input.GetAxis("NavX")* Datacore.fUserControlMult * Time.deltaTime;
		
		
		
		
		
		gameObject.transform.Translate(xspeed, yspeed, zspeed);
	}
}
