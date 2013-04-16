using UnityEngine;
using System.Collections;

public class GUI_Interface : MonoBehaviour {
	
	public GUISkin _myGUISkin;
	public GUI_Core _guiCore;
	
	bool bDisplayAllMenus = false;
	
	// Use this for initialization
	void Start () {
		_guiCore = GetComponent<GUI_Core>() as GUI_Core;
	}
	
	// Update is called once per frame
	void Update () {
		//toggle for main menu
		if( Input.GetButtonUp("ToggleMainMenu") ) {
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
			
			//create bottommenu group
			GUI.BeginGroup(_guiCore._CreateBottomMenu() );
			GUI.Box(_guiCore._CreateBottomMenu(0,0), new GUIContent() );
			GUI.EndGroup();
			//end bottommenu
			
			//create sidebar group
			GUI.BeginGroup(_guiCore._CreateSidebar() );
			GUI.Box(_guiCore._CreateSidebar(0,0), new GUIContent() );
			GUI.EndGroup();
			//end sidebar
		} else {return;}
    }
}
		
