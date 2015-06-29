using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoleScore {
	public int Strokes {get; set;}
	public int Objects {get; set;}

	public HoleScore() {
		this.Strokes = 0;
		this.Objects = 0;
	}

	public HoleScore(int strokes, int objects) {
		this.Strokes = strokes;
		this.Objects = objects;
	}

	public void UpdateScore(int strokes, int objects) {
		this.Strokes = strokes;
		this.Objects = objects;
	}

	public int Score {
		get {
			return Strokes + Objects;
		}
	}
}

public class CourseScore {
	private List<int> holePars;
	private List<HoleScore> holeScores;
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
				t += holeScores[i].Score;
			}
			return t;
		}
	}

	public int PlusMinusScore {
		get {
			return TotalScore - CoursePar;
		}
	}

	private CourseData course;

	public CourseScore(CourseData course) {
		this.course = course;
		holePars = new List<int>();
		holeScores = new List<HoleScore>();
		currentHole = -1;
		for(int i = 0; i < this.course.Holes.Count; i++) {
			holePars.Add(this.course.Holes[i].Par);
			holeScores.Add(new HoleScore());
		}
	}

	public int CurrentHole {
		get {
			return this.currentHole;
		}
	}

	public void NextHole() {
		currentHole++;
		Debug.Log("Scoring began for hole: " + currentHole.ToString());
	}

	public HoleScore CurrentHoleScore {
		get {
			return holeScores[currentHole];
		}
	}

	public void SetCurrentHole(int strokes, int objects) {
		SetHoleScore(currentHole,strokes,objects);
	}

	public void SetHoleScore(int holeIndex, int strokes, int objects) {
		holeScores[holeIndex].UpdateScore(strokes,objects);
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
		ObjectPlacement.OnObjectSelected += HandleOnObjectPlaced;
		ObjectPlacement.OnObjectRemoved += HandleOnObjectRemoved;
		scorer = new CourseScore(Game.SelectedCourse);
	}
	
	void OnDestroy () {
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnHoleBegin -= ResetHoleScore;
		ObjectPlacement.OnObjectSelected -= HandleOnObjectPlaced;
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
		scorer.SetCurrentHole(currentActionAttempts, placedObjects);
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
