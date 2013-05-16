using UnityEngine;
using System.Collections;

public class GUI_LevelSelect : MonoBehaviour
{
	
	public int levelGridSelect = -1;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnGUI ()
	{
		GUILayout.BeginArea (
					new Rect (Screen.currentResolution.height / 2,
					Screen.currentResolution.width / 2,
					500,
					500) 
					);
		levelGridSelect = GUILayout.SelectionGrid (levelGridSelect, _ListOfLevels_.levels, 5);
		GUILayout.EndArea ();
	}
}
