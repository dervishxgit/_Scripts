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
		zspeed = Datacore.fUserControlNavZ * Input.GetAxis("NavZ") * Time.deltaTime;
		yspeed = Datacore.fUserControlNavY * Input.GetAxis("NavY") * Time.deltaTime;
		xspeed = Datacore.fUserControlNavX * Input.GetAxis("NavX") * Time.deltaTime;
		
		
		
		gameObject.transform.Translate(xspeed, yspeed, zspeed);
	}
}
