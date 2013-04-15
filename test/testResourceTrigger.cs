using UnityEngine;
using System.Collections;

public class testResourceTrigger : MonoBehaviour {
	
	public Color myColor = Color.red;
	
	public bool bDoesDecay = true;
	public float fDecayRate = 1.0f;
	public float fLifeSpanSeconds = 5.0f;
	
	// Use this for initialization
	void Start () {
		//myColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(bDoesDecay == true) {
			_Decay(Time.deltaTime * fDecayRate);
		}
		
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("triggered resource");
		other.SendMessageUpwards("ReceiveColor", myColor, SendMessageOptions.DontRequireReceiver);
	}
	
	void _SetMyColor(Color c) {
		myColor = c;
	}
	
	void _Decay(float rate) {
		//decrease our lifespan by our decay rate
		fLifeSpanSeconds -= rate;
		if(fLifeSpanSeconds < 0.0) {Destroy(gameObject);}
	}
}
