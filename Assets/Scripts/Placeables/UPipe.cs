using UnityEngine;
using System.Collections;

public class UPipe : Placeable {

	public Transform other;

	[SerializeField]
	private float slowDownFactor = 0.50f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnCollisionEnter2D (Collision2D col)
	{
		base.OnCollisionEnter2D (col);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == GameConsts.TAG_BALL)
		{
			base.OnCollisionEnter2D(null);
			col.gameObject.transform.position = other.position;

			Vector3 x = this.transform.up * col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * slowDownFactor;

			col.gameObject.GetComponent<Rigidbody2D>().velocity = x;
		}
	}

}
