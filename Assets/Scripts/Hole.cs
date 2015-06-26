using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	public delegate void HoleEvent();
	public static event HoleEvent OnObjectiveReached;
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(GameConsts.TAG_BALL)) {
			if(OnObjectiveReached != null) {
				OnObjectiveReached();
			}
		}
	}
}
