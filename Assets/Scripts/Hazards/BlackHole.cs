using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlackHole : MonoBehaviour {
	public float power = 5f;

	private CircleCollider2D collider;
	private float maxRadius;


	// Use this for initialization
	void Start () {
		this.collider = GetComponent<CircleCollider2D>();
		this.maxRadius = this.collider.radius;
	}

	void OnTriggerStay2D(Collider2D other) {
		Rigidbody2D rgd = other.GetComponent<Rigidbody2D>();
		if(rgd != null) {
			float dist = Vector2.Distance(rgd.position,this.transform.position);

			Vector2 direction = (Vector2)((Vector2)this.transform.position - rgd.position);
			rgd.AddForce(direction * power * Time.deltaTime);
//			rgd.AddForce(direction * power * powerCurve.Evaluate(Mathf.Clamp(1 - (dist / maxRadius),0f,1f)));
		}
	}

//	IEnumerator RotateRoutine(Rigidbody2D other) {
//		float radius = Vector2.Distance(other.position,transform.position);
//		// c^2 = 
//		float dist = Mathf.Sqrt(2 * radius * radius); 
//		Vector2 direction = other.position - this.transform.position;
//		float speed = other.velocity.magnitude;
//		PathType pt = PathType.CatmullRom;
//		Vector3[] points = new Vector3[3];
//		points[0] = other.transform.position;
//		points[2] = -direction.normalized * radius + transform.position;
//	}
}
