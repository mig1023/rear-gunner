using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public Transform[] points;
	public float speed; 
	public Rigidbody body;
	
	private byte selected;
	private float acceleration;
	private float falling;
	
	public bool Dead = false;
	
	void Start()
	{
		acceleration = 0f;
		falling = 0f;
	}
	
	void Update()
	{
		if (points.Length > 0)
		{
			var heading = transform.position - points[selected].position;
			
			if (heading.sqrMagnitude > 10)
			{
				if (acceleration < 1)
				{
					acceleration += 0.01f;
				}
				
				if (Dead)
				{
					falling -= 0.001f;
				}
				
				Vector3 target = points[selected].position - transform.position;
				Vector3 direction = Vector3.RotateTowards(transform.forward, target, 0.1f, 0.1f);	
				direction.y = falling;		
				transform.rotation = Quaternion.LookRotation(direction);
			}
			else
			{
				if (selected < points.Length - 1)
				{
					selected += 1;
				}
				else if (acceleration > 0)
				{
					acceleration -= 0.02f;
				}
				else if (acceleration != 0)
				{
					acceleration = 0f;
				}
			}
		}
	}
	
	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * acceleration * speed * Time.deltaTime;
		body.MovePosition(body.position + movement);
	}
}
