﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public delegate void BallCollisionEvent(Vector3 point);
    public static event BallCollisionEvent OnWaterHit, OnGroundImpact, OnHoleHit;


    void OnEnable()
    {
        CourseHandler.OnPlacementBegin += HandleOnPlacementBegin;
        CourseHandler.OnPlacementEnd += HandleOnPlacementEnd;
    }

    void OnDisable()
    {
        CourseHandler.OnPlacementBegin -= HandleOnPlacementBegin;
        CourseHandler.OnPlacementEnd -= HandleOnPlacementEnd;
    }

    void HandleOnPlacementBegin()
    {
        this.GetComponent<Collider2D>().enabled = false;
    }

    void HandleOnPlacementEnd()
    {
        this.GetComponent<Collider2D>().enabled = true;
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        Raise (OnGroundImpact, collision.contacts[0].point);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameConsts.TAG_WATER)) {
            Raise(OnWaterHit, this.transform.position);
		} else if(collision.CompareTag(GameConsts.TAG_HOLE)) {
			print ("TAG");
			Raise (OnHoleHit, this.transform.position);
		}
    }

    void Raise(BallCollisionEvent anEvent, Vector3 point) {
        if (anEvent != null) {
            anEvent(point);
        }
    }
}
