using UnityEngine;
using System.Collections;

public class FocusTargetBehaviour : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseDown ()
	{
		if (gameObject.layer.ToString() == "Wasp") {
			//if wasp, make sure we get the root
			Debug.Log ("clicked wasp");
			Datacore._SetFocusTarget (gameObject.transform.root.gameObject);
		} else {
			Debug.Log ("clicked focus object");
			Datacore._SetFocusTarget (gameObject);
		}
	}
}
