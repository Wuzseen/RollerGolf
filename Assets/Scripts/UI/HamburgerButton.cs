﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HamburgerButton : MonoBehaviour {
	public ScoreCard scoreCard;
	public float tweenTime = .3f;
	
	private RectTransform scoreCardTransform;
	private bool on = false;

	void Awake() {
		CourseHandler.OnCourseEnd += On;
		scoreCardTransform = scoreCard.transform as RectTransform;	
		scoreCardTransform.localScale = new Vector2(1f,0f);
	}

	void OnDestroy() {
		CourseHandler.OnCourseEnd -= On;
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
		scoreCardTransform.DOScaleY(1f,tweenTime);
	}

	void Off() {
		on = false;
		scoreCardTransform.DOScaleY(0f,tweenTime);
	}
}
