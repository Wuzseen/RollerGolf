using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CourseButton : MonoBehaviour {
	public Text nameField, parField;
	public Image img;

	private CourseData courseData;

	public void SetFromData(CourseData data) {
		this.courseData = data;
		this.nameField.text = data.Name;
		this.parField.text = string.Format("Par: {0:D}",data.Par);
		this.img.sprite = data.Image;
	}

	public void Press() {
		Game.SelectedCourse = this.courseData;
		Application.LoadLevel(GameConsts.GAME_SCENE_NAME);
	}
}
