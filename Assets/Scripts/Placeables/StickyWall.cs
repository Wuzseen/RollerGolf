using UnityEngine;
using System.Collections;

public class StickyWall : Placeable {

	[SerializeField]
	public float slowFactor = .25f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OnCollisionEnter2D(Collision2D col)
	{
		base.OnCollisionEnter2D(col);
		col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		col.gameObject.GetComponent<Rigidbody2D>().gravityScale = slowFactor;
	}

	void OnCollisionExit2D(Collision2D col)
	{
		col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
	}
}
