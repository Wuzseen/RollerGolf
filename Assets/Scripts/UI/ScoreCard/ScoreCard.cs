using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreCard : MonoBehaviour {
	public delegate void ScoreCardEvent();
	public static event ScoreCardEvent OnRetirePressed;
	public List<ScoreColumn> scoreColumns;

	public ScoreColumn totalColumn;

	public Text courseField;
	public Text totalScoreField;

	private CourseData course;

	private int activeHole = -1; // will get ++'ed to 0 when the  first hole begins

	private List<HoleData> holes {
		get {
			return course.Holes;
		}
	}

	void Awake() {
		CourseHandler.OnHoleBegin += HandleOnHoleBegin;
		HoleScorer.OnScoreChanged += HandleOnScoreChanged;
	}

	void OnDestroy() {
		CourseHandler.OnHoleBegin -= HandleOnHoleBegin;
		HoleScorer.OnScoreChanged -= HandleOnScoreChanged;
	}
	
	void HandleOnHoleBegin () {
		activeHole++;
	}
	
	void HandleOnScoreChanged (CourseScore scoreObject) {
		scoreColumns[activeHole].SetScore(scoreObject.CurrentHoleScore.Score);
		totalColumn.SetScore(scoreObject.TotalScore,scoreObject.CoursePar);
		string scoreString = scoreObject.PlusMinusScore > 0 ? string.Format("+{0:D}",scoreObject.PlusMinusScore) : scoreObject.PlusMinusScore.ToString();
		totalScoreField.text = string.Format("Total Score: {0}",scoreString);
	}

	// Use this for initialization
	void Start () {
		course = Game.SelectedCourse;
		courseField.text = course.Name;
		SetUp();
	}

	void SetUp() {
		totalColumn.OverridePar(course.Par);
		for(int i = 0; i < holes.Count; i++) {
			ScoreColumn column = scoreColumns[i];
			column.LoadHoleData(holes[i],i + 1);
		}
	}

	public void Retire() {
		if(OnRetirePressed != null) {
			OnRetirePressed();
		}
	}
}
