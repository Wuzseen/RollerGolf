using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public delegate void BallCollisionEvent(Vector3 point);
    public static event BallCollisionEvent OnWaterHit, OnSandHit, OnGroundImpact, OnHoleHit;

	public float gravityScale = 1f;

    void OnEnable()
    {
		this.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
        CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
    }

    void OnDisable()
    {
        CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
        CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
    }

    void HandleOnPlacementBegin()
    {
        this.GetComponent<Collider2D>().enabled = false;
    }

    void HandleOnPlacementEnd()
    {
        this.GetComponent<Collider2D>().enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer(GameConsts.LAYER_GROUND))
        {
            Debug.Log("Yo dawg, this layer collision stuff works.");
            Raise(OnGroundImpact, collision.contacts[0].point);
        }

        //Raise (OnGroundImpact, collision.contacts[0].point);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameConsts.TAG_WATER)) {
            Raise(OnWaterHit, this.transform.position);
        } else if (collision.CompareTag(GameConsts.TAG_SAND)) {
            Raise(OnSandHit, this.transform.position);
        } else if (collision.CompareTag(GameConsts.TAG_HOLE)) {
            print("TAG");
            Raise(OnHoleHit, this.transform.position);
        }
    }

    void Raise(BallCollisionEvent anEvent, Vector3 point) {
        if (anEvent != null) {
            anEvent(point);
        }
    }
}
