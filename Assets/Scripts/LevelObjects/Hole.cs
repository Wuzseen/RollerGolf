using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
	public delegate void HoleEvent();
	public static event HoleEvent OnObjectiveReached;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag(GameConsts.TAG_BALL)) {
			other.transform.parent = this.transform;
			other.transform.localPosition = Vector3.zero;
			if(OnObjectiveReached != null) {
				OnObjectiveReached();
			}
		}
	}
}
