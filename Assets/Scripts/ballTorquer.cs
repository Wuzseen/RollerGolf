using UnityEngine;
using System.Collections;

public class ballTorquer : MonoBehaviour {

	float maxSpeedY = 10.0f;
	
	Vector2 worldVelocity = Vector2.right * 2.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		var dir = GetComponent<Rigidbody2D>().velocity;
		dir += worldVelocity;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		GetComponent<Rigidbody2D>().MoveRotation(angle);
		
		if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) >= maxSpeedY){
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxSpeedY);
		}       
	}
}
