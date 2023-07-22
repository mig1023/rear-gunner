using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject[] Aircraft;
    public int Shotdown = 0;
    public Text ShotdownField;

    private float Behind = 300;
    private float Altitude = 30;

    void Start()
    {
        InvokeRepeating("Spawning", 3f, 10f);
    }

    private void Update()
    {
        ShotdownField.text = $"Aircrafts shot down: {Shotdown}";
    }

    private void Spawning()
    {
        GameObject battleship = GameObject.Find("Battleship");

        Vector3 shipPosition = battleship.gameObject.transform.position;
        shipPosition.z -= Behind;
        shipPosition.y += Altitude;
        shipPosition.x += Random.Range(-30, 30);

        int typeAircraft = Random.Range(0, Aircraft.Length);
        Instantiate(Aircraft[typeAircraft], shipPosition, battleship.gameObject.transform.rotation);
    }
}
