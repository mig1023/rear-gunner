using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
	public Transform BoneTurret;   
    public Transform BoneBarrelTurn;
	
	public int verticalLimithMin = -30;
	public int verticalLimithMax = 0;
	public int horizontalLimithMin = -30;
	public int horizontalLimithMax = 30;
	
	Vector2 rotationX = Vector2.zero;
	Vector2 rotationY = Vector2.zero;
	
	public float speed = 1;

	void Update()
	{
		rotationX.y = Mathf.Clamp(rotationY.y - Input.GetAxis("Mouse X"), horizontalLimithMin, horizontalLimithMax);
		BoneTurret.eulerAngles = (Vector2)rotationX * speed;

		rotationY.x = Mathf.Clamp(rotationY.x - Input.GetAxis("Mouse Y"), verticalLimithMin, verticalLimithMax);
		rotationY.y = Mathf.Clamp(rotationY.y - Input.GetAxis("Mouse X"), horizontalLimithMin, horizontalLimithMax);
		BoneBarrelTurn.eulerAngles = (Vector2)rotationY * speed;
	}
}
