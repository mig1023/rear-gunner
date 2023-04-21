using UnityEngine;

public class SW_Props_Shark : SW_Props_AnimalsMove
{
 
    void Awake()
    {
        GetComponent<Animator>().Play("Idle", 0, Random.Range(0,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));  
    }

}
