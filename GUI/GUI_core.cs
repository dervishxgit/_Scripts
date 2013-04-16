using UnityEngine;
using System.Collections;

public class GUI_Core : MonoBehaviour {
	
	//topmenu settings
	Vector2 topOrigin;
	Vector2 topSize;
	float fTopHeightPercent = 0.05f;
	
	// Use this for initialization
	void Start () {
		topOrigin = new Vector2(0, 0);
		topSize = new Vector2(Screen.width, Screen.height * fTopHeightPercent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Rect _CreateTopMenu()
	{
		return new Rect(topOrigin.x, topOrigin.y, topSize.x, topSize.y);
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
