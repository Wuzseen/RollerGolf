using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseScore {
	private List<int> holePars;
	private List<int> holeScores;
	private int currentHole;
	public int CoursePar {
		get {
			return course.Par;
		}
	}

	public int TotalScore {
		get {
			int t = 0;
			for(int i = 0; i < holeScores.Count; i++) {
				t += holeScores[i];
			}
			return t;
		}
	}

	public int PlusMinusScore {
		get {
			return CoursePar - TotalScore;
		}
	}

	private CourseData course;

	public CourseScore(CourseData course) {
		this.course = course;
		holePars = new List<int>();
		holeScores = new List<int>();
		currentHole = -1;
		for(int i = 0; i < this.course.Holes.Count; i++) {
			holePars.Add(this.course.Holes[i].Par);
			holeScores.Add(0);
		}
	}

	public void NextHole() {
		currentHole++;
		Debug.Log("Scoring began for hole: " + currentHole.ToString());
	}

	public int CurrentHoleScore {
		get {
			return holeScores[currentHole];
		}
	}

	public void SetCurrentHole(int score) {
		SetHoleScore(currentHole,score);
	}

	public void SetHoleScore(int holeIndex, int score) {
		holeScores[holeIndex] = score;
	}
}

public class HoleScorer : MonoBehaviour {
	public delegate void ScorerEvent(CourseScore scoreObject);
	public static event ScorerEvent OnScoreChanged;

	private int currentActionAttempts = 0;
	private int placedObjects = 0;

	private CourseScore scorer;

	// Use this for initialization
	void Awake () {
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnHoleBegin += ResetHoleScore;
		ObjectPlacement.OnObjectPlaced += HandleOnObjectPlaced;
		ObjectPlacement.OnObjectRemoved += HandleOnObjectRemoved;
		scorer = new CourseScore(Game.SelectedCourse);
	}
	
	void OnDestroy () {
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnHoleBegin -= ResetHoleScore;
		ObjectPlacement.OnObjectPlaced -= HandleOnObjectPlaced;
		ObjectPlacement.OnObjectRemoved -= HandleOnObjectRemoved;
	}
	
	void HandleOnObjectRemoved () {
		placedObjects = Mathf.Max(0, placedObjects - 1);
		ScoreChanged();
	}
	
	void HandleOnObjectPlaced () {
		placedObjects++;
		ScoreChanged();
	}
	
	void ResetHoleScore () {
		scorer.NextHole();
		print (scorer.CurrentHoleScore);
		currentActionAttempts = 0;
		placedObjects = 0;
	}

	void HandleOnActionBegin () {
		currentActionAttempts++;
		ScoreChanged();
	}

	void ScoreChanged() {
		scorer.SetCurrentHole(Score);
		if(OnScoreChanged != null) {
			OnScoreChanged(scorer);
		}
	}

	int Score {
		get {
			return placedObjects + currentActionAttempts;
		}
	}
}
