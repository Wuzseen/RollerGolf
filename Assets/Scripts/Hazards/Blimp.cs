using UnityEngine;
using System.Collections;

public class Blimp : MonoBehaviour {
	
	public float moveSpeed = 1.0f;
	
	public float maxGainAngle = 25f;
	
	public Sprite rightSprite, leftSprite;
	
	public float leftBound, rightBound;
	

	private SpriteRenderer sr;
	private float randomJitter;
	
	// Use this for initialization
	void Start () {
		
		sr = this.GetComponent<SpriteRenderer>();
		
		randomJitter = Random.Range (-.25f,.25f);
		
		moveSpeed += randomJitter;
		//rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		float newRot = Mathf.Cos (Time.time)*(maxGainAngle + randomJitter);
		this.transform.position += (this.transform.right * moveSpeed * Time.deltaTime);
		this.transform.Rotate(new Vector3(0,0,newRot) * Time.deltaTime * 0.15f );
		
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

		Destroy(GetComponent<PolygonCollider2D>());
		gameObject.AddComponent<PolygonCollider2D>();

	}
	
}
