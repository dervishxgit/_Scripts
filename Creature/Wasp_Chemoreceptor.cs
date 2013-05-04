using UnityEngine;
using System.Collections;

public class Wasp_Chemoreceptor : MonoBehaviour {
	
	public Wasp_Core wCore;
	
	public Wasp_Core._Chemo_ currentChemo;
	
	public Color currentChemoColor;
	
	float fTriggered = 0.0f;
	bool bTriggered = false;
	float fTriggeredTimeThreshold = 1.0f;
	
	//should only trigger on "Chemo" Layer
	void OnTriggerEnter(Collider other) {
		
	}
	
	void OnTriggerStay(Collider other) {
		currentChemo = other.transform.root.gameObject.GetComponent<ChemoBehavior>()._Chemo;
		currentChemoColor = other.transform.root.gameObject.GetComponent<ChemoBehavior>()._Chemo.chemoColor;
		
		bTriggered = true;
		fTriggered = 0.0f;
		Debug.Log("did trigger");
	}
	
	// Use this for initialization
	void Start () {
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core>();
	}
	
	// Update is called once per frame
	void Update () {
		fTriggered += Time.deltaTime;
		if(fTriggered > fTriggeredTimeThreshold) {
			bTriggered = false;
		}
		if(currentChemo != null) {
			wCore._UpdateCurrentChemo(currentChemo);
			currentChemoColor = currentChemo.chemoColor;
			
			if(!bTriggered) {
				//decay our chemocolor
				currentChemo.chemoColor *= 0.9f;
			}
		}
		
	}
}
