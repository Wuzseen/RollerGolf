using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;

public class CourseData {
	[fsProperty]
	public string Name;
	[fsProperty]
	public List<HoleData> Holes;

	public CourseData() {
		Name = "New Course";
		Holes = new List<HoleData>();
	}
}
