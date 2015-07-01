using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BlackHole : MonoBehaviour {
	public float power = 5f;

	private Color c1 = Color.blue;
	private Color c2 = Color.red;

	private CircleCollider2D collider;
	private float maxRadius;

	private SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
		this.collider = GetComponent<CircleCollider2D>();
		this.maxRadius = this.collider.radius;
		this.renderer = GetComponent<SpriteRenderer>();
		c1 = Color.Lerp(Color.cyan,Color.green,Random.Range(0f,1f));
		c2 = Color.Lerp(Color.red,Color.yellow,Random.Range(0f,1f));
	}

	void OnTriggerStay2D(Collider2D other) {
		Rigidbody2D rgd = other.GetComponent<Rigidbody2D>();
		if(rgd != null) {
			float dist = Vector2.Distance(rgd.position,this.transform.position);

			Vector2 direction = (Vector2)((Vector2)this.transform.position - rgd.position);
			rgd.AddForce(direction * power * Time.deltaTime);
		}
	}

	void Update() {

		this.renderer.color = Color.Lerp(c1,c2,Mathf.Sin(Time.time));
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
