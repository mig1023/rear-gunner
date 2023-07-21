using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
	public float Speed;
	public int Hitpoints = 2;
	public ParticleSystem ParticleExplosive;

	private float Falling = 0f;
	private bool Dead = false;

	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * Speed * Time.deltaTime;

		if (Dead)
		{
			Falling -= 0.001f;

			Vector3 direction = movement;
			direction.y = Falling;
			transform.rotation = Quaternion.LookRotation(direction);
		}

		Rigidbody body = GetComponent<Rigidbody>();
		body.MovePosition(body.position + movement);

		if ((ParticleExplosive != null) && (transform.position.y <= 5))
		{
			var instParticle = Instantiate(ParticleExplosive, transform.position,
				Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

			instParticle.transform.localScale = new Vector3(15, 15, 5);

			Destroy(instParticle.gameObject, ParticleExplosive.main.duration);
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter(Collider collision)
	{
		Hitpoints -= 1;
		
		if (Hitpoints <= 0)
		{
			Dead = true;
			GetComponent<Animator>().Play("Dead2");
		}
	}
}
