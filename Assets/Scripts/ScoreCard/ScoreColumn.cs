using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreColumn : MonoBehaviour {
	public Text score, par, hole;

	private int number = 0;
	private HoleData holeData;

	public void LoadHoleData(HoleData data, int holeNumber) {
		this.holeData = data;
		this.number = holeNumber;
		this.score.text = "0";
		this.par.text = data.Par.ToString();
		this.hole.text = holeNumber.ToString();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
