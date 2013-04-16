using UnityEngine;
using System.Collections;

public class GUI_Interface : MonoBehaviour
{
	
	public GUISkin _myGUISkin;
	public GUI_Core _guiCore;
	bool bDisplayAllMenus = false;
	bool bDisplaySimMenu = false;
	bool bDisplayConfigurationMenu = false;
	bool bDisplayRuntimeMenu = false;
	
	//top menu toolbar
	int iTopMenuInt = 0;
	
	// Use this for initialization
	void Start ()
	{
		_guiCore = GetComponent<GUI_Core> () as GUI_Core;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//toggle for main menu
		if (Input.GetButtonUp ("ToggleMainMenu")) {
			//Debug.Log ("space");
			bDisplayAllMenus = !bDisplayAllMenus;
		}
	}
	
	void OnGUI ()
	{
		//set custom skin
		GUI.skin = _myGUISkin;
		
		if (bDisplayAllMenus) {
			//following are examples of fixed layout
			
//			//initial test
//			GUI.BeginGroup(_guiCore._CreateCenteredRect(Screen.width/4, Screen.height/4) );
//	        GUI.Box(new Rect(0, 0, Screen.width/4, Screen.height/4), "This box is now centered! - here you would put your main menu", "box");
//	        GUI.EndGroup();
//			//create topmenu group
//			GUI.BeginGroup(_guiCore._CreateTopMenu() );
//			GUI.Box(_guiCore._CreateTopMenu(), new GUIContent() );
//			GUI.EndGroup();
//			//end top menu
//			
//			//create bottommenu group
//			GUI.BeginGroup(_guiCore._CreateBottomMenu() );
//			GUI.Box(_guiCore._CreateBottomMenu(0,0), new GUIContent() );
//			GUI.EndGroup();
//			//end bottommenu
//			
//			//create sidebar group
//			GUI.BeginGroup(_guiCore._CreateSidebar() );
//			GUI.Box(_guiCore._CreateSidebar(0,0), new GUIContent() );
//			GUI.EndGroup();
			//end sidebar
			//end fixed layout
			///////////
			
			//automatic layout:
			
			//create topmenu group
			//GUILayout.BeginArea(_guiCore._CreateTopMenu() );
			GUILayout.BeginArea (_guiCore._CreateTopMenu ());
			GUILayout.BeginHorizontal ();
			//GUILayout.Box ("topboxlettersletters");
			if (GUILayout.Button ("Simulation")) {
				Debug.Log ("user clicked simulation button");
				bDisplaySimMenu = !bDisplaySimMenu;
			}
			if (bDisplaySimMenu) {
				GUILayout.BeginArea (new Rect (5, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth(50));
				if (GUILayout.Button ("New")) {
						
				}
				if (GUILayout.Button ("Save")) {
						
				}
				if (GUILayout.Button ("Load")) {
						
				}
				if (GUILayout.Button ("Exit")) {
						
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
			}
			if (GUILayout.Button ("Configuration")) {
				Debug.Log ("user clicked configutation menu");
				bDisplayConfigurationMenu = !bDisplayConfigurationMenu;
			}
			if (bDisplayConfigurationMenu) {
				GUILayout.BeginArea (new Rect (100, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth(50));
				if (GUILayout.Button ("Global Settings")) {
						
				}
				if (GUILayout.Button ("Conditions")) {
						
				}
				if (GUILayout.Button ("Current Creature")) {
						
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();	
			}
			if (GUILayout.Button ("Runtime")) {
				Debug.Log ("user clicked runtime menu");
				bDisplayRuntimeMenu = !bDisplayRuntimeMenu;
			}
			if (bDisplayRuntimeMenu) {
				GUILayout.BeginArea (new Rect (150, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth(50));
				if (GUILayout.Button ("TimeScale")) {
						
				}
				if (GUILayout.Button ("Pause/Play")) {
						
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();	
			}
			GUILayout.EndArea ();
			//end top menu
			
			//create bottommenu group
			GUILayout.BeginArea (_guiCore._CreateBottomMenu ());
			GUILayout.Box (new GUIContent ());
			GUILayout.EndArea ();
			//end bottommenu
			
			//create sidebar group
			GUILayout.BeginArea (_guiCore._CreateSidebar ());
			GUILayout.Box (new GUIContent ());
			GUILayout.EndArea ();
			//end sidebar
			
		} else {
			return;
		}
	}
}
		
