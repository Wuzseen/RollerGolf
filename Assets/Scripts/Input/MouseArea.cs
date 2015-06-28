using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MouseArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public void OnPointerExit (PointerEventData eventData) {
		MouseIsOver = false;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		MouseIsOver = true;
	}

	public static bool MouseIsOver;
	// Use this for initialization
	void Start () {
		MouseIsOver = false;
	}
}
