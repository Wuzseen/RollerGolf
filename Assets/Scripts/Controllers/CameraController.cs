using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraController : MonoBehaviour {
    public static float RESET_TIME = 2f;

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
		ResetCamera(false);
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

    void ResetCamera(bool firstTime) {
        StartCoroutine(CamReset(firstTime));
    }

	void ResetCamera() {
        ResetCamera(true);
	}

	private bool resettingCamera;
	IEnumerator CamReset(bool firstTime) {
		if(resettingCamera) {
			yield break;
		}
        if (firstTime) {
            GameObject hole = GameObject.FindGameObjectWithTag(GameConsts.TAG_HOLE);
            Vector3 targetP = hole.transform.position;
            targetP.z = Camera.main.transform.position.z;
            Camera.main.transform.position = targetP;
            yield return new WaitForSeconds(1f);
        }
		resettingCamera = true;
		Vector3 targetPosition = Tee.position;
		targetPosition.z = Camera.main.transform.position.z;
		float transitionTime = RESET_TIME - 1f;
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
			if(Camera.main == null) {
				yield return null;
				continue;
			}
			float vert = Input.GetAxis("Vertical");
			CameraExtents ce = CameraExtents.Instance;
			if(vert > 0f && ce.RestrictUp()) {
				print ("UpRestrict");
				vert = 0f;
			} else if(vert < 0f && ce.RestrictDown()) {
				print ("DownRestrict");
				vert = 0f;
			}
			float horiz = Input.GetAxis("Horizontal");
			if(horiz > 0 && ce.RestrictRight()) {
				horiz = 0f;
			} else if(horiz < 0 && ce.RestrictLeft()) {
				horiz = 0f;
			}
			Vector3 positionOffset = Vector3.zero;
			positionOffset.x = horiz * Time.deltaTime * baseCamMoveSpeed + horiz * Time.deltaTime * extraCamMoveSpeed * moveSpeedT;
			positionOffset.y = vert * Time.deltaTime * baseCamMoveSpeed + vert * Time.deltaTime * extraCamMoveSpeed * moveSpeedT;
			Camera.main.transform.position += positionOffset;

			float zoom = Input.GetAxis("Zoom");
			if(ce.RestrictDown() || ce.RestrictUp() || ce.RestrictRight() || ce.RestrictLeft()) {
				zoom = Mathf.Clamp(zoom,0f,1f);
			}
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
			Vector3 direction = end-start;
			if(direction.x > 0 && CameraExtents.Instance.RestrictRight()) {
				end.x = start.x;
			} else if(direction.x < 0 && CameraExtents.Instance.RestrictLeft()) {
				end.x = start.x;
			}
			if(direction.y < 0 && CameraExtents.Instance.RestrictDown()) {
				end.y = start.y;
			} else if(direction.y > 0 && CameraExtents.Instance.RestrictUp()) {
				end.y = start.y;
			}
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
