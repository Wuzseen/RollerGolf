using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour {

	public float accelerationForce = 10.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		col.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up * accelerationForce);
	}
}
