using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HamburgerButton : MonoBehaviour {
	public ScoreCard scoreCard;
	private RectTransform scoreCardTransform;
	public float tweenTime = .3f;


	private Vector2 scoreCardDimensions;
	private bool on = false;

	void Awake() {
		scoreCardTransform = scoreCard.transform as RectTransform;
		scoreCardDimensions = scoreCardTransform.rect.size;
		scoreCardTransform.localScale = new Vector2(1f,0f);
	}

	public void Toggle() {
		if(on) {
			Off ();
		} else {
			On ();
		}
	}

	void On() {
		on = true;
		scoreCard.UpdateScores();
		scoreCardTransform.DOScaleY(1f,tweenTime);
	}

	void Off() {
		on = false;
		scoreCardTransform.DOScaleY(0f,tweenTime);
	}
}
