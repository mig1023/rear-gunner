using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    public GameObject OtherFragment;
    public GameObject BottomFragment;

    void MoveFragment(GameObject fragment, float step)
    {
        Vector3 current = fragment.transform.position;
        fragment.transform.position = new Vector3(current.x, current.y, current.z + step);
    } 

    void OnTriggerEnter(Collider col)
    {
        MoveFragment(OtherFragment, 500);
        MoveFragment(BottomFragment, 250);
    }
}
