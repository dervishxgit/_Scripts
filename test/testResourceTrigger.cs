using UnityEngine;
using System.Collections;

public class testResourceTrigger : MonoBehaviour {
	
	public Color myColor = Color.red;
	
	// Use this for initialization
	void Start () {
		//myColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("triggered resource");
		other.SendMessageUpwards("ReceiveColor", myColor, SendMessageOptions.DontRequireReceiver);
	}
}
