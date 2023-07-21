using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Aircraft;

    private float Behind = 300;
    private float Altitude = 30;

    void Start()
    {
        InvokeRepeating("Spawning", 3f, 10f);
    }

    private void Spawning()
    {
        GameObject battleship = GameObject.Find("Battleship");

        Vector3 shipPosition = battleship.gameObject.transform.position;
        shipPosition.z -= Behind;
        shipPosition.y += Altitude;
        shipPosition.x += Random.Range(-30, 30);

        Instantiate(Aircraft, shipPosition, battleship.gameObject.transform.rotation);
    }
}
