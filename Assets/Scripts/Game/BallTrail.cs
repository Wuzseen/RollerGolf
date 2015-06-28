using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallTrail : MonoBehaviour {

	private GameObject ball;

	public GameObject trail;

	public List<GameObject> trails;

	private bool inAction;
	public int numFrames;

	public int numTrails;

	public float decreaseAlphaBy;

	// Use this for initialization
	void Start () {
		inAction = false;
		ball = GameObject.Find ("Ball");
		CourseHandler.OnActionBegin += HandleOnActionBegin;
		CourseHandler.OnActionEnd += HandleOnActionEnd;
	}

	void OnDestroy() {
		CourseHandler.OnActionBegin -= HandleOnActionBegin;
		CourseHandler.OnActionEnd -= HandleOnActionEnd;
	}
	// Update is called once per frame
	void Update () {
	
	}

	void HandleOnActionEnd() {
		inAction = false;

		LineRenderer[] lr = GetComponentsInChildren<LineRenderer>();

		if(lr.Length > numTrails)
		{
			GameObject temp = trails[0];
			trails.RemoveAt(0);
			Destroy (temp);
		}


		for(int i = 0; i < lr.Length; i++)
		{
			Color newColor = lr[i].material.color - new Color(0,0,0,decreaseAlphaBy);
			lr[i].material.SetColor("_Color", newColor);
		}

	}
	
	void HandleOnActionBegin () {
		inAction = true;
		StartCoroutine(recordTrail());
		//create new trail object in trails array
		//parent it to balltraildrawer
		//every x frames add a new point to the line renderer
	}

	private IEnumerator recordTrail() {
		trails.Add((GameObject)Instantiate(trail));
		LineRenderer current = trails[trails.Count-1].GetComponent<LineRenderer>();
		trails[trails.Count-1].transform.parent = this.transform;

		int startFrame = Time.frameCount;
		int position = 0;
		while(inAction) {
			if((Time.frameCount - startFrame) % numFrames == 0)
			{
				current.SetVertexCount(position+1);
				current.SetPosition(position, ball.transform.position);
				position++;
			}
			yield return null;
		}
	}

}
