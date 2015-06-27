using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour {

	public float rotationDegree = 10;

	public bool placing;

	private GameObject MouseFollow;

	// Use this for initialization
	void Start () {
		MouseFollow = GameObject.Find("Mouse Follower");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void beginPlacing() {
		StartCoroutine(Placing ());
	}

	public IEnumerator Placing() {
		if(placing) yield break;

		placing = true;
		yield return null;

		while(placing) {
			if(this.transform.parent == null)
			{
				this.transform.parent = MouseFollow.transform;
			}
			
			if(Input.GetAxis("Mouse ScrollWheel") != 0)
			{
				this.transform.Rotate(new Vector3(0,0,rotationDegree * Input.GetAxis("Mouse ScrollWheel")));
			}
			
			if(Input.GetButtonDown("Fire1"))
			{
				this.transform.SetParent(null);
				placing = false;
			}

			if(Input.GetButtonDown("Fire2"))
			{
				Destroy (this.gameObject);
				yield break;
			}

			yield return null;
		}
	}


	void OnMouseDown() {
		StartCoroutine(Placing());
	}
}
