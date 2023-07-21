using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject Aircraft; 

    void Start()
    {
        InvokeRepeating("Spawning", 3f, 5f);
    }

    private void Spawning()
    {
        GameObject battleship = GameObject.Find("Battleship");

        Vector3 pos = battleship.gameObject.transform.position;
        pos.z -= 300;
        pos.y += 30;

        Instantiate(Aircraft, pos, battleship.gameObject.transform.rotation);
    }
}
