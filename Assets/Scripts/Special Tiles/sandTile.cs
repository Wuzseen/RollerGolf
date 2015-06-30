using UnityEngine;
using System.Collections;

public class sandTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ball")
        {
            col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
