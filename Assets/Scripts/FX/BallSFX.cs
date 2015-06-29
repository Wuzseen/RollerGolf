using UnityEngine;
using System.Collections;

public class BallSFX : MonoBehaviour {
    private AudioSource source;

    public AudioClip[] waterSplashes;

    public AudioClip[] bumps;

	void Awake () {
        source = this.GetComponent<AudioSource>();
        Ball.OnWaterHit += Ball_OnWaterHit;
        Ball.OnGroundImpact += Ball_OnGroundImpact;
    }

    void OnDestroy() {
        Ball.OnWaterHit -= Ball_OnWaterHit;
        Ball.OnGroundImpact -= Ball_OnGroundImpact;
	}
	
	void Ball_OnGroundImpact(Vector3 point) {
		print ("BUMP");
		PlayFX(bumps);
	}

    void Ball_OnWaterHit(Vector3 point) {
        PlayFX(waterSplashes);
    }

    void PlayFX(AudioClip[] collection) {
        PlayFX(collection[Random.Range(0, collection.Length)]);
    }

    void PlayFX(AudioClip clip) {
		SoundManager.PlaySFX(clip);
//        source.clip = clip;
//        source.Play();
    }
}
