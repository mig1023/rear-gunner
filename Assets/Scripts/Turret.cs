using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public Transform BoneTurret;   
	public Transform BoneBarrelTurn;
	public GameObject shell;
	// public GameObject shotsFlash;  
	
	public int verticalLimithMin = -90;
	public int verticalLimithMax = -15;
	public float speed = 3;
	public float shellSize = 0.45f;
	public float shellSpeed = 700f;
	
	Vector3 rotateY = Vector2.zero;

	void Update()
	{
		Vector3 rotateX = transform.eulerAngles;
		rotateX.y += Input.GetAxis("Mouse X") * speed;
		BoneTurret.rotation = Quaternion.Euler(rotateX);
		
		rotateY.y = rotateX.y;
		rotateY.x += Input.GetAxis("Mouse Y") * speed;
		rotateY.x = Mathf.Clamp(rotateY.x, verticalLimithMin, verticalLimithMax);
		BoneBarrelTurn.rotation = Quaternion.Euler(rotateY);
		
		if (Input.GetMouseButtonDown(0))
		{
			Transform barrel = BoneBarrelTurn.GetChild(0);
			
			Quaternion shellRot = Quaternion.Euler(
				barrel.eulerAngles.x+Random.Range(-3f, 3f),
				barrel.eulerAngles.y+Random.Range(-3f, 3f),
				barrel.eulerAngles.z+Random.Range(-3f, 3f));
			
			var instShell = Instantiate(shell, barrel.position, shellRot); 
			instShell.transform.localScale = new Vector3(shellSize, shellSize, shellSize);
			float shot = shellSpeed + Random.Range(-1 * shellSpeed * 0.2f, shellSpeed * 0.2f);
			Vector3 dir = shellRot * Vector3.up * shot;
			instShell.GetComponent<Rigidbody>().AddForce(dir);
			
			// var instFlash = Instantiate(shotsFlash, barrel.position, Quaternion.Euler(barrel.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z)); 
            // instFlash.transform.localScale = new Vector3(shellSize, shellSize, shellSize);
			// Destroy(instFlash, 3f); 
		}
	}
}
