using UnityEngine;
using System.Collections;

public class HoleScorer : MonoBehaviour {

	private int currentActionAttempts = 0;

	private static HoleScorer instance;

	// Use this for initialization
	void Awake () {
		if(instance != null) {
			Destroy(instance);
			return;
		}
		instance = this;
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnHoleBegin += ResetHoleScore;
	}

	void ResetHoleScore () {
		currentActionAttempts = 0;
	}
	
	void OnDestroy () {
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnHoleBegin -= ResetHoleScore;
	}

	void HandleOnActionBegin () {
		currentActionAttempts++;
	}

	int GetPlaceablesScore () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag(GameConsts.TAG_SCORABLE);
		return objs.Length;
	}

	int Score {
		get {
			return GetPlaceablesScore() + currentActionAttempts;
		}
	}

	public static int HoleScore {
		get {
			return instance.Score;
		}
	}
}
