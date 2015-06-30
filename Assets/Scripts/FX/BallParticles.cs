using UnityEngine;
using System.Collections;

public class BallParticles : MonoBehaviour {
    public ParticleSystem water, sand, confetti;
    public int waterCount = 30;
	public int confettiCount = 30;
    public int sandCount;

    void Awake() {
        Ball.OnWaterHit += Ball_OnWaterHit;
        Ball.OnSandHit += Ball_OnSandHit;
		Ball.OnHoleHit += HandleOnHoleHit;
	}

   void OnDestroy() {
		Ball.OnWaterHit -= Ball_OnWaterHit;
        Ball.OnSandHit -= Ball_OnSandHit;
		Ball.OnHoleHit -= HandleOnHoleHit;
	}

   private void Ball_OnSandHit(Vector3 point)
   {
       PositionAndEmit(point, sand, sandCount);
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
