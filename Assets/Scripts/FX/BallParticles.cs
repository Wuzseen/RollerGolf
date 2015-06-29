using UnityEngine;
using System.Collections;

public class BallParticles : MonoBehaviour {
    public ParticleSystem water;
    public int waterCount = 30;

    void Awake() {
        Ball.OnWaterHit += Ball_OnWaterHit;
    }

    void Ball_OnWaterHit(Vector3 point) {
        PositionAndEmit(point, water, waterCount);
    }

    void PositionAndEmit(Vector3 point, ParticleSystem system, int count) {
        system.transform.position = point;
        system.Emit(count);
    }

    void OnDestroy() {
        Ball.OnWaterHit -= Ball_OnWaterHit;
    }

}
