using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(Selfliquidator());
	}

	void Update()
	{
	
	}
	
	private IEnumerator Selfliquidator()
	{
		yield return new WaitForSeconds(2f);
			Destroy(gameObject);
	}
}
