using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddObjectGUIHandler : MonoBehaviour {

	public Text buttonText;
	public Image theImage;

	public GameObject myGameObject;

	// Use this for initialization
	void Start () {
	
	}

	public void setName(string newName)
	{
		buttonText.text = newName;
	}

	public void setImage(Sprite newImage)
	{
		theImage.sprite = newImage;
	}

	public void setGameObject(GameObject newGO)
	{
		myGameObject = newGO;
	}

	public void onButtonClick()
	{
		GameObject spawnedObject = (GameObject)Instantiate(myGameObject);
		spawnedObject.transform.SetParent(GameObject.Find ("Mouse Follower").transform,false);
		spawnedObject.GetComponent<ObjectPlacement>().beginPlacing();
	}


	// Update is called once per frame	
	void Update () {
	
	}
}
