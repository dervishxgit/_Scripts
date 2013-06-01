using UnityEngine;
using System.Collections;

public class SwarmSpawnBehavior : MonoBehaviour {
	
	public GameObject wasprootprefab;
	
	public int numWaspsToSpawn = 50, numSpawned = 0;
	public float intervalSpawn = 1.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (numSpawned < numWaspsToSpawn) {
			StartCoroutine(_SpawnWasp() );
		}
	}
	
	IEnumerator _SpawnWasp() {
		GameObject.Instantiate(wasprootprefab, transform.root.position, transform.root.rotation);
		numSpawned++;
		yield return null;
	}
}
