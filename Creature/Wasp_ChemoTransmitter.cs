using UnityEngine;
using System.Collections;

public class Wasp_ChemoTransmitter : MonoBehaviour {
	
	public Wasp_Core wCore;
	
	public _Chemo_ chemTransmit;
	
	// Use this for initialization
	void Start () {
		if(chemTransmit == null) {
			chemTransmit = new _Chemo_();
		}
		
		wCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Wasp_Core>() as Wasp_Core;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void _SpawnChemoBehavior(_Chemo_ chem) {
		//Debug.Log("spawning chemo");
		//test of chemo transmit
		GameObject cheminst = Instantiate(wCore.chemoBehaviorPrefab)as GameObject;
		ChemoBehavior chembehave = cheminst.GetComponent<ChemoBehavior>();
		chembehave._Initialize(5.0f, 10.0f, new _Chemo_(), Color.green);
		chembehave.SendMessage("_SetReady", true, SendMessageOptions.DontRequireReceiver);
	}
	
	public void _SpawnChemoBehavior(GameObject chemPrefab, _Chemo_ chem, Color col) {
		//Debug.Log("spawning chemo");
		//test of chemo transmit
		GameObject cheminst = Instantiate(chemPrefab, transform.position, transform.rotation)as GameObject;
		ChemoBehavior chembehave = cheminst.GetComponent<ChemoBehavior>();
		chembehave._Initialize(5.0f, 10.0f, chem, col);
		chembehave.SendMessage("_SetReady", true, SendMessageOptions.DontRequireReceiver);
	}
	
	public void _SpawnChemoBehavior(GameObject chemPrefab, _Chemo_ chem, Color col, float lifetime ) {
		//Debug.Log("spawning chemo");
		//test of chemo transmit
		GameObject cheminst = Instantiate(chemPrefab, transform.position, transform.rotation)as GameObject;
		ChemoBehavior chembehave = cheminst.GetComponent<ChemoBehavior>();
		chembehave._Initialize(5.0f, lifetime, chem, col);
		chembehave.SendMessage("_SetReady", true, SendMessageOptions.DontRequireReceiver);
	}
}
