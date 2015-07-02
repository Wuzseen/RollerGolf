using UnityEngine;
using System.Collections;

public class DevCourseLoader : MonoBehaviour {

    private CourseHandler ch;

	// Use this for initialization
	void Start () {
        ch = this.GetComponentInChildren<CourseHandler>();
	}

	// Update is called once per frame
	void Update () {
      if (Input.GetKey(KeyCode.LeftShift)) {
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                Debug.Log("loading hole 1");
                ch.LoadHole(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("loading hole 2");
                ch.LoadHole(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("loading hole 3");
                ch.LoadHole(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("loading hole 4");
                ch.LoadHole(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("loading hole 5");
                ch.LoadHole(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("loading hole 6");
                ch.LoadHole(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Debug.Log("loading hole 7");
                ch.LoadHole(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Debug.Log("loading hole 8");
                ch.LoadHole(8);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                Debug.Log("loading hole 9");
                ch.LoadHole(9);
            }

        }

	
	}
}
