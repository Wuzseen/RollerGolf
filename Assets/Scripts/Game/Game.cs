using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public static CourseData SelectedCourse { get; set; }

	private static Game instance;
	public static Game Instance {
		get {
			return instance;
		}
	}

	public GameObject ui;

	public CourseHandler courseManager;

	void Awake() {
		if(instance != null) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		if(SelectedCourse == null) {
			SelectedCourse = CourseLoader.Instance.Courses[0];
			Instantiate(ui);
		}
		courseManager.BeginNewCourse(SelectedCourse);
		ScoreCard.OnRetirePressed += HandleOnRetirePressed;
	}

	void OnDestroy() {
		ScoreCard.OnRetirePressed -= HandleOnRetirePressed;
	}

	void HandleOnRetirePressed () {
		Application.LoadLevel("mainmenu");
		Destroy(this.gameObject);
	}
}
