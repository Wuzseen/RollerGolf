using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () {
	
	}
}
