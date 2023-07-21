using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float Speed; 
		
	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * Speed * Time.deltaTime;
		Rigidbody body = GetComponent<Rigidbody>();
		body.MovePosition(body.position + movement);
	}
}
