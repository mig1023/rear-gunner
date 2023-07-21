using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
	//public Transform[] Points;
	public float Speed;
	public int Hitpoints = 2;
	//public Rigidbody Body;
	public ParticleSystem ParticleExplosive;

	private float Falling = 0f;
	private bool Dead = false;
	//private byte Selected;

	void Update()
	{
		//if (Points.Length > 0)
		//{
		//	var heading = transform.position - Points[Selected].position;

		//	if (heading.sqrMagnitude > 10)
		//	{
		//		if (Dead)
		//		{
		//			Falling -= 0.001f;
		//		}

		//		Vector3 target = Points[Selected].position - transform.position;
		//		Vector3 direction = Vector3.RotateTowards(transform.forward, target, 0.1f, 0.1f);
		//		direction.y = Falling;
		//		transform.rotation = Quaternion.LookRotation(direction);
		//	}
		//	else
		//	{
		//		if (Selected < Points.Length - 1)
		//		{
		//			Selected += 1;
		//		}
		//	}
		//}

		//if ((ParticleExplosive != null) && (transform.position.y <= 5))
		//{
		//	var instParticle = Instantiate(ParticleExplosive, transform.position,
		//		Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

		//	instParticle.transform.localScale = new Vector3(15, 15, 5);

		//	Destroy(instParticle.gameObject, ParticleExplosive.main.duration);
		//	Destroy(gameObject);
		//}
	}

	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * Speed * Time.deltaTime;
		//Body.MovePosition(Body.position + movement);
		Rigidbody body = GetComponent<Rigidbody>();
		body.MovePosition(body.position + movement);
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
