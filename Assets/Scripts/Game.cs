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

        Vector3 pos = battleship.gameObject.transform.position;
        pos.z -= Behind;
        pos.y += Altitude;
        pos.x += Random.Range(-30, 30);

        Instantiate(Aircraft, pos, battleship.gameObject.transform.rotation);
    }
}
