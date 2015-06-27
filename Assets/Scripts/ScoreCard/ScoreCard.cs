using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreCard : MonoBehaviour {
	public List<ScoreColumn> scoreColumns;

	private CourseData course;
	// Use this for initialization
	void Start () {
		course = Game.SelectedCourse;
		SetUp();
	}

	void SetUp() {
		for(int i = 0; i < course.Holes.Count; i++) {
			print (course.Holes[i]);
			ScoreColumn column = scoreColumns[i];
			column.LoadHoleData(course.Holes[i],i + 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
