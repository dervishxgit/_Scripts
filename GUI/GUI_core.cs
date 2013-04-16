using UnityEngine;
using System.Collections;

public class GUI_Core : MonoBehaviour {
	
	//topmenu settings
	Vector2 topOrigin;
	Vector2 topSize;
	float fTopHeightPercent = 0.05f;
	
	//sidebar settings
	Vector4 sBar; //.xyzw
	float fSBarWidthPercent = 0.25f;
	
	//bottom panel
	Vector2 bottomOrigin;
	Vector2 bottomSize;
	float fBottomSizePercent = 0.1f;
	
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
