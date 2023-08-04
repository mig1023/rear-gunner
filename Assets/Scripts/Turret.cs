using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public Transform BoneTurret;   
	public Transform BoneBarrelTurn;
	public GameObject shell;
	// public GameObject shotsFlash;  
	
	public int verticalLimithMin;
	public int verticalLimithMax;
	public float speed;
	public float shellSize;
	public float shellSpeed;
	
	Vector3 rotateY = Vector2.zero;

	void Update()
	{
		Vector3 rotateX = transform.eulerAngles;
		rotateX.y += Input.GetAxis("Mouse X") * speed;
		BoneTurret.rotation = Quaternion.Euler(rotateX);
		
		rotateY.y = rotateX.y;
		rotateY.x += Input.GetAxis("Mouse Y") * speed * -1;
		rotateY.x = Mathf.Clamp(rotateY.x, verticalLimithMin, verticalLimithMax);
		BoneBarrelTurn.rotation = Quaternion.Euler(rotateY);

		if (Input.GetMouseButtonDown(0))
		{
            Transform barrel = BoneBarrelTurn.GetChild(0);

            Quaternion shellRot = Quaternion.Euler(
                barrel.eulerAngles.x + Random.Range(-3f, 3f),
                barrel.eulerAngles.y + Random.Range(-3f, 3f),
                barrel.eulerAngles.z + Random.Range(-3f, 3f));

            float shot = shellSpeed + Random.Range(-1 * shellSpeed * 0.2f, shellSpeed * 0.2f);
            Vector3 dir = shellRot * Vector3.forward * shot;

            var instShell = Instantiate(shell, barrel.position, barrel.rotation);
            instShell.transform.localScale = new Vector3(shellSize, shellSize, shellSize);
            instShell.GetComponent<Rigidbody>().AddForce(dir);


            // var instFlash = Instantiate(shotsFlash, barrel.position, Quaternion.Euler(barrel.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z)); 
            // instFlash.transform.localScale = new Vector3(shellSize, shellSize, shellSize);
            // Destroy(instFlash, 3f); 
        }
	}
}
