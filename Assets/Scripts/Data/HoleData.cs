using UnityEngine;
using System.Collections;
using FullSerializer;

public class HoleData {
	[fsProperty]
	public string HoleName;

	[fsProperty]
	public int Par;

	[fsProperty]
	public string HoleSceneName;

	public HoleData() {
		HoleName = "TestHole";
		Par = 3;
		HoleSceneName = "testHole";
	}

	public override string ToString () {
		return string.Format ("Hole {0}, Par {1:D} - {2}",HoleName,Par.ToString(),HoleSceneName);
	}
}
