using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectorMenuBehaviour : MonoBehaviour {

    public GUIText inspectorGUIText;

    public bool bDisplay = true;

    const int inspectorX = 300;
    const int inspectorY = 400;

    List<string> lStrings = new List<string>();

    const string 
        test00 = "\n",
        test01 = "one: ",
        test02 = "wasp: ",
        test03 = "printing...";

    static Rect inspectorWindowRect;

	// Use this for initialization
	void Start () {
        //inspectorWindowRect = new Rect(Screen.width - inspectorX,
        //Screen.height / 2 + inspectorY / 2,
        //inspectorX,
        //inspectorY);

        inspectorWindowRect = new Rect(
            100,
            100,
            400,
            300
            );

        lStrings.Add(test01);
        lStrings.Add(test02);
        lStrings.Add(test03);

        
	}
	
	// Update is called once per frame
	void Update () {
        if (bDisplay) {
            do
            {
                StartCoroutine(DisplayInspectorText());
            } while (false);
        }
	}

    void OnEnable()
    {
        StartCoroutine(DisplayInspectorText());
    }

    IEnumerator DisplayInspectorText() {
        inspectorGUIText.text =
            test01 + test00 +
            test02 + test00 +
            test03 + test00;
        yield return new WaitForEndOfFrame();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(inspectorWindowRect);
        GUILayout.Box("Inspector");
        //GUILayout.BeginHorizontal();
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
        //GUILayout.te
    }
}
