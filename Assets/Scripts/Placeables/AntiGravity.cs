using UnityEngine;
using System.Collections;

public class AntiGravity : Placeable {

	public float antiGravForce = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D (col);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == GameConsts.TAG_BALL)
		{
			base.OnCollisionEnter2D(null);
			col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up * antiGravForce, ForceMode2D.Impulse);
		}
	}

}
