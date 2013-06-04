using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectorMenuBehaviour : MonoBehaviour {

    public GUIText inspectorGUIText;

    InspectableObjectBehaviour inspectableObject;

    public bool bDisplay = true;
    bool btest = false;

    const int inspectorX = 300;
    const int inspectorY = 400;

    List<string> lStrings = new List<string>();



    const string 
        test00 = "\n",
        test01 = "one: ",
        test02 = "wasp: ",
        test03 = "printing...";

    static Rect inspectorWindowRect;

    string inspectorWindowText;

    void Awake () {
        inspectableObject = gameObject.GetComponent<InspectableObjectBehaviour>();
    }

	// Use this for initialization
	void Start () {
        //inspectorWindowRect = new Rect(Screen.width - inspectorX,
        //Screen.height / 2 + inspectorY / 2,
        //inspectorX,
        //inspectorY);

        inspectorWindowRect = new Rect(
            Screen.width - inspectorX,
            Screen.height/2,
            inspectorX,
            inspectorY
            );

        lStrings.Add(test01);
        lStrings.Add(test02);
        lStrings.Add(test03);

        //lsProperties.Add(test01);
        //lsProperties.Add(test02);
        //lsProperties.Add(test03);

        //lsValues.Add(test01);
        //lsValues.Add(test02);
        //lsValues.Add(test03);
	}
	
	// Update is called once per frame
	void Update () {
        if (bDisplay) {
            //do
            //{
            //    StartCoroutine(DisplayInspectorText());
            //} while (false);
            if (!btest) {
                ClearInspectorText();
                StartCoroutine(DisplayInspectorText(inspectableObject));
            }
        }
	}

    void OnEnable()
    {
        //StartCoroutine(DisplayInspectorText(inspectableObject));
    }

    //IEnumerator DisplayInspectorText() {
    //    inspectorGUIText.text =
    //        test01 + test00 +
    //        test02 + test00 +
    //        test03 + test00;

    //    inspectorWindowText =
    //        test01 + test00 +
    //        test02 + test00 +
    //        test03 + test00;
    //    yield return new WaitForEndOfFrame();
    //}

    //IEnumerator DisplayInspectorText()
    //{
    //    foreach (string s in lStrings) {
    //        inspectorWindowText += s + "\n";
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}

    IEnumerator DisplayInspectorText(InspectableObjectBehaviour inspect)
    {
        //string[] sprops = new string[lsProperties.Count];
        //string[] svals = new string[lsValues.Count];

        btest = true;

        string[] sprops = inspect.lsProperties.ToArray();
        string[] svals = inspect.lsValues.ToArray();

        for (int i = 0; i < sprops.Length; i++) { 
            inspectorWindowText += sprops[i] + " " + svals[i] + "\n";
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(5.0f);
        btest = false;
    }

    void ClearInspectorText() {
        inspectorWindowText = "";
    }

    //public void _SetProperties(List<string> props) {
    //    lsProperties = props;
    //}

    //public void _SetValues(List<string> vals) {
    //    lsValues = vals;
    //}

    void OnGUI()
    {
        GUILayout.BeginArea(inspectorWindowRect);
        GUILayout.Box(inspectorWindowText);
        //GUILayout.BeginHorizontal();
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
        //GUILayout.te
    }

    void OnMouseDown() { 
        
    }
}
