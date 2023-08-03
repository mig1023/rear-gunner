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
        GameObject aircraft = GameObject.Find("Aircraft");

        Vector3 airPosition = aircraft.gameObject.transform.position;
        airPosition.z -= Behind;
        airPosition.y += Altitude;
        airPosition.x += Random.Range(-30, 30);

        int typeAircraft = Random.Range(0, Aircraft.Length);
        Instantiate(Aircraft[typeAircraft], airPosition, aircraft.gameObject.transform.rotation);
    }
}
