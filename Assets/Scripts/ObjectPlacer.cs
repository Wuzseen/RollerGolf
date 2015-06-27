using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ObjectPlacer : MonoBehaviour {
	public RectTransform shelf;
	public RectTransform endActionPhaseButton;

	[System.Serializable]
	class placableObject : System.Object {
		public string name;
		public GameObject prefab;
		public Sprite guiRepresenation;
	}

	[SerializeField]
	placableObject[] objects;

	public GameObject guiAddObjectPrefab;
	public GameObject guiGridLayout;

	void OnEnable() {
		CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnActionEnd += HandleOnActionEnd;
	}

	void OnDisable() {
		CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnActionEnd -= HandleOnActionEnd;
	}
	
	void HandleOnActionEnd () {
		TweenOut(endActionPhaseButton);
	}
	
	void HandleOnActionBegin () {
		TweenIn(endActionPhaseButton);
	}

	void TweenOut(RectTransform rt) {
		float tweenDist = -rt.rect.height;
		rt.DOAnchorPos(new Vector2(0f,tweenDist),.2f);
	}
	
	void TweenIn(RectTransform rt) {
		rt.DOAnchorPos(new Vector2(0f,0f),.2f);
	}

	void HandleOnPlacementEnd () {
		TweenOut(shelf);
	}
	
	void HandleOnPlacementBegin () {
		TweenIn(shelf);
	}

	void Awake () {
		shelf.anchoredPosition = shelf.anchoredPosition - new Vector2(0f,shelf.rect.height);
		endActionPhaseButton.anchoredPosition = endActionPhaseButton.anchoredPosition - new Vector2(0f,endActionPhaseButton.rect.height);
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < objects.Length; i++) {
			GameObject temp = (GameObject)Instantiate (guiAddObjectPrefab);
			temp.GetComponent<AddObjectGUIHandler>().setName(objects[i].name);
			temp.GetComponent<AddObjectGUIHandler>().setImage(objects[i].guiRepresenation);
			temp.GetComponent<AddObjectGUIHandler>().setGameObject(objects[i].prefab);
			temp.transform.SetParent(guiGridLayout.transform, false);
		}
	}
}
