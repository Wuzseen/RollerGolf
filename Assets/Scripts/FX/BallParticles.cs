using UnityEngine;
using System.Collections;

public class BallParticles : MonoBehaviour {
    public ParticleSystem water, confetti;
    public int waterCount = 30;
	public int confettiCount = 30;

    void Awake() {
        Ball.OnWaterHit += Ball_OnWaterHit;
		Ball.OnHoleHit += HandleOnHoleHit;
	}

	void OnDestroy() {
		Ball.OnWaterHit -= Ball_OnWaterHit;
		Ball.OnHoleHit -= HandleOnHoleHit;
	}

    void HandleOnHoleHit (Vector3 point) {
		PositionAndEmit(point, confetti, confettiCount);
    }

    void Ball_OnWaterHit(Vector3 point) {
        PositionAndEmit(point, water, waterCount);
    }

    void PositionAndEmit(Vector3 point, ParticleSystem system, int count) {
        system.transform.position = point;
        system.Emit(count);
    }

}
