using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HoleStartEffect : MonoBehaviour {
	public Text title, par;

	private int holeCount = -1;
	void Awake() {
		title.rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
		par.rectTransform.anchoredPosition = new Vector2(0f, Screen.height);
		CourseHandler.OnHoleBegin += HandleOnHoleBegin;
	}

	void OnDestroy() {
		CourseHandler.OnHoleBegin -= HandleOnHoleBegin;
	}

	void HandleOnHoleBegin () {
		StartCoroutine(EntranceRoutine());
	}

	IEnumerator EntranceRoutine() {
		holeCount++;
		HoleData data = Game.SelectedCourse.Holes[holeCount];
		title.text = data.HoleName;
		par.text = string.Format("Par {0:D}", data.Par);
		title.rectTransform.DOAnchorPos(new Vector2(0f, 40f),.5f);
		yield return new WaitForSeconds(.7f);
		par.rectTransform.DOAnchorPos(new Vector2(0f,0f),.3f);
		yield return new WaitForSeconds(2f);
		title.rectTransform.DOAnchorPos(new Vector2(0f, -Screen.height),.3f);
		yield return new WaitForSeconds(.15f);
		par.rectTransform.DOAnchorPos(new Vector2(0f, -Screen.height),.3f);
	}
}
