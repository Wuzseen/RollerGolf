using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {
    private AudioSource source;

    public AudioClip[] waterSplashes;

    public AudioClip[] bumps;

	void Awake () {
        source = this.GetComponent<AudioSource>();
        Ball.OnWaterHit += Ball_OnWaterHit;
        Ball.OnGroundImpact += Ball_OnGroundImpact;
    }

    void Ball_OnGroundImpact(Vector3 point) {
        PlayFX(bumps);
    }

    void OnDestroy() {
        Ball.OnWaterHit -= Ball_OnWaterHit;
        Ball.OnGroundImpact -= Ball_OnGroundImpact;
    }

    void Ball_OnWaterHit(Vector3 point) {
        PlayFX(waterSplashes);
    }

    void PlayFX(AudioClip[] collection) {
        PlayFX(collection[Random.Range(0, collection.Length)]);
    }

    void PlayFX(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
