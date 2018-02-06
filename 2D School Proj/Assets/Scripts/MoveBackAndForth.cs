﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour {

	#region Variables
	public GameObject Endpoint1;
	public GameObject Endpoint2;
	public float speed = .5f;

	private GameObject nextPoint;
	#endregion

	#region Methods
	void Start()
	{
		nextPoint = Endpoint1;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position == nextPoint.transform.position)
		{
			nextPoint = (nextPoint == Endpoint1) ? Endpoint2 : Endpoint1;
			Flip();
		}
		transform.position = Vector2.MoveTowards(transform.position, nextPoint.transform.position,
		speed * Time.deltaTime);
	}

	void Flip()
	{
		if (gameObject.GetComponent<SpriteRenderer>() != null)
		{
			gameObject.GetComponent<SpriteRenderer>().flipX =
			!gameObject.GetComponent<SpriteRenderer>().flipX;
		}
	}
	#endregion
}
