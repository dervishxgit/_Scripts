using UnityEngine;
using System.Collections;

public class ChemoSphere_Behavoir : MonoBehaviour {
	
	public SphereCollider sphereCollider;
	
	public void _SetRadius(float rad) {
		sphereCollider.radius = rad;
	}
	
	public void _SetScale(float sca) {
		//uniform scale
		Vector3 newscale = new Vector3(sca, sca, sca);
		gameObject.transform.localScale = newscale;
	}
	
	public void _ResizeSphere(float scale) {
		_SetScale(scale);
		//_SetRadius(radius);
	}
	
	public void _SetAlpha(float alph) {
		Color tempColor = gameObject.renderer.material.color;
		tempColor.a = Mathf.Clamp(alph, 0.0f, 1.0f);
		
		gameObject.renderer.material.color = tempColor;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
