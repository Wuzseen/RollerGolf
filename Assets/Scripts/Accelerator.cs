using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Accelerator : MonoBehaviour {

	[SerializeField]
	public int direction = 1;
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
		col.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.right * accelerationForce);
	}
}
