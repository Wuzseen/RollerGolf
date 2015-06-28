using UnityEngine;
using System.Collections;

public class ObjectPlacement : MonoBehaviour {
	public delegate void PlacementEvent();
	public static event PlacementEvent OnObjectSelected, OnObjectRemoved;

	public float rotationDegree = 10;

	public bool placing;

	private bool valid;

	public bool keepUpright;

	private GameObject MouseFollow;

	// Use this for initialization
	void Start () {
		MouseFollow = GameObject.Find("Mouse Follower");
		valid = true;
	}

	void Raise(PlacementEvent anEvent) {
		if(anEvent != null) {
			anEvent();
		}
	}

	public void beginPlacing() {
		StartCoroutine(Placing ());
	}

	private bool reSelect = false;
	public IEnumerator Placing() {
		if(placing) yield break;

		placing = true;
		yield return null;
		if(!reSelect) {
			Raise (OnObjectSelected);
		}

		while(placing) {
			if(this.transform.parent == null)
			{
				this.transform.parent = MouseFollow.transform;
			}
			
			if(Input.GetAxis("Mouse ScrollWheel") != 0 && !keepUpright)
			{
				this.transform.Rotate(new Vector3(0,0,rotationDegree * Input.GetAxis("Mouse ScrollWheel")));
			}
			
			if(Input.GetButtonDown("Fire1") && valid)
			{
				this.transform.SetParent(null);
				placing = false;
			}

			if(Input.GetButtonDown("Fire2"))
			{
				Raise (OnObjectRemoved);
				Destroy (this.gameObject);
				yield break;
			}

			yield return null;
		}
		reSelect = true;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		valid = false;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		valid = true;
	}


	void OnMouseDown() {
		StartCoroutine(Placing());
	}
}
