using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Interface : MonoBehaviour
{
	
	public GUISkin _myGUISkin;
	public GUI_Core _guiCore;
	//bool bDisplayAllMenus = false;
	bool bDisplaySimMenu = false;
	bool bDisplayConfigurationMenu = false;
	bool bDisplayRuntimeMenu = false;
	bool bDisplayTime = true;
	
	int bottomPanelGridSelection;
	string[] bottomPanelGridStrings = {"Focus", "Orbit", "Goto", "LookThrough", "TakeControl"};
	
	int levelGridSelect = -1;
	public GameObject levelmenu;
	
	GUILayoutOption[] bottomPanelLayoutOptions = {GUILayout.MaxWidth(300)};
	
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
			//Datacore.bDisplayAllMenus = !Datacore.bDisplayAllMenus;
			Datacore.ToggleAllMenus();
		}
		
		
	}
	
	void OnGUI ()
	{
		//set custom skin
		GUI.skin = _myGUISkin;
		
		if (Datacore.bDisplayAllMenus) {
			//test for datacore to set mousemenu
			//Datacore._SetMouseLookState(Datacore.stateMouseMenu);
			
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
				if(bDisplaySimMenu) {
				 	_guiCore._SetTopMenuState ("_Simulation_");
				} else _guiCore._ResetTopMenuState();
				
			}
			if (GUILayout.Button ("Configuration")) {
				Debug.Log ("user clicked configutation menu");
				bDisplayConfigurationMenu = !bDisplayConfigurationMenu;
				if(bDisplayConfigurationMenu) {
					_guiCore._SetTopMenuState ("_Configuration_");	
				} else _guiCore._ResetTopMenuState();
				
			}
			if (GUILayout.Button ("Runtime")) {
				Debug.Log ("user clicked runtime menu");
				bDisplayRuntimeMenu = !bDisplayRuntimeMenu;
				if(bDisplayRuntimeMenu) {
					_guiCore._SetTopMenuState ("_Runtime_");	
				} else _guiCore._ResetTopMenuState();				
			}
			
			switch (_guiCore._GetTopMenuStateString ()) {
			case "_NoDisplay_":
				break;
			case "_Simulation_":
//				
				GUILayout.BeginArea (new Rect (5, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth (50));
				if (GUILayout.Button ("New")) {
					Application.LoadLevel("testEnv");	
				}
				if (GUILayout.Button ("Save")) {
						
				}
				if (GUILayout.Button ("Load")) {
					Component levels = gameObject.AddComponent("GUI_LevelSelect");
					levelGridSelect = levels.GetComponent<GUI_LevelSelect>().levelGridSelect;
					GUILayout.BeginVertical();
					
					//int select = 0;
					//string[] guilevelstrings = new string[_ListOfLevels_.levels.Length];
					//for(int i = 0; i < _ListOfLevels_.levels.Length; i++) {
//						if(GUILayout.Button(_ListOfLevels_.levels[i])) {
//							Application.LoadLevel(_ListOfLevels_.levels[i]);
//						}

					//}
					
					if(levelGridSelect != -1) {
						Application.LoadLevel(_ListOfLevels_.levels[levelGridSelect]);
					}
					GUILayout.EndVertical();
				}
				if (GUILayout.Button ("Exit")) {
					Application.Quit();	
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
				break;
				
			case "_Configuration_":
				GUILayout.BeginArea (new Rect (100, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth (50));
				if (GUILayout.Button ("Global Settings")) {
						
				}
				if (GUILayout.Button ("Conditions")) {
						
				}
				if (GUILayout.Button ("Current Creature")) {
						
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();	
				break;
				
			case "_Runtime_":
				GUILayout.BeginArea (new Rect (150, 30, 600, 600));
				GUILayout.BeginVertical (GUILayout.MaxWidth (50));
				if (GUILayout.Button ("TimeScale")) {
						
				}
				if (GUILayout.Button ("Pause/Play")) {
						
				}
				GUILayout.EndVertical ();
				GUILayout.EndArea ();
				break;
			}
			
			GUILayout.EndArea ();
			//end top menu
			
			//create bottommenu group
			GUILayout.BeginArea (_guiCore._CreateBottomMenu ());
			//GUILayout.Box (new GUIContent ());
			//string[] bottomPanelGridStrings = {"Focus", "Orbit", "Goto", "LookThrough", "TakeControl"};
			bottomPanelGridSelection = GUILayout.SelectionGrid(bottomPanelGridSelection, bottomPanelGridStrings, 3, bottomPanelLayoutOptions);  
			switch(bottomPanelGridSelection) {
			case 0: //Focus
				//UserCameraFocusBehaviour._SimulateRemoveFocus();
				break;
			case 1:	//Orbit (not used yet)
				
				break;
			case 2: //Goto (same as addfocus right now)
				if(Datacore.goFocusTarget != null) {
					UserCameraFocusBehaviour._SimulateAddFocus();
				}
				break;
			case 5:	//
				
				break;
			}
			GUILayout.EndArea ();
			//end bottommenu
			
			//create sidebar group
			GUILayout.BeginArea (_guiCore._CreateSidebar ());
			GUILayout.BeginVertical();
			GUILayout.Box (new GUIContent ("Metrics") );
			GUILayout.Box (new GUIContent ("Inspector") ); 
			GUILayout.EndVertical();
			GUILayout.EndArea ();
			//end sidebar
			
		} else {
			return;
		}
	}
}
		
