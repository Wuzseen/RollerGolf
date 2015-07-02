using UnityEngine;
using System.Collections;

public class DevCourseLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
       //Debug.Log(FetchKey());

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("yo");
            }
        }

	
	}
}
