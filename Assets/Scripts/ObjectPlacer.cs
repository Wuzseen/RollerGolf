using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ObjectPlacer : MonoBehaviour {
	public RectTransform shelf;

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
	}

	void OnDisable() {
		CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
		CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
	}



	void HandleOnPlacementEnd () {
		float tweenDist = -shelf.rect.height;
		shelf.DOAnchorPos(new Vector2(0f,tweenDist),.2f).SetRelative(true);
	}
	
	void HandleOnPlacementBegin () {
		float tweenDist = -shelf.rect.height;
		shelf.DOAnchorPos(new Vector2(0f,tweenDist),.2f).SetRelative(true);
	}

	void Awake () {
		shelf.anchoredPosition = shelf.anchoredPosition + new Vector2(0f,shelf.rect.height);
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < objects.Length; i++)
		{
			GameObject temp = (GameObject)Instantiate (guiAddObjectPrefab);
			temp.GetComponent<AddObjectGUIHandler>().setName(objects[i].name);
			temp.GetComponent<AddObjectGUIHandler>().setImage(objects[i].guiRepresenation);
			temp.GetComponent<AddObjectGUIHandler>().setGameObject(objects[i].prefab);
			temp.transform.SetParent(guiGridLayout.transform, false);
		}
	}
}
