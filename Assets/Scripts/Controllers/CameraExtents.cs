using UnityEngine;
using System.Collections;

public class CameraExtents : MonoBehaviour {
	public Transform Left, Right, Top, Bottom;

	private static CameraExtents instance;
	public static CameraExtents Instance {
		get {
			return instance;
		}
	}

	void Awake() {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		Vector2 camPosition = Camera.main.transform.position;
		Left.position = new Vector2(Left.position.x, camPosition.y);
		Right.position = new Vector2(Right.position.x, camPosition.y);
		Top.position = new Vector2(camPosition.x, Top.position.y);
		Bottom.position = new Vector2(camPosition.x, Bottom.position.y);
	}

	private Vector3 TransVisible(Transform aPoint) {
		Vector3 vp = Camera.main.WorldToViewportPoint(aPoint.position);
		return vp;
	}

	public bool RestrictLeft() {
		Vector3 vp = TransVisible(Left);
		return !(vp.x <= 0f);
	}

	public bool RestrictRight() {
		Vector3 vp = TransVisible(Right);
		return !(vp.x >= 1f);
	}

	public bool RestrictUp() {
		Vector3 vp = TransVisible(Top);
		return !(vp.y >= 1f);
	}

	public bool RestrictDown() {
		Vector3 vp = TransVisible(Bottom);
		if(vp.y <= 0f) {
			print (vp);
		}
		return !(vp.y <= 0f);
	}
}
