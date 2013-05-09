using UnityEngine;
using System.Collections;

public class Wasp_Chemoreceptor : MonoBehaviour
{
	
	/*
	 * Wasp_Chemoreceptor (c) 2013 - Steve Miller
	 * 
	 * Chemoreceptor should, while in the zone get a _Chemo_ sense and send that to the Wasp Core
	 * the core will add if it is a new sense, or update if it is an existing sense
	 * 
	 * -problem: the receptor is trying to access the same chem after it has been passed to the wasp core,
	 * 		but the core is refreshing the current one while in memory
	 */
	
	public Wasp_Core wCore;
	public _Chemo_ currentChemo = new _Chemo_();
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
		currentChemo.chemoColor = other.transform.root.gameObject.GetComponent<ChemoBehavior> ()._Chemo.chemoColor;
		//currentChemoColor = currentChemo.chemoColor;
		
		bTriggered = true;
		fTriggered = 0.0f;
		//Debug.Log ("did trigger");
	}
	
	void OnTriggerExit (Collider other)
	{
		//bTriggered = false;
		
		//we've left the chemo zone, make a copy of the sensation
		//currentChemo = new _Chemo_ (other.transform.root.gameObject.GetComponent<ChemoBehavior> ()._Chemo);
		//Debug.Log ("should see this once");
		
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
			//wCore._UpdateCurrentChemo (currentChemo);
			currentChemoColor = currentChemo.chemoColor;
			
			if (bTriggered == false) {
				//bool expired;
				//currentChemo._Run (Time.deltaTime, out expired);
				//Debug.Log ("yes we are hitting this");
				//Debug.Log (currentChemo._GetCurrentMemTime ());
				//Debug.Log (currentChemo.fCurrentMemTime);
				//not smelling
//				if (expired) {
//					currentChemo = null;
//					currentChemoColor = Color.black;
//				}
			} else wCore._UpdateCurrentChemo (currentChemo);
		} 
		
	}
}
