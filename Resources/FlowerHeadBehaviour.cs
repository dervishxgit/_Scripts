using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowerHeadBehaviour : MonoBehaviour {
	/*
	 * Flower head will orient to sun direction
	 * 
	 * will perform simple open/close animation depending on bloom
	 */ 
	
	
	public List<GameObject> _lAllPetals = new List<GameObject>();
	
	public Animation anim;
	
	public AnimationClip animPetalsOpen,
					 animPetalsClose;
	
	public int statePetals = 0;
	public const int statePetalsClosed = 0;
	public const int statePetalsOpen = 1;
	
	public GameObject sun;
	bool bDidFace = false;
	
	void Awake() {
		anim = gameObject.GetComponent<Animation>();
		sun = GameObject.FindGameObjectWithTag("Sun");
	}
	
	// Use this for initialization
	void Start () {
		animPetalsOpen = anim.GetClip("flowerHeadPetalsOpen");
		animPetalsClose = anim.GetClip("flowerHeadPetalsClose");
		
		anim.Play("flowerHeadPetalsClose");
		
		statePetals = statePetalsClosed;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	public void _PetalsOpen() {
		if(statePetals != statePetalsOpen) {
			anim.Play("flowerHeadPetalsOpen");
			statePetals = statePetalsOpen;
		}
		
	}
	
	public void _PetalsClose() {
		if(statePetals != statePetalsClosed) {
			anim.Play("flowerHeadPetalsClose");
			statePetals = statePetalsClosed;
		}
		
	}
	
	
	public void _FaceSun() {
		if(!bDidFace) {
			Debug.Log("tried to face sun");
			StartCoroutine(_FaceSun_CO());
		}
	}
	
	public IEnumerator _FaceSun_CO() {
		bDidFace = true;
		if( sun != null) {
			Vector3 vecSun = sun.transform.position - gameObject.transform.position;
			Datacore._SeekTarget3D(gameObject.transform, vecSun,
				0.0f, true);
		}
		yield return new WaitForSeconds(10.0f);
		bDidFace = false;
	}
	
}
