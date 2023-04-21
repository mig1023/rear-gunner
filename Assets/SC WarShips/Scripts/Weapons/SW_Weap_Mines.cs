using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Weap_Mines : MonoBehaviour
{
    public GameObject g_mine;
    public Transform  slotDropMine;
    public int  damage;
    public int  count;
    public bool  autoDropMine;
    SW_Struct_ForAllUnits  self;
    Animator _Animator;
    Vector3 lastPosition;
    public ParticleSystem particle_WaterSpray;
    public Transform WaterSprayPosition;
    void Start()
    {
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;
        _Animator=GetComponent<Animator>();
        if(autoDropMine) StartCoroutine(dropMineTimer());
    }
    void e_drop_Mine(){
            if(count==0)  return; 
            count--;
            var instShell = Instantiate( g_mine, slotDropMine.position, Quaternion.Euler(Random.Range(0,360), Random.Range(0,360),Random.Range(0,360))) ;
            instShell.GetComponent<SW_Mine>().damage=damage;
            instShell.GetComponent<SW_Mine>().team=self.Team;  
            _Animator.SetLayerWeight(1,0);      
 
    }
	public void dropMineWaterSpray()
	{
             //particle
                var instParticle  =   Instantiate( particle_WaterSpray, WaterSprayPosition.position,Quaternion.Euler(0,0,0)) ; 
                
                
                Destroy(instParticle.gameObject ,3); 
    }
	private IEnumerator dropMineTimer()
	{
		yield return new WaitForSeconds(20f);
                
                if(count>0){
                    startAnimationDropMine();
                    StartCoroutine(dropMineTimer());   
                }
                    
	}  

    void startAnimationDropMine(){
            if(Vector3.Distance(lastPosition,slotDropMine.position)>8){ 
                lastPosition=slotDropMine.position;		    
                _Animator.SetTrigger("mine");
                _Animator.SetLayerWeight(1,1);
             
            }

    }    
}
