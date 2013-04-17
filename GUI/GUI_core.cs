using UnityEngine;
using System.Collections;

public class GUI_Core : MonoBehaviour {
	
	//topmenu settings
	Vector2 topOrigin;
	Vector2 topSize;
	float fTopHeightPercent = 0.5f;
	
	int topMenuState = 0;
	const int topMenuState_NoDisplay = 0;
	const int topMenuState_Simulation = 1;
	const int topMenuState_Configuration = 2;
	const int topMenuState_Runtime = 3;
	
	string topMenuStateString = "_NoDisplay_";
	
	//sidebar settings
	Vector4 sBar; //.xyzw
	float fSBarWidthPercent = 0.2f;
	
	//bottom panel
	Vector2 bottomOrigin;
	Vector2 bottomSize;
	float fBottomSizePercent = 0.10f;
	
	// Use this for initialization
	void Start () {
		//top panel init
		topOrigin = new Vector2(0, 0);
		topSize = new Vector2(Screen.width, Screen.height * fTopHeightPercent);
		
		//sidebar init
		sBar = new Vector4(
			Screen.width - (Screen.width * fSBarWidthPercent), //originx
			0,												   //originy
			Screen.width * fSBarWidthPercent,				   //width
			Screen.height									   //height
			);
		
		//bottom panel init
		bottomOrigin = new Vector2(0, Screen.height - (Screen.height* fBottomSizePercent) );
		bottomSize = new Vector2(Screen.width, Screen.height * fBottomSizePercent);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Rect _CreateTopMenu()
	{
		return new Rect(topOrigin.x, topOrigin.y, topSize.x, topSize.y);
	}
	
	public void _SetTopMenuState(int state) {
		topMenuState = state;	
	}
	
	public void _SetTopMenuState(string state) {
//		switch( state ) {
//		case "Simulation":
//			_SetTopMenuState(1);
//			break;
//		case "Configuration":
//			_SetTopMenuState(2);
//			break;
//		case "Runtime":
//			_SetTopMenuState(3);
//			break;
//		}
		topMenuStateString = state;
	}
	
	public int _GetTopMenuState() {
		return topMenuState;	
	}
	
	public string _GetTopMenuStateString() {
		return topMenuStateString;	
	}
	
	public void _ResetTopMenuState() {
		topMenuStateString = "_NoDisplay_";	
	}
	
	public Rect _CreateBottomMenu() {
		return new Rect(bottomOrigin.x, bottomOrigin.y, bottomSize.x, bottomSize.y);
	}
	
	public Rect _CreateBottomMenu(float originx, float originy) {
		return new Rect(originx, originy, bottomSize.x, bottomSize.y);	
	}
	
	public Rect _CreateSidebar() {
		return new Rect(sBar.x, sBar.y, sBar.z, sBar.w);	
		//return new Rect(sBar.x, 0, 200, 200);
	}
	
	public Rect _CreateSidebar(float originx, float originy) {
		return new Rect(originx, originy, sBar.z, sBar.w);	
	}
	
	public Rect _CreateCenteredRect(float width, float height) {
			return new Rect(Screen.width/2 - width/2,
				Screen.height/2 - height/2,
				width, height
				);
		}
	public void _MoveRectToCenter(Rect r) {
		r.x = Screen.width/2 - r.width/2;
		r.y = Screen.height/2 - r.height/2;
	}
}
