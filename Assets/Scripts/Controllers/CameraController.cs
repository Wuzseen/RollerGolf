using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraController : MonoBehaviour {
	public float baseCamMoveSpeed = 10f;

	public float extraCamMoveSpeed = 20f;

	public float zoomSpeed = 10f;


	tk2dCamera cam;
	public Transform target;
	public float followSpeed = 1.0f;
	
	public float minZoomSpeed = 20.0f;
	public float maxZoomSpeed = 40.0f;
	
	public float maxZoomFactor = 0.6f;
	public float minZoomFactor = 1.2f;

	private static CameraController instance;
	void Awake() {
		if(instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		cam = GetComponent<tk2dCamera>();
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnActionEnd += HandleOnActionEnd;
		CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
		CourseHandler.OnHoleBegin += ResetCamera;
	}

	void OnDestroy() {
		if(instance == this) {
			CourseHandler.OnActionBegin -= HandleOnActionBegin;
			CourseHandler.OnActionEnd -= HandleOnActionEnd;
			CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
			CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
			CourseHandler.OnHoleBegin -= ResetCamera;
		}
	}
	
	void HandleOnPlacementEnd () {
		placementCam = false;
	}
	
	void HandleOnPlacementBegin () {
		ResetCamera();
		StartCoroutine(PlacementRoutine());
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

	void ResetCamera() {
		StartCoroutine(CamReset());
	}

	private bool resettingCamera;
	IEnumerator CamReset() {
		if(resettingCamera) {
			yield break;
		}
		resettingCamera = true;
		Vector3 targetPosition = Tee.position;
		targetPosition.z = Camera.main.transform.position.z;
		float transitionTime = 1f;
		Camera.main.transform.DOMove(targetPosition, transitionTime);
		float t = 0f;
		while(t < transitionTime) {
			t += Time.deltaTime;
			yield return null;
		} 
		resettingCamera = false;
	}

	private Transform tee;
	Transform Tee {
		get {
			if(tee == null) {
				tee = GameObject.Find ("Cannon").transform;
			}
			return tee;
		}
	}

	float moveSpeedT {
		get {
			return 1f - ((cam.ZoomFactor - maxZoomFactor) / (minZoomFactor - maxZoomFactor)) ;
		}
	}

	private bool placementCam;
	IEnumerator PlacementRoutine() {
		placementCam = true;
		while(resettingCamera) {
			yield return null;
		}
		while(placementCam) {
			float vert = Input.GetAxis("Vertical");
			float horiz = Input.GetAxis("Horizontal");
			Vector3 positionOffset = Vector3.zero;
			positionOffset.x = horiz * Time.deltaTime * baseCamMoveSpeed + horiz * Time.deltaTime * extraCamMoveSpeed * moveSpeedT;
			positionOffset.y = vert * Time.deltaTime * baseCamMoveSpeed + vert * Time.deltaTime * extraCamMoveSpeed * moveSpeedT;
			Camera.main.transform.position += positionOffset;

			float zoom = Input.GetAxis("Zoom");
			cam.ZoomFactor = Mathf.Clamp(cam.ZoomFactor + zoom * Time.deltaTime * zoomSpeed,maxZoomFactor,minZoomFactor);

			yield return null; // placement camera controls
		}
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
