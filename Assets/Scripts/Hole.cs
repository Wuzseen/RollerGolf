using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	public delegate void HoleEvent();
	public static event HoleEvent OnObjectiveReached;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(GameConsts.TAG_BALL)) {
			print ("Ball hit hole");
			if(OnObjectiveReached != null) {
				OnObjectiveReached();
			}
		}
	}
}
