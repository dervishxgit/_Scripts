using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectableObjectBehaviour : MonoBehaviour {

    bool bUpdatingStrings = false;

    float fUpdateInterval = 1.0f;

    public List<string> lsProperties = new List<string>();

    public List<string> lsValues = new List<string>();

    public int inspectableObjectType = 0; //set in editor
    const int
        typeTest = 0,
        typeWasp = 1,
        typeHive = 2,
        typePlant = 3;

    public struct stInspectorStrings {
       public const string 
            p_name = "Name: ",
            p_type = "Type: ",
            p_state = "State: ";
       public string
           v_name,
           v_type,
           v_state;

       //public string[] aProps;
       //public string[] aVals;
    };

    public stInspectorStrings _stins;

    void Awake() {
        BuildPropertiesList(_stins);
        
        switch(inspectableObjectType) {
            case typeTest:
                _stins.v_name = gameObject.name;
                _stins.v_type = "TEST";
                _stins.v_state = "testing...";
                break;

            case typeWasp:
                _stins.v_name = gameObject.name;
                _stins.v_type = "WASP";
                //_stins.v_state = gameObject.GetComponent<Wasp_Core>().stateOfMind.ToString();
                break;
        }

        BuildValuesList(_stins);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!bUpdatingStrings) {
            StartCoroutine(UpdateStrings());
        }
	}

    void OnMouseDown() {
        Datacore._SetInspectObject(gameObject);
        Debug.Log("set inspect" + gameObject.transform.root.gameObject.ToString());
    }

    IEnumerator UpdateStrings() {
        bUpdatingStrings = true;

        switch (inspectableObjectType)
        {
            case typeTest:
                _stins.v_name = gameObject.name;
                _stins.v_type = "TEST";
                _stins.v_state = "testing...";
                break;

            case typeWasp:
                _stins.v_name = gameObject.name;
                _stins.v_type = "WASP";
                //_stins.v_state = gameObject.GetComponent<Wasp_Core>().stateOfMind.ToString();
                break;
        }

        yield return new WaitForSeconds(fUpdateInterval);
        bUpdatingStrings = false;
    }

    void BuildPropertiesList(stInspectorStrings st) {
        lsProperties.Add(stInspectorStrings.p_name);
        lsProperties.Add(stInspectorStrings.p_type);
        lsProperties.Add(stInspectorStrings.p_state);

    }

    void BuildValuesList(stInspectorStrings st) {
        lsValues.Add(st.v_name);
        lsValues.Add(st.v_type);
        lsValues.Add(st.v_state);

    }
}
