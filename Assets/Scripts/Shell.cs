using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
	public ParticleSystem particle_Explosive;
	
	void Start()
	{
		StartCoroutine(Selfliquidator());
	}
	
	void OnTriggerEnter(Collider col)
	{
		Quaternion explosion = Quaternion.Euler(Random.Range(0, 360),Random.Range(0, 360),Random.Range(0,360));
		var instExplosion = Instantiate(particle_Explosive, transform.position, explosion); 
		instExplosion.transform.localScale = new Vector3(3, 3, 3);

		Destroy(instExplosion.gameObject, particle_Explosive.main.duration); 
		Destroy(gameObject);
	}
	
	private IEnumerator Selfliquidator()
	{
		yield return new WaitForSeconds(2f);
			Destroy(gameObject);
	}
}
