using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreCard : MonoBehaviour {
	public List<ScoreColumn> scoreColumns;

	public Text courseField;

	private CourseData course;

	private int activeHole = -1; // will get ++'ed to 0 when the  first hole begins

	private List<HoleData> holes {
		get {
			return course.Holes;
		}
	}

	void Awake() {
		CourseHandler.OnHoleBegin += HandleOnHoleBegin;
		CourseHandler.OnHoleEnd += UpdateScores;
	}

	void OnDestroy() {
		CourseHandler.OnHoleBegin -= HandleOnHoleBegin;
		CourseHandler.OnHoleEnd -= UpdateScores;
	}
	
	void HandleOnHoleBegin () {
		activeHole++;
	}

	// Use this for initialization
	void Start () {
		course = Game.SelectedCourse;
		courseField.text = course.Name;
		SetUp();
	}

	void SetUp() {
		for(int i = 0; i < holes.Count; i++) {
			ScoreColumn column = scoreColumns[i];
			column.LoadHoleData(holes[i],i + 1);
		}
	}

	public void UpdateScores() {
		int holeScore = HoleScorer.HoleScore;
		scoreColumns[activeHole].SetScore(holeScore);
	}
}
