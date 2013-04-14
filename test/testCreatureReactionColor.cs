using UnityEngine;
using System.Collections;

public class testCreatureReactionColor : MonoBehaviour {

	public Color lerpColor = Color.white;
	public Color colorstart = Color.red;
	public Color colorend = Color.blue;
	public float duration = 10.0f;
	
	public Color myReceivedColor = Color.black;
	
	public GameObject prefabColor;
	
	public bool bProduced = false;
	
	// Use this for initialization
	void Start () {
		//test initmaterial
		//renderer.material.color = Color.black;
		
		renderer.material.color = lerpColor;
	}
	
	// Update is called once per frame
	void Update () {
		//lerpColor = Color.Lerp(lerpColor, myReceivedColor, Time.time);
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		
		//renderer.material.color = Color.Lerp (colorstart, colorend, lerp);
		renderer.material.color = Color.Lerp( lerpColor, myReceivedColor, lerp );
		
		
	}
	
	void ReceiveColor(Color c) {
		Debug.Log("receivingColor");
		Debug.Log(c);
		myReceivedColor = c;
		
		//test for feedback/output
		if(bProduced == false) {
			_ProduceColor(myReceivedColor);
			bProduced = true;
		}
		
	}
	
	void _ProduceColor(Color c) {
		GameObject producedColor = Instantiate(prefabColor, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		producedColor.BroadcastMessage("_SetMyColor", c, SendMessageOptions.DontRequireReceiver);
	}
}
