using UnityEngine;
using System.Collections;

public class Wasp_Chemoreceptor : MonoBehaviour
{
	
	public Wasp_Core wCore;
	public _Chemo_ currentChemo;
	public Color currentChemoColor;
	public float fTriggered = 0.0f;
	public bool bTriggered = false;
	float fTriggeredTimeThreshold = 1.0f;
	float fReceptorDecayRate = 0.1f;
	
	//should only trigger on "Chemo" Layer
	void OnTriggerEnter (Collider other)
	{
		
	}
	
	void OnTriggerStay (Collider other)
	{
		currentChemo = other.transform.root.gameObject.GetComponent<ChemoBehavior> ()._Chemo;
		currentChemoColor = currentChemo.chemoColor;
		
		bTriggered = true;
		fTriggered = 0.0f;
		//Debug.Log ("did trigger");
	}
	
	void OnTriggerExit (Collider other)
	{
		//bTriggered = false;
		
		//we've left the chemo zone, make a copy of the sensation
		currentChemo = new _Chemo_ (other.transform.root.gameObject.GetComponent<ChemoBehavior> ()._Chemo);
	}
	
	// Use this for initialization
	void Start ()
	{
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		fTriggered += Time.deltaTime;
		if (fTriggered > fTriggeredTimeThreshold) {
			bTriggered = false;
		}
		if (currentChemo != null) {
			wCore._UpdateCurrentChemo (currentChemo);
			currentChemoColor = currentChemo.chemoColor;
			
			if (bTriggered == false) {
				bool expired;
				currentChemo._Run (0.1f, out expired);
				Debug.Log (currentChemo._GetCurrentMemTime ());
				//not smelling
				if (expired) {
					currentChemo = null;
					currentChemoColor = Color.black;
				}
			}
		} 
		
	}
}
