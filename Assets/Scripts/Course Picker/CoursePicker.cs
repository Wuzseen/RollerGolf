using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CoursePicker : MonoBehaviour {
	public RectTransform contentRoot;
	public GameObject courseButtonPrefab;

	private List<CourseButton> buttons;
	// Use this for initialization
	void Start () {
		buttons = new List<CourseButton>();
		LoadButtons();
	}

	CourseButton NewButton() {
		GameObject buttonObj = Instantiate(courseButtonPrefab) as GameObject;
		buttonObj.transform.SetParent(contentRoot);
		CourseButton ret = buttonObj.GetComponent<CourseButton>();
		buttons.Add(ret);
		return ret;
	}

	void LoadButtons() {
		List<CourseData> cd = CourseLoader.Instance.Courses;
		for(int i = 0; i < cd.Count; i++) {
			CourseData c = cd[i];
			CourseButton button = NewButton();
			button.SetFromData(c);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
