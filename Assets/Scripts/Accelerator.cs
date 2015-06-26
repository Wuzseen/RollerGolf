using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour {

	[SerializeField]
	public Vector2 direction = new Vector2(1.0f,0.0f);
	[SerializeField]
	public float accelerationForce = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * accelerationForce);
	}
}
