using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
	public Transform BoneTurret;   
    public Transform BoneBarrelTurn;
	
	Vector2 rotationX = Vector2.zero;
	Vector2 rotationY = Vector2.zero;
	
	public float speed = 1;

	void Update()
	{
		rotationX.y = Mathf.Clamp(rotationY.y - Input.GetAxis("Mouse X"), -30, 30);
		BoneTurret.eulerAngles = (Vector2)rotationX * speed;

		rotationY.x = Mathf.Clamp(rotationY.x - Input.GetAxis("Mouse Y"), -30, 0);
		rotationY.y = Mathf.Clamp(rotationY.y - Input.GetAxis("Mouse X"), -30, 30);
		BoneBarrelTurn.eulerAngles = (Vector2)rotationY * speed;
	}
}
