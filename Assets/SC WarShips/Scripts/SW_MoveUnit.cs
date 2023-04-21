using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SW_MoveUnit : MonoBehaviour
{
    // Start is called before the first frame update
      NavMeshAgent nv_Agent ;
      AudioSource a_Engine ;
      SW_Struct_ForAllUnits  self;
      Animator _animator;
    void Start()
    {
        if(GetComponent<Animator>()) _animator = GetComponent<Animator>();
          else _animator = GetComponentInChildren<Animator>();
        nv_Agent = GetComponent<NavMeshAgent>();
        a_Engine = GetComponent<AudioSource>();
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits; 
 
    }

    // Update is called once per frame
    void Update()
    {
      if(a_Engine)  a_Engine.pitch=1+(nv_Agent.velocity.magnitude)/10;
        if(self.UnitType==Enum_UnitType.Tank){
          _animator.SetFloat("speed",(nv_Agent.velocity.magnitude)/10);
        }  
    }
    public void setPosMove(Vector3 pos){ //select position to move
        nv_Agent.destination=pos;


    }
}
