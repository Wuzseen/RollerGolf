using UnityEngine;
using System.Collections;

public class sPipe : Placeable {

	public Transform other;
	
	[SerializeField]
	private float launchVelocity = 8.0f;
	
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

            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            rb.AddForce(other.transform.up * launchVelocity, ForceMode2D.Impulse);
		}
	}
}
