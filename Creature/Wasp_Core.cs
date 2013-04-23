using UnityEngine;
using System.Collections;

public class Wasp_Core : MonoBehaviour {
	
	//References
	public Datacore dCore;
	private Wasp_Core wCore;
	public GameObject waspRoot;
	public GameObject waspGeo;
	
	// Use this for initialization
	void Start () {
		//establish connection to creature core and global datacore
		dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
		wCore = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
