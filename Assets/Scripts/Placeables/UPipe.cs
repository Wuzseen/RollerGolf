using UnityEngine;
using System.Collections;

public class UPipe : MonoBehaviour {

	public Transform other;

	[SerializeField]
	private float slowDownFactor = 0.50f;

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
			col.gameObject.transform.position = other.position;

			Vector3 x = this.transform.up * col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * slowDownFactor;

			col.gameObject.GetComponent<Rigidbody2D>().velocity = x;
		}
	}

}
