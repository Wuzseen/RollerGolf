using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Accelerator : Placeable {

	public float accelerationForce = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		base.OnCollisionEnter2D(col);
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
        float currMag = rb.velocity.magnitude;
        rb.velocity = Vector3.zero;
		rb.AddForce(this.transform.right * (accelerationForce + currMag), ForceMode2D.Impulse);
	}
}
