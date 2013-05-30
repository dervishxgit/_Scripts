using UnityEngine;
using System.Collections;

public class UserMotorBehaviour : MonoBehaviour {
	
	float zspeed;
	float yspeed;
	float xspeed;
			
	// Use this for initialization
	float fInMenuMult = 0.01f;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch(Datacore.bDisplayAllMenus) {
		case true:
			if ( Input.GetButton("Shift") ) {
			Datacore.fUserControlMult += Datacore.fUserControlMultStep * fInMenuMult;
//			Debug.Log(Datacore.fUserControlMult);
		} else {
			
			Datacore.fUserControlMult = 1.0f;
//			Debug.Log (Datacore.fUserControlMult);
		}
		
		zspeed = Datacore.fUserControlNavZ * Input.GetAxis("NavZ")* Datacore.fUserControlMult * fInMenuMult;
		yspeed = Datacore.fUserControlNavY * Input.GetAxis("NavY")* Datacore.fUserControlMult * fInMenuMult;
		xspeed = Datacore.fUserControlNavX * Input.GetAxis("NavX")* Datacore.fUserControlMult * fInMenuMult;
			break;
			
		case false:
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
			break;
		}
		
		
		
		
		
		
		gameObject.transform.Translate(xspeed, yspeed, zspeed);
	}
}
