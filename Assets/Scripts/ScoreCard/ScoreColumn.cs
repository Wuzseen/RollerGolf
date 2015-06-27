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

	public void SetScore(int holeScore, int parValue) {
		this.score.text = holeScore.ToString();
		if(holeScore < parValue) {
			score.color = Color.green;
		} else if(holeScore == parValue) {
			score.color = Color.white;
		} else {
			score.color = Color.red;
		}

	}

	public void SetScore(int holeScore) {
		SetScore(holeScore, this.holeData.Par);
	}

	public void OverridePar(int newPar) {
		this.par.text = newPar.ToString();
	}
}
