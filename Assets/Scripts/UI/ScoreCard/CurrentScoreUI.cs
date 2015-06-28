using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentScoreUI : MonoBehaviour {
	public Text holeNumber, strokes, objects, parText;

	private int currentHole = 0;

	void Awake() {
		HoleScorer.OnScoreChanged += HandleOnScoreChanged;
		CourseHandler.OnHoleBegin += HandleOnHoleBegin;
	}

	void HandleOnHoleBegin () {
		parText.text = string.Format("Par {0:D}",Game.SelectedCourse.Holes[currentHole].Par);
		currentHole++;
		holeNumber.text = currentHole.ToString();
	}

	void OnDestroy() {
		HoleScorer.OnScoreChanged -= HandleOnScoreChanged;
		CourseHandler.OnHoleBegin -= HandleOnHoleBegin;
	}

	void HandleOnScoreChanged (CourseScore scoreObject) {
		holeNumber.text = currentHole.ToString();
		strokes.text = string.Format("{0:D} Strokes",scoreObject.CurrentHoleScore.Strokes);
		objects.text = string.Format("{0:D} Objects",scoreObject.CurrentHoleScore.Objects);

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
