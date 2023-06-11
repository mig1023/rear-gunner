using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft : MonoBehaviour
{
	public void OnTriggerEnter(Collider collision)
	{
		GetComponent<Animator>().Play("Dead2");
	}
}
