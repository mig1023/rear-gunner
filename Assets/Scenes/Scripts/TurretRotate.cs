using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
	public Transform BoneTurret;   
	public Transform BoneBarrelTurn;
	
	public int verticalLimithMin = -90;
	public int verticalLimithMax = -15;
	public float speed = 3;
	
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
	}
}
