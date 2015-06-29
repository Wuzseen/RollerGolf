using UnityEngine;
using System.Collections;
public class CourseHandler : MonoBehaviour {
	public delegate void GameEvent();
	public static event GameEvent OnCourseBegin, OnCourseEnd;
	public static event GameEvent OnHoleBegin, OnHoleEnd;
	public static event GameEvent OnPlacementBegin, OnPlacementEnd;
	public static event GameEvent OnActionBegin, OnActionEnd;

	public AudioClip applause;

	private int holeIndex = 0;

	private CourseData course;

	public void BeginNewCourse(CourseData course) {
		this.course = course;
		StartCoroutine(RollerGolf());
	}

	void OnEnable () {
		Hole.OnObjectiveReached += HandleOnObjectiveReached;
		ObjectPlacer.OnRetry += EndAction;
		ObjectPlacer.OnConfirm += ConfirmPlacement;
	}

	void OnDisable () {
		Hole.OnObjectiveReached -= HandleOnObjectiveReached;
		ObjectPlacer.OnRetry -= EndAction;
		ObjectPlacer.OnConfirm -= ConfirmPlacement;
	}

	private bool success = false;
	void HandleOnObjectiveReached () {
		inHole = false;
		success = true;
		EndAction();
	}

	public void EndAction () {
		action = false;
	}

	bool playing;
	IEnumerator RollerGolf () {
		Raise (OnCourseBegin);
		playing = true;
		int holeCount = 0;
		while(playing) {
			LoadHole();
			yield return StartCoroutine(GameHole ());
			AdvanceHole();
			if(holeCount >= course.Holes.Count) {
				playing = false;
			}
		}
		Raise (OnCourseEnd);
	}

	private bool inHole;

	private bool fullGame = false;
	void LoadHole() {
		HoleData hd = this.course.Holes[holeIndex];
		if(Application.loadedLevelName == "game" || fullGame) {
			fullGame = true;
			Application.LoadLevel(hd.HoleSceneName);
		}
	}

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
		yield return null;
		print ("Hole begun");
		inHole = true;
		success = false;
		Raise (OnHoleBegin);
        yield return new WaitForSeconds(CameraController.RESET_TIME);
		while(inHole && playing) {
			if(playing) yield return StartCoroutine(PlacementPhase());
			if(playing) yield return StartCoroutine(ActionPhase());
		}
		if(success) {
			SoundManager.PlaySFX(applause);
			yield return new WaitForSeconds(applause.length);
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
