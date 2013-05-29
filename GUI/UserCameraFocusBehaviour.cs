using UnityEngine;
using System.Collections;

public class UserCameraFocusBehaviour : MonoBehaviour {
	
	GameObject focusTarget;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Datacore.goFocusTarget != null) {
			//make my focus and track
			Datacore._SetMouseLookState(Datacore.stateMouseOrbit);
			if( Input.GetButton("AddFocus") ) {
				Vector3 vCamToTarget = Datacore.goFocusTarget.transform.position - gameObject.transform.root.position;
				vCamToTarget *= Datacore.fFocusDistanceMult;
				transform.Translate(vCamToTarget);
			} else if( Input.GetButton("RemoveFocus") ) {
				Datacore._UnFocusTarget();
			}
			
		} else {
			Datacore._SetMouseLookState(Datacore.stateMouseLook);
			return;
		}
		
	}
}
