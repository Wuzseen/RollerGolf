using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreColumn : MonoBehaviour {
	public Text score, par, hole;

	private int number = 0;
	private HoleData holeData;

	void Awake() {
		this.score.text = "";
	}

	public void LoadHoleData(HoleData data, int holeNumber) {
		this.holeData = data;
		this.number = holeNumber;
		this.score.text = "";
		this.par.text = data.Par.ToString();
		this.hole.text = holeNumber.ToString();
	}

	public void SetScore(int holeScore) {
		this.score.text = holeScore.ToString();
		if(holeScore < holeData.Par) {
			score.color = Color.green;
		} else if(holeScore == holeData.Par) {
			score.color = Color.white;
		} else {
			score.color = Color.red;
		}
	}
}
