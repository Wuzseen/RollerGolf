﻿using UnityEngine;
using System.Collections;

public class ActionCam : MonoBehaviour {
	
	tk2dCamera cam;
	public Transform target;
	public float followSpeed = 1.0f;
	
	public float minZoomSpeed = 20.0f;
	public float maxZoomSpeed = 40.0f;
	
	public float maxZoomFactor = 0.6f;
	
	void Awake() {
		cam = GetComponent<tk2dCamera>();
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnActionEnd += HandleOnActionEnd;
	}

	void OnDestroy() {
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnActionEnd -= HandleOnActionEnd;
	}

	void HandleOnActionEnd() {
		actionCam = false;
	}

	void HandleOnActionBegin () {
		if(target == null) {
			target = GameObject.FindGameObjectWithTag(GameConsts.TAG_BALL).transform;
		}
		StartCoroutine(ActionCamRoutine());
	}

	private bool actionCam;
	IEnumerator ActionCamRoutine() {
		actionCam = true;
		while(actionCam) {
			Vector3 start = transform.position;
			Vector3 end = Vector3.MoveTowards(start, target.position, followSpeed * Time.deltaTime);
			end.z = start.z;
			transform.position = end;
			
			Rigidbody rigidbody = target.GetComponent<Rigidbody>();
			if (rigidbody != null && cam != null) {
				float spd = rigidbody.velocity.magnitude;
				float scl = Mathf.Clamp01((spd - minZoomSpeed) / (maxZoomSpeed - minZoomSpeed));
				float targetZoomFactor = Mathf.Lerp(1, maxZoomFactor, scl);
				cam.ZoomFactor = Mathf.MoveTowards(cam.ZoomFactor, targetZoomFactor, 0.2f * Time.deltaTime);
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
