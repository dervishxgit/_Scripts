using UnityEngine;
using System.Collections;

public class GUI_Interface : MonoBehaviour {
	
	public GUISkin _myGUISkin;
	public GUI_Core _guiCore;
	
	bool bDisplayAllMenus = false;
	
	// Use this for initialization
	void Start () {
		//GUI.skin = myGUISkin;
		_guiCore = GetComponent<GUI_Core>() as GUI_Core;
	}
	
	// Update is called once per frame
	void Update () {
		//toggle for main menu
		if( Input.GetButtonUp("ToggleMainMenu") ) {
			//Debug.Log ("hit spacebar");
			bDisplayAllMenus = !bDisplayAllMenus;
		}
	}
	
	 void OnGUI() {
		//set custom skin
		GUI.skin = _myGUISkin;
		if(bDisplayAllMenus) {
			//initial test
			GUI.BeginGroup(_guiCore._CreateCenteredRect(Screen.width/4, Screen.height/4) );
	        GUI.Box(new Rect(0, 0, Screen.width/4, Screen.height/4), "This box is now centered! - here you would put your main menu", "box");
	        GUI.EndGroup();
			//create topmenu group
			GUI.BeginGroup(_guiCore._CreateTopMenu() );
			GUI.Box(_guiCore._CreateTopMenu(), new GUIContent() );
			GUI.EndGroup();
			//end top menu
		} else {return;}
    }
		
//	public Rect CreateCenteredRect(float width, float height) {
//			return new Rect(Screen.width/2 - width/2,
//				Screen.height/2 - height/2,
//				width, height
//				);
//		}
}
		
