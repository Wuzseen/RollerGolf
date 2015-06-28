using UnityEngine;
using System.Collections;

public class AntiGravity : MonoBehaviour {

	public float antiGravForce = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "ball")
		{
			col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up * antiGravForce, ForceMode2D.Impulse);
		}
	}

}
