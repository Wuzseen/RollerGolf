using UnityEngine;
using System.Collections;

public abstract class Placeable : MonoBehaviour {
	public AudioClip collideClip;

	protected virtual void OnCollisionEnter2D(Collision2D col) {
		SoundManager.PlaySFX(collideClip);
	}
}
