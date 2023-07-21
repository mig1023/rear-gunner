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
			DeadFalling(movement);
		}

		GameObject battleship = GameObject.Find("Battleship");
		Vector3 shipPosition = battleship.gameObject.transform.position;

		Move(movement);

		if (transform.position.z > shipPosition.z)
        {
			Destruction(withFireworks: false);
		}
		else if (transform.position.y <= 5)
        {
			Destruction(withFireworks: true);
		}
	}

	private void DeadFalling(Vector3 movement)
    {
		Falling -= 0.001f;

		Vector3 direction = movement;
		direction.y = Falling;
		transform.rotation = Quaternion.LookRotation(direction);
	}

	private void Move(Vector3 movement)
    {
		Rigidbody body = GetComponent<Rigidbody>();
		body.MovePosition(body.position + movement);
	}

	private void Destruction(bool withFireworks)
    {
		if (withFireworks)
        {
			var instParticle = Instantiate(ParticleExplosive, transform.position,
				Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));

			instParticle.transform.localScale = new Vector3(15, 15, 5);

			Destroy(instParticle.gameObject, ParticleExplosive.main.duration);
		}

		Destroy(gameObject);
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
