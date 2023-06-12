using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
	public int Hitpoints = 2;
	
	public void OnTriggerEnter(Collider collision)
	{
		Hitpoints -= 1;
		
		if (Hitpoints <= 0)
		{
			GetComponent<Move>().Dead = true;
			GetComponent<Animator>().Play("Dead2");
		}
	}
}
