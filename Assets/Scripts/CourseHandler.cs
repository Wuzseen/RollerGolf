using UnityEngine;
using System.Collections;

public enum State {
	Placing,
	Action,
	Other
}

public class CourseHandler : MonoBehaviour {
	public delegate void GameEvent();
	public static event GameEvent OnGameBegin, OnGameEnd;
	public static event GameEvent OnHoleBegin, OnHoleEnd;
	public static event GameEvent OnPlacementBegin, OnPlacementEnd;
	public static event GameEvent OnActionBegin, OnActionEnd;

	private State state;

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
	}

	bool playing;
	IEnumerator RollerGolf () {
		Raise (OnGameBegin);
		playing = true;
		state = State.Other;
		while(playing) {
			yield return StartCoroutine(GameHole ());
			AdvanceHole();
		}
		Raise (OnGameEnd);
	}

	private bool inHole;

	void AdvanceHole() {
		holeIndex++;
	}

	void ExitCourse() {
		inHole = false;
		playing = false;
		state = State.Other;
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
		state = State.Other;
	}

	IEnumerator PlacementPhase () {
		state = State.Placing;
		Raise (OnPlacementBegin);
		while(state == State.Placing) {
			yield return null;
		}
		Raise (OnPlacementEnd);
	}

	IEnumerator ActionPhase () {
		state = State.Action;
		Raise (OnActionBegin);
		while(state == State.Action) {
			yield return null;
		}
		Raise (OnActionEnd);
	}

	void Raise(GameEvent anEvent) {
		if(anEvent != null) {
			anEvent();
		}
	}
}
