using UnityEngine;
using System.Collections;

public class Object_Spawner : MonoBehaviour {

	public GameObject SpikesPrefab;

	// Use this for initialization
	void Start () {
		Instantiate (SpikesPrefab, new Vector3 (0, 0, 5), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
