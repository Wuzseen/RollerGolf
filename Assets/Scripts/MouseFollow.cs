using UnityEngine;
using System.Collections;

public class MouseFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		this.transform.position = new Vector3(mp.x,mp.y,0.0f);
	}
}
