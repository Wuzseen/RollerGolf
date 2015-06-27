using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseLoader : MonoBehaviour {
	private List<CourseData> courses;
	public List<CourseData> Courses {
		get {
			return this.courses;
		}
	}

	// Use this for initialization
	void Awake () {
		LoadAllCourses();
	}

	void LoadAllCourses() {
		if(courses != null) {
			return;
		}
		courses = new List<CourseData>();

		TextAsset[] courseFiles = Resources.LoadAll<TextAsset>(GameConsts.COURSE_DIRECTORY);
		for(int i = 0; i < courseFiles.Length; i++) {
			courses.Add(JSONSerializer.Deserialize(typeof(CourseData),courseFiles[i].text) as CourseData);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
