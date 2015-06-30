using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {

	public float moveSpeed = 5.0f;

	public float maxGainAngle = 25f;

	public Sprite rightSprite, leftSprite;

	public float leftBound, rightBound;

	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private float randomJitter;

    private Vector3 startingPosition;

    private float rotationFactor = 0;

	// Use this for initialization
	void Start () {
		sr = this.GetComponent<SpriteRenderer>();

		randomJitter = Random.Range (-3.0f,5.0f);

		moveSpeed += randomJitter;

	}

    void OnEnable()
    {
        CourseHandler.OnHoleBegin += HandleOnHoleBegin;
    }

    void OnDisable()
    {
        CourseHandler.OnHoleBegin -= HandleOnHoleBegin;
    }

    void HandleOnHoleBegin()
    {
        startingPosition = this.transform.position;
    }

	// Update is called once per frame
	void Update () {
        rotationFactor += Time.deltaTime;
        float newRot = Mathf.Cos (rotationFactor)*(maxGainAngle + randomJitter);
		this.transform.position += ((this.transform.right * moveSpeed) * Time.deltaTime);
		this.transform.Rotate(new Vector3(0,0,newRot) * Time.deltaTime);

        if((this.transform.position.x < leftBound || this.transform.position.x > rightBound)) {
			switchSprite();
            moveSpeed *= -1;
            Vector3 target = new Vector3(this.transform.position.x, startingPosition.y, this.transform.position.z);
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            this.transform.position = target;
            //this.transform.position += this.transform.right * moveSpeed * Random.Range(1.0f, 1.5f);

            //maxGainAngle *= Random.RandomRange(0.9f, 1.1f);

            rotationFactor = 0;
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
