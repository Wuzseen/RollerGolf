using UnityEngine;
using System.Collections;
using FullSerializer;

public class HoleData {
	[fsProperty]
	public string HoleName;

	[fsProperty]
	public int Par;

	public HoleData() {
		HoleName = "TestHole";
		Par = 3;
	}
}
