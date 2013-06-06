using UnityEngine;
using System.Collections;

public class titleWait : MonoBehaviour {

	// Use this for initialization
	void Start () {
		_DoLoad();
	}
	
	void Awake () {
			
	}
	
	void _DoLoad() {
		StartCoroutine(_LoadNextTitle(3.0f));
	}
	
	IEnumerator _LoadNextTitle (float waitTime) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(1);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
