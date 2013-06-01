using UnityEngine;
using System.Collections;

public class MenuPrompt : MonoBehaviour {
	
	public GUIText text;
	
	// Use this for initialization
	void Awake () {
		text = gameObject.GetComponent<GUIText>();
	}
	
	void Start () {
		text.text = "Press " + Datacore.sToggleMainMenuButton + " for Main Menu";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
