using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddObjectGUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public Text buttonText;
	public Image theImage;
    public CanvasGroup grp;

	public GameObject myGameObject;

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
		if(ObjectPlacement.CurrentPlaceable != null) {
			return;
		}
		GameObject spawnedObject = (GameObject)Instantiate(myGameObject);
		spawnedObject.transform.SetParent(GameObject.Find ("Mouse Follower").transform,false);
		spawnedObject.GetComponent<ObjectPlacement>().beginPlacing();
	}

    public void OnPointerEnter(PointerEventData eventData) {
        grp.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData) {
        grp.alpha = 0f;
    }
}
