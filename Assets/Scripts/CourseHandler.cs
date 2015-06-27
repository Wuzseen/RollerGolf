using UnityEngine;
using System.Collections;
public class CourseHandler : MonoBehaviour {
	public delegate void GameEvent();
	public static event GameEvent OnGameBegin, OnGameEnd;
	public static event GameEvent OnHoleBegin, OnHoleEnd;
	public static event GameEvent OnPlacementBegin, OnPlacementEnd;
	public static event GameEvent OnActionBegin, OnActionEnd;

	private int holeIndex = 0;

	void Start() {
		StartCoroutine(RollerGolf());
	}

	void OnEnable () {
		Hole.OnObjectiveReached += HandleOnObjectiveReached;
	}

	void OnDisable () {
		Hole.OnObjectiveReached -= HandleOnObjectiveReached;
	}

	void HandleOnObjectiveReached () {
		inHole = false;
		EndAction();
	}

	public void EndAction () {
		action = false;
	}

	bool playing;
	IEnumerator RollerGolf () {
		Raise (OnGameBegin);
		playing = true;
		while(playing) {
			yield return StartCoroutine(GameHole ());
			AdvanceHole();
		}
		Raise (OnGameEnd);
	}

	private bool inHole;

	void AdvanceHole() {
		holeIndex++;
		placing = false;
		action = false;
	}

	void ExitCourse() {
		inHole = false;
		playing = false;
		placing = false;
		action = false;
	}

	IEnumerator GameHole () {
		print ("Hole begun");
		inHole = true;
		Raise (OnHoleBegin);
		while(inHole && playing) {
			if(playing) yield return StartCoroutine(PlacementPhase());
			if(playing) yield return StartCoroutine(ActionPhase());
		}
		Raise (OnHoleEnd);
		print ("Hole end");
	}

	public void ConfirmPlacement() {
		print ("placement confirmed");
		placing = false;
	}

	private bool placing;
	IEnumerator PlacementPhase () {
		placing = true;
		print ("Placing begin");
		Raise (OnPlacementBegin);
		while(placing) {
			yield return null;
		}
		print ("Placing end");
		Raise (OnPlacementEnd);
	}

	private bool action;
	IEnumerator ActionPhase () {
		action = true;
		print ("Action begin");
		Raise (OnActionBegin);
		while(action) {
			yield return null;
		}
		print ("Action end");
		Raise (OnActionEnd);
	}

	void Raise(GameEvent anEvent) {
		if(anEvent != null) {
			anEvent();
		}
	}
}
