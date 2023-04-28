using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public Transform [] points;
	public float speed = 8.0f; 
	public Rigidbody body;
	
	private byte selected;
	private float acceleration;
	
    void Start()
    {
		acceleration = 0f;
    }

    void Update()
    {
        if (points.Length > 0)
		{
			var heading = transform.position - points[selected].position;
			double distance = heading.sqrMagnitude;
			
			if (distance > 10)
			{
				if (acceleration < 1)
					acceleration += 0.01f;
				
				Vector3 target = points[selected].position - transform.position;
				float step = 5.5f * Time.deltaTime;
				
				Vector3 direction = Vector3.RotateTowards(transform.forward, target, step, 0.0F);	
				direction.y = 0;		
				transform.rotation = Quaternion.LookRotation(direction);
			}
			else
			{
				if (selected < points.Length - 1)
					selected += 1;
				else if (acceleration > 0)
					acceleration -= 0.02f;
				else if (acceleration != 0)
					acceleration = 0;
			}
		}
    }
	
	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * acceleration * speed * Time.deltaTime;
		body.MovePosition(body.position + movement);
	}
}
