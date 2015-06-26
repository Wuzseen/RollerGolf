using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(GameConsts.TAG_BALL)) {

		}
	}
}
