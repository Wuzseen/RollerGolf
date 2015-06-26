using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public delegate void GameEvent();
	public static event GameEvent OnGameBegin, OnGameEnd;
	public static event GameEvent OnHoleBegin, OnHoleEnd;

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
		while(playing) {
			yield return StartCoroutine(GameHole ());
		}
		Raise (OnGameEnd);
	}

	private bool inHole;

	IEnumerator GameHole () {
		print ("Hole begun");
		inHole = true;
		Raise (OnHoleBegin);
		while(inHole) {
			yield return null;
		}
		Raise (OnHoleEnd);
		print ("Hole end");
	}

	void Raise(GameEvent anEvent) {
		if(anEvent != null) {
			anEvent();
		}
	}
}
