using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public float Speed; 
	public Rigidbody Body;
		
	private void FixedUpdate()
	{
		Vector3 movement = transform.forward * Speed * Time.deltaTime;
		Body.MovePosition(Body.position + movement);
	}
}
