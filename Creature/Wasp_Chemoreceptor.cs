using UnityEngine;
using System.Collections;

public class Wasp_Chemoreceptor : MonoBehaviour {
	
	public Wasp_Core wCore;
	
	// Use this for initialization
	void Start () {
		//map to wasp core no matter where we are in hierarchy
		wCore = transform.root.gameObject.GetComponent<Wasp_Core>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
