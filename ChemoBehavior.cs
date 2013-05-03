using UnityEngine;
using System.Collections;

public class ChemoBehavior : MonoBehaviour {
	
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
	 */ 
	float fRadius = 10.0f, fLifeTime = 10.0f, fLifeTimeRemaining = 10.0f;
	
	public GameObject chemoSpherePrefab;
	GameObject mysphere;
	Material sphereMaterial;
	
	Wasp_Core._Chemo_ _Chemo = new Wasp_Core._Chemo_();
	
	ChemoBehavior() {
		
	}
	
	ChemoBehavior(float radius, float lifetime, Wasp_Core._Chemo_ chem) {
		fRadius = radius;
		fLifeTimeRemaining = fLifeTime = lifetime;
		
		_Chemo = chem;
	}
	
	void Decay(float fAmount, bool bDestroyWhenFinished) {
		fLifeTimeRemaining -= fAmount;
		if(bDestroyWhenFinished) {
			if(fLifeTimeRemaining <= 0.0f) {
				Destroy(gameObject);
			}	
		}
		
		//dynamic resize
		_ResizeChemoSphere(fLifeTimeRemaining);
	}
	
	void _InitializeChemoSphere() {
		//spawn and map
		mysphere = Instantiate(chemoSpherePrefab, transform.position, transform.rotation) as GameObject;
		
		mysphere.transform.root.parent = gameObject.transform;
		
		//mysphere.BroadcastMessage("_ResizeSphere", fRadius, SendMessageOptions.DontRequireReceiver);
		_ResizeChemoSphere(fRadius);
		
		sphereMaterial = mysphere.GetComponent<Renderer>().material;
		
		sphereMaterial.color = _Chemo.chemoColor;
		
	}
	
	void _ResizeChemoSphere(float rad) {
		mysphere.SendMessage("_ResizeSphere", rad, SendMessageOptions.DontRequireReceiver);
	}
	
	// Use this for initialization
	void Start () {
		//mysphere = gameObject.AddComponent<SphereCollider>();
		
		/*this starts as an empty gameobject
		 * needs to create a proper sphere renderer and collider,
		 * and initialize chemical properties for sensation
		 */ 
		
		_InitializeChemoSphere();
		
		//_SetRadius(fRadius);
	}
	
	// Update is called once per frame
	void Update () {
		Decay(Time.deltaTime, true);
	}
	
//	public void _SetRadius(float rad) {
//		mysphere.radius = rad;
//	}
}
