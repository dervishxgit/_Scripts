using UnityEngine;
using System.Collections;

public class UserCameraBehaviour : MonoBehaviour {
	
	GameObject rainEffect;
	
	public GameObject rainCameraEffectPrefab;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void _DestroyRainEffect() {
		if(rainEffect != null) Destroy(rainEffect, 1.0f);
	}
	
	public void _SpawnRainEffect() {
		rainEffect = GameObject.Instantiate(
			rainCameraEffectPrefab, gameObject.transform.position, gameObject.transform.rotation) 
			as GameObject;
		
		rainEffect.transform.parent = gameObject.transform;
	}
}
