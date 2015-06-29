using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public delegate void BallCollisionEvent(Vector3 point);
    public static event BallCollisionEvent OnWaterHit, OnGroundImpact;

    public void OnCollisionEnter2D(Collision2D collision) {
        Raise(OnGroundImpact, collision.contacts[0].point);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameConsts.TAG_WATER)) {
            Raise(OnWaterHit, this.transform.position);
        }
    }

    void Raise(BallCollisionEvent anEvent, Vector3 point) {
        if (anEvent != null) {
            anEvent(point);
        }
    }
}
