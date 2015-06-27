using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoleData : MonoBehaviour {
	public string HoleName {
		get {
			return nameField.text;
		}
		set {
			nameField.text = value;
		}
	}

	[SerializeField]
	private Text nameField;

	public int Par;

	public int Score;
}
