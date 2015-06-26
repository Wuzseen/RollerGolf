using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {
	public float rotateSpeed = 15f;
	public float force = 30f;
	public Rigidbody2D ball;
	public Transform turretBase, turretEnd, spawnPoint;
	// Update is called once per frame
	void Update () {
		float horiz = Input.GetAxis("Horizontal") * -1f;
		turretBase.Rotate(Vector3.forward * rotateSpeed * horiz * Time.deltaTime);
	}

	void FixedUpdate () {
		if(Input.GetButtonDown("Jump")) {
			ball.velocity = Vector2.zero;
			ball.transform.position = spawnPoint.position;
			ball.AddForce(shotDirection * force);
		}
	}

	Vector2 shotDirection {
		get {
			return (turretEnd.position - turretBase.position).normalized;
		}
	}
}
