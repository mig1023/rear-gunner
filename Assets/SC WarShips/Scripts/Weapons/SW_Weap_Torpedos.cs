using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SW_Weap_Torpedos : MonoBehaviour
{
    
    public GameObject toredo;
    public GameObject particle_TorpedoSteam;
    public int distanceAttack=60;

    public int damage=70;
    public int rateFire_State=8;
    float rateFire;   
    public Transform[] toredosSlots;
    List <SW_Struct_Target>  Targets;
    SW_AudioSourseAdd _s_PlaySoundAt;
    public AudioClip audioShot;
    SW_Struct_ForAllUnits  self;
    int speed=110;

    // Start is called before the first frame update
    void Start()
    {
        Targets=GetComponent<SW_FindTargets>().arrTargets;

        _s_PlaySoundAt = Camera.main.GetComponent<SW_AudioSourseAdd> ();
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;
    }

    // Update is called once per frame
    void Update()
    { 
    
        if(rateFire>0)rateFire-=1*Time.deltaTime;
        if(rateFire<=0)
            foreach (var target in Targets) if(target.trans )
            if(target.target_MainData.UnitType==Enum_UnitType.Boat || target.target_MainData.UnitType==Enum_UnitType.Cannon )
            { // check all Targets
            
                var targetDir= target.trans.position - transform.position;
                var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up) ; 
                float angleTurret=-transform.eulerAngles.y+targetRotation.eulerAngles.y; 
                    
                
                if(Mathf.Abs(Mathf.DeltaAngle(-angleTurret,0))<15)
                    if(distanceAttack>Vector3.Distance(target.trans.position , transform.position))
                        { //fire
                            rateFire=rateFire_State;
                            _s_PlaySoundAt.PlayClipAt(audioShot, transform.position,1,1+Random.Range(-0.15f,0.15f));
                            foreach (var item in toredosSlots)
                            {
                                //particle
                                var instParticleShot =   Instantiate( particle_TorpedoSteam, item.position,Quaternion.Euler(item.eulerAngles.x, item.eulerAngles.y,item.eulerAngles.z)) ;  
                                Destroy(instParticleShot,3f); 
                                StartCoroutine(startTorpedo(item));
     
                                
                            }
                        

                        }
                    if(target.target_MainData.Health<=0) {
                        GetComponent<SW_FindTargets>().RemoveTarget(target);  
                        break;
                    }     
            } 
    }
    
	private IEnumerator startTorpedo(Transform slotForFire){
       //toredo   
       yield return new WaitForSeconds(0.5f);  
            Quaternion Rot=Quaternion.Euler(0, transform.eulerAngles.y+Random.Range(-7f,7f),0);
			var instShell =   Instantiate( toredo, slotForFire.position, Rot) ;
            instShell.GetComponent<SW_Torpedo>().damage=damage;
            instShell.GetComponent<SW_Torpedo>().team=self.Team;
            Vector3 dir =Rot*-Vector3.back*(speed+Random.Range(-speed*0.2f,speed*0.2f))  ; 
			if(instShell.GetComponent<Rigidbody>())
				instShell.GetComponent<Rigidbody>().AddForce( dir+GetComponent<Rigidbody>().velocity); 

    } 
    
}
