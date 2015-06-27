using UnityEngine;
using System.Collections;

public class sPipe : MonoBehaviour {

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
		col.gameObject.transform.position = other.position;
		col.gameObject.GetComponent<Rigidbody2D>().velocity *= slowDownFactor;
	}
}
