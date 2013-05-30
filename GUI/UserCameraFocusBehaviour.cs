using UnityEngine;
using System.Collections;

public class UserCameraFocusBehaviour : MonoBehaviour
{
	
	GameObject focusTarget;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		if (Datacore.goFocusTarget != null) {
			//make my focus and track
			
			if (Input.GetButton ("AddFocus")) {
				Datacore._SetMouseLookState (Datacore.stateMouseOrbit);
				Vector3 vCamToTarget = Datacore.goFocusTarget.transform.position - gameObject.transform.root.position;
				vCamToTarget *= Datacore.fFocusDistanceMult;
				transform.Translate (vCamToTarget);
			} else if (Input.GetButtonDown ("RemoveFocus")) {
				Datacore._UnFocusTarget ();
				Datacore._SetMouseLookState (Datacore.stateMouseLook);
			}
			
		} else {
			//Datacore._SetMouseLookState(Datacore.stateMouseLook);
			return;
		}
		
	}

	static public void _SimulateAddFocus ()
	{
		Component cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UserCameraFocusBehaviour>();
		Datacore._SetMouseLookState (Datacore.stateMouseOrbit);
		Vector3 vCamToTarget = Datacore.goFocusTarget.transform.position - cam.gameObject.transform.root.position;
		vCamToTarget *= Datacore.fFocusDistanceMult;
		cam.transform.Translate (vCamToTarget);
	}
	
	static public void _SimulateRemoveFocus () {
		Datacore._UnFocusTarget ();
		Datacore._SetMouseLookState (Datacore.stateMouseLook);
	}
}
