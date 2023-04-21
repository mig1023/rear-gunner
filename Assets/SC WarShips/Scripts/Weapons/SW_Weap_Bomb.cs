using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Weap_Bomb : MonoBehaviour
{
    public GameObject Bomb;
    public Transform  slotDropBomb;
    public int  damage;
 
    SW_Struct_ForAllUnits  self;


    void Start()
    {
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;

    }
    public void f_Drop_Bomb(){

                    Quaternion ShellRot=Quaternion.Euler(transform.eulerAngles.x , transform.eulerAngles.y-90 ,transform.eulerAngles.z );
                    var instShell = Instantiate( Bomb, slotDropBomb.position, ShellRot) ;
                    instShell.GetComponent<SW_Bomb>().team=self.Team;   
                    instShell.GetComponent<SW_Bomb>().damage=damage;   
                    Vector3 dir =ShellRot*Vector3.right*200 ; 
    				instShell.GetComponent<Rigidbody>().AddForce( dir);    
     
 
    }


   
}
