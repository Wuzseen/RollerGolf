using UnityEngine;
using System.Collections;

public class SandTrapHover : MonoBehaviour {

	public float hoverAmount = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float newY = Mathf.Sin (Time.time);
		float newX = Mathf.Sin (2.3f * Time.time);

		Vector3 hoverPos = new Vector3(newX * 0.5f, newY * hoverAmount, 0);

		this.transform.position += hoverPos * Time.deltaTime;
	}
}
