using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoleDataButton : MonoBehaviour {
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
}
