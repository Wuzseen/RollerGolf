using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HamburgerButton : MonoBehaviour {
	public RectTransform scoreCard;
	public float tweenTime = .3f;


	private Vector2 scoreCardDimensions;
	private bool on = false;

	void Awake() {
		scoreCardDimensions = scoreCard.rect.size;
		scoreCard.localScale = new Vector2(1f,0f);
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
		scoreCard.DOScaleY(1f,tweenTime);
	}

	void Off() {
		on = false;
		scoreCard.DOScaleY(0f,tweenTime);
	}
}
