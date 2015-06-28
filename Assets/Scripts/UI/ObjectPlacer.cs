	using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ObjectPlacer : MonoBehaviour {
	public Button confirmButton;
	public RectTransform shelf;
	public RectTransform endActionPhaseButton;

	public delegate void GameUIEvent();
	public static event GameUIEvent OnConfirm, OnRetry;

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
	
	private static ObjectPlacer instance;
	void Awake () {
		if(instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		shelf.anchoredPosition = shelf.anchoredPosition - new Vector2(0f,shelf.rect.height);
		endActionPhaseButton.anchoredPosition = endActionPhaseButton.anchoredPosition - new Vector2(0f,endActionPhaseButton.rect.height);
	}
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < objects.Length; i++) {
			GameObject temp = (GameObject)Instantiate (guiAddObjectPrefab);
			AddObjectGUIHandler guiHandle = temp.GetComponent<AddObjectGUIHandler>();
			guiHandle.setName(objects[i].name);
			guiHandle.setImage(objects[i].guiRepresenation);
			guiHandle.setGameObject(objects[i].prefab);
			temp.transform.SetParent(guiGridLayout.transform, false);
		}
	}
	
	void HandleOnRetirePressed () {
		instance = null;
		Destroy(this.gameObject);
	}

	void OnEnable() {
		CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnActionEnd += HandleOnActionEnd;
		ScoreCard.OnRetirePressed += HandleOnRetirePressed;
	}

	void OnDisable() {
		CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnActionEnd -= HandleOnActionEnd;
		ScoreCard.OnRetirePressed -= HandleOnRetirePressed;
	}

	void Raise(GameUIEvent anEvent) {
		if(anEvent != null) {
			anEvent();
		}
	}

	void Update() {
		confirmButton.interactable = (ObjectPlacement.CurrentPlaceable == null);
	}

	public void Retry() {
		Raise (OnRetry);
	}

	public void Confirm() {
		if(ObjectPlacement.CurrentPlaceable != null) {
			return;
		}
		Raise (OnConfirm);
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
}
