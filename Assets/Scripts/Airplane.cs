using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {

	public float moveSpeed = 5.0f;

	public float maxGainAngle = 25f;

	public Sprite rightSprite, leftSprite;

	public float leftBound, rightBound;

	private Vector3 myPos;

	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private float randomJitter;

	// Use this for initialization
	void Start () {

		Vector3 myPos = this.transform.position;
		sr = this.GetComponent<SpriteRenderer>();

		randomJitter = Random.Range (-3.0f,5.0f);

		moveSpeed += randomJitter;
		//rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float newRot = Mathf.Cos (Time.time)*(maxGainAngle + randomJitter);
		this.transform.position += (this.transform.right * moveSpeed * Time.deltaTime);
		this.transform.Rotate(new Vector3(0,0,newRot) * Time.deltaTime);

		if(this.transform.position.x < leftBound || this.transform.position.x > rightBound) {
			switchSprite();
			moveSpeed *= -1;
		}

	}

	private void switchSprite()
	{
		if(sr.sprite == rightSprite)
		{
			sr.sprite = leftSprite;
		}
		else sr.sprite = rightSprite;
	}
}
