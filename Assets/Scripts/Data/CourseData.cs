using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;

public class CourseData {
	[fsProperty]
	public string Name;

	[fsProperty]
	public string ImageName;

	[fsProperty]
	public List<HoleData> Holes;

	public CourseData() {
		Name = "New Course";
		Holes = new List<HoleData>();
	}

	[fsIgnore]
	public int Par {
		get {
			int p = 0;
			for(int i = 0; i < Holes.Count; i++) {
				p += Holes[i].Par;
			}
			return p;
		}
	}

	[fsIgnore]
	public Sprite Image {
		get {
			Sprite ret = Resources.Load<Sprite>(System.IO.Path.Combine(GameConsts.COURSE_IMAGE_DIRECTORY,this.ImageName));
			return ret;
		}
	}
}
