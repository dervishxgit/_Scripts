using UnityEngine;
using System.Collections;

public class ResourceBasic_Behavior : MonoBehaviour {

		public float resourceLevel = 10.0f;
	//public Datacore dCore;
	

	
	// Use this for initialization
	void Awake () {
		//dCore = GameObject.FindGameObjectWithTag("CORE").GetComponent<Datacore>() as Datacore;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void tempEat() {
//		resourceLevel -= 0.1f;
//		if (resourceLevel <= 0) _selfDestroy();
		//_selfDestroy();
	}
	
	public void _selfDestroy() {
		gameObject.SendMessage("_selfDestroy");
		//Destroy(this, 1);
	}
	
	public void _DepleteResource() {
		BroadcastMessage("_DepleteResource", SendMessageOptions.DontRequireReceiver);
	}
}
