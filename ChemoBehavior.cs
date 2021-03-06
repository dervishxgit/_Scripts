using UnityEngine;
using System.Collections;

public class ChemoBehavior : MonoBehaviour
{
	
	/*
	 * ChemoBehavior (c) 2013 - Steve Miller
	 * 
	 * main script for the chemo unit
	 * 
	 * this HAS A _Chemo_ instance that is transmitted to the wasps' receptors (for sensation)
	 * 
	 * other behavior is for self managing object
	 * 
	 * object will need to be able to be created and will create its own components when instantiated
	 * depending on the settings we provide it
	 * 
	 * Chemo.chemocolor should determine the color of the material and the decay
	 */ 
	float fRadius = 10.0f, fLifeTime = 10.0f, fLifeTimeRemaining = 10.0f;
	public GameObject chemoSpherePrefab;
	GameObject mysphere;
	Material sphereMaterial;
	float sphereMaxAlpha = 0.9f;
	float sphereMinAlpha = 0.1f;
	public Chemo_ _Chemo;// = new _Chemo_ ();
	int iMasterState = 0;
	const int iStateNotReady = 0;
	const int iStateReady = 1;
	const int iStateRunning = 2;
	
//	ChemoBehavior ()
//	{
//		
//	}
//	
//	ChemoBehavior (float radius, float lifetime, _Chemo_ chem)
//	{
//		fRadius = radius;
//		fLifeTimeRemaining = fLifeTime = lifetime;
//		
//		_Chemo = chem;
//	}
	
	void Decay (float fAmount, bool bDestroyWhenFinished)
	{
		fLifeTimeRemaining -= fAmount;
		if (bDestroyWhenFinished) {
			if (fLifeTimeRemaining <= 0.0f) {
				Destroy (gameObject);
			}	
		}
		
		//dynamic resize test
		//_ResizeChemoSphere(fLifeTimeRemaining);
		
		//dynamic alpha
//		_SetChemoSphereAlpha (Mathf.Clamp (
//			AICORE._IsItMax (fLifeTimeRemaining, 0.0f, fLifeTime), 
//			sphereMinAlpha, sphereMaxAlpha)
//			);
		
		float newAlpha = Mathf.Clamp (
			AICORE._IsItMax (fLifeTimeRemaining, 0.0f, fLifeTime), 
			sphereMinAlpha, sphereMaxAlpha);
		
		_SetChemoSphereAlpha (newAlpha);
		
		_Chemo.chemoColor.a = sphereMaterial.color.a;
	}
	
	void _InitializeChemoSphere ()
	{
		//spawn and map
		mysphere = Instantiate (chemoSpherePrefab, transform.position, transform.rotation) as GameObject;
		
		mysphere.transform.root.parent = gameObject.transform;
		
		//mysphere.BroadcastMessage("_ResizeSphere", fRadius, SendMessageOptions.DontRequireReceiver);
		_ResizeChemoSphere (fRadius);
		
		sphereMaterial = mysphere.GetComponent<Renderer> ().material;
		
		sphereMaterial.color = _Chemo.chemoColor;
		
		_SetChemoSphereAlpha (sphereMaxAlpha);
		
	}
	
	void _ResizeChemoSphere (float rad)
	{
		mysphere.BroadcastMessage ("_ResizeSphere", rad, SendMessageOptions.DontRequireReceiver);
	}
	
	void _SetChemoSphereAlpha (float alph)
	{
		mysphere.BroadcastMessage ("_SetAlpha", alph, SendMessageOptions.DontRequireReceiver);
	}
	
	void _SetReady(bool bReady) {
		if(bReady) {
			iMasterState = iStateReady;
		} else iMasterState = iStateNotReady;
	}
	
	public void _Initialize(float radius, float lifetime, Chemo_ chem) {
		fRadius = radius;
		fLifeTimeRemaining = fLifeTime = lifetime;
		
		_Chemo = new Chemo_(chem);
	}
	
	public void _Initialize(float radius, float lifetime, Chemo_ chem, Color c) {
		fRadius = radius;
		fLifeTimeRemaining = fLifeTime = lifetime;
		
		_Chemo = new Chemo_(chem);
		_Chemo.chemoColor = c;
	}
	
	// Use this for initialization
	void Start ()
	{
		//mysphere = gameObject.AddComponent<SphereCollider>();
		
		/*this starts as an empty gameobject
		 * needs to create a proper sphere renderer and collider,
		 * and initialize chemical properties for sensation
		 */ 
		
		//_Chemo.chemoColor = Color.red;
		
		//_InitializeChemoSphere ();
		
		//_SetRadius(fRadius);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (iMasterState) {
		case iStateNotReady:
			//Debug.Log ("chemobehavior not ready");
			break;
			
		case iStateReady:
			//Debug.Log ("chemobehavior ready");
			//_Chemo.chemoColor = Color.red;
		
			_InitializeChemoSphere ();
			
			iMasterState = iStateRunning;
			break;

		
		case iStateRunning:
			Decay (Time.deltaTime, true);
			break;
		}
	}
	
//	public void _SetRadius(float rad) {
//		mysphere.radius = rad;
//	}
}
