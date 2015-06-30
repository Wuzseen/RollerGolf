using UnityEngine;
using System.Collections;

public class BallSFX : MonoBehaviour {
    public AudioClip[] waterSplashes;

    public AudioClip[] bumps;

	void Awake () {
        Ball.OnWaterHit += Ball_OnWaterHit;
        Ball.OnSandHit += Ball_OnSandHit;
        Ball.OnGroundImpact += Ball_OnGroundImpact;
    }

    void OnDestroy() {
        Ball.OnWaterHit -= Ball_OnWaterHit;
        Ball.OnSandHit -= Ball_OnSandHit;
        Ball.OnGroundImpact -= Ball_OnGroundImpact;
	}
	
	void Ball_OnGroundImpact(Vector3 point) {
		print ("BUMP");
		PlayFX(bumps);
	}

    void Ball_OnWaterHit(Vector3 point) {
        PlayFX(waterSplashes);
    }

    private void Ball_OnSandHit(Vector3 point)
    {
        throw new System.NotImplementedException();
    }

    void PlayFX(AudioClip[] collection) {
        PlayFX(collection[Random.Range(0, collection.Length)]);
    }

    void PlayFX(AudioClip clip) {
		SoundManager.PlaySFX(clip);
    }
}
