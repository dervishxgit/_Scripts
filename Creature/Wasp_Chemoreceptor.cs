using UnityEngine;
using System.Collections;

public class Wasp_Chemoreceptor : MonoBehaviour {
	
	public Wasp_Core wCore;
	
	public Wasp_Core._Chemo_ currentChemo;
	
	//should only trigger on "Chemo" Layer
	void OnTriggerEnter(Collider other) {
		
	}
	
	void OnTriggerStay(Collider other) {

	}
	
	// Use this for initialization
	void Start () {
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
