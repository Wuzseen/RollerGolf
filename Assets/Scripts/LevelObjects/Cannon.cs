using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Cannon : MonoBehaviour {
	public float rotateSpeed = 15f;
	public float force = 30f;
	public Rigidbody2D ball;
	public Transform turretBase, turretEnd, spawnPoint;
	public ParticleSystem particles;

	public AudioClip[] cannonShots;

	void Start() {
		ResetBall();
	}

	void OnEnable() {
		CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
		CourseHandler.OnActionBegin += ShootCannon;
		CourseHandler.OnActionEnd += ResetBall;
		CourseHandler.OnHoleBegin += ResetBall;
	}
	
	void OnDisable() {
		CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
		CourseHandler.OnActionBegin -= ShootCannon;
		CourseHandler.OnActionEnd -= ResetBall;
		CourseHandler.OnHoleBegin -= ResetBall;
	}

	bool placing;
	
	void ResetBall () {
		ball.transform.parent = spawnPoint.transform;
		ball.transform.localPosition = Vector3.zero + new Vector3(0,0,1);
		ball.velocity = Vector2.zero;
		ball.isKinematic = true;
	}

	void HandleOnPlacementBegin () {
		StartCoroutine(CannonRoutine());
	}

	void HandleOnPlacementEnd () {
		placing = false;
	}

	IEnumerator CannonRoutine() {
		placing = true;
		while(placing) {
			float horiz = Input.GetAxis("CannonAdjust") * -1f;
			turretBase.Rotate(Vector3.forward * rotateSpeed * horiz * Time.deltaTime);
			yield return null;
		}
	}

	void ShootCannon () {
		StartCoroutine(CannonShotRoutine());
	}

	IEnumerator CannonShotRoutine () {
		turretBase.DOScaleX(.6f,.2f);
		yield return new WaitForSeconds(.2f);
		turretBase.DOScaleX(1f,.2f);
		yield return new WaitForSeconds(.2f);
		ball.transform.localPosition = new Vector3(0,0,-1);
		ball.transform.parent = null;
		ball.isKinematic = false;
		ball.velocity = Vector2.zero;
        ball.transform.localScale = Vector3.one;
		ball.transform.position = spawnPoint.position;
		ball.AddForce(shotDirection * force);
		Camera.main.transform.DOShakePosition(.2f,2f);
		particles.Emit (30);
		SoundManager.PlaySFX(cannonShots[Random.Range(0,cannonShots.Length)]);
	}

	Vector2 shotDirection {
		get {
			return (turretEnd.position - turretBase.position).normalized;
		}
	}
}
