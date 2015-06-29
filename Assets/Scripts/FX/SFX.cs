using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {
    private AudioSource source;

    public AudioClip[] waterSplashes;

	void Awake () {
        source = this.GetComponent<AudioSource>();
        Ball.OnWaterHit += Ball_OnWaterHit;
    }

    void OnDestroy() {
        Ball.OnWaterHit -= Ball_OnWaterHit;
    }

    void Ball_OnWaterHit(Vector3 point) {
        PlayFX(waterSplashes[Random.Range(0, waterSplashes.Length)]);
    }

    void PlayFX(AudioClip clip) {
        source.clip = clip;
        source.Play();
    }
}
