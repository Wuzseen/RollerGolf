using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreCard : MonoBehaviour {
	public List<ScoreColumn> scoreColumns;

	public Text courseField;

	private CourseData course;
	// Use this for initialization
	void Start () {
		course = Game.SelectedCourse;
		courseField.text = course.Name;
		SetUp();
	}

	void SetUp() {
		for(int i = 0; i < course.Holes.Count; i++) {
			print (course.Holes[i]);
			ScoreColumn column = scoreColumns[i];
			column.LoadHoleData(course.Holes[i],i + 1);
		}
	}

	public void UpdateScores() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
