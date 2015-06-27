using UnityEngine;
using System.Collections;

public class HoleScorer : MonoBehaviour {
	public delegate void ScorerEvent(int newScore);
	public static event ScorerEvent OnScoreChanged;

	private int currentActionAttempts = 0;
	private int placedObjects = 0;

	// Use this for initialization
	void Awake () {
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnHoleBegin += ResetHoleScore;
		ObjectPlacement.OnObjectPlaced += HandleOnObjectPlaced;
		ObjectPlacement.OnObjectRemoved += HandleOnObjectRemoved;
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
		currentActionAttempts = 0;
		placedObjects = 0;
	}

	void HandleOnActionBegin () {
		currentActionAttempts++;
		ScoreChanged();
	}

	void ScoreChanged() {
		print("Score has changed");
		if(OnScoreChanged != null) {
			OnScoreChanged(Score);
		}
	}

	int Score {
		get {
			return placedObjects + currentActionAttempts;
		}
	}
}
