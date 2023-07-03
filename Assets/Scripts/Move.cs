using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public Transform[] points;
	public float speed; 
	public Rigidbody body;
	public ParticleSystem particle_Explosive;
	
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
		
		if ((particle_Explosive != null) && (transform.position.y <= 5))
		{
			var instParticle = Instantiate(particle_Explosive, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
			instParticle.transform.localScale = new Vector3(15,15,5);

			Destroy(instParticle.gameObject, particle_Explosive.main.duration);
			Destroy(gameObject);
		}
	}
	
	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * acceleration * speed * Time.deltaTime;
		body.MovePosition(body.position + movement);
	}
}
