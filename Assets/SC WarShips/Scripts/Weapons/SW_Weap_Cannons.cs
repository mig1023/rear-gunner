using System.Collections.Generic;
using UnityEngine;

public class SW_Weap_Cannons : MonoBehaviour
{
    SW_Struct_ForAllUnits  self;
    public GameObject shell;
    public GameObject particle_Shot;    
    public SW_Struct_Turret[] Turrets;

    List <SW_Struct_Target>  Targets;

    SW_AudioSourseAdd _s_PlaySoundAt;
    
    Quaternion OriginalRot;
    void Start()
    {
      
        Targets=GetComponent<SW_FindTargets>().arrTargets;
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits; 
        _s_PlaySoundAt = Camera.main.GetComponent<SW_AudioSourseAdd> ();
        foreach (var turret in Turrets){
            turret.BoneBarrelOriginalAxis=turret.BoneBarrelTurn.localRotation ;
            if(self.UnitType==Enum_UnitType.Aircraft) turret.BoneBarrelOriginalPos=turret.BoneBarrelTurn.localPosition.y;
            else{
                int i=0;
                if(self.UnitType==Enum_UnitType.Tank) i=1;
                turret.BoneBarrelOriginalPos=turret.BoneBarrelTurn.GetChild(i).localPosition.y;
            }
        }
    }
    void f_BarrelRecoil(SW_Struct_Turret turret, bool doShot=false){
            //barrel recoil
            if(turret.ST_ShotRecoil>0){ 
                turret.ST_ShotRecoil-=0.02f;
                if(self.UnitType==Enum_UnitType.Aircraft){
 
                    if(doShot) f_Fire(turret,turret.BoneTurret);
                     
                }
                if(self.UnitType==Enum_UnitType.Tank){
                    var boneBarrelRecoil= turret.BoneBarrelTurn.GetChild(1);
                    if(doShot) f_Fire(turret,boneBarrelRecoil.GetChild(0).GetChild(0));
                     boneBarrelRecoil.localPosition=new Vector3(boneBarrelRecoil.localPosition.x, turret.BoneBarrelOriginalPos+turret.ST_ShotRecoil,boneBarrelRecoil.localPosition.z);
                    
                }
                if(self.UnitType==Enum_UnitType.Boat || self.UnitType==Enum_UnitType.Cannon) 
                for (int i = 0; i < turret.BoneBarrelTurn.childCount; i++){
                        var boneBarrelRecoil=turret.BoneBarrelTurn.GetChild(i);
                        if(boneBarrelRecoil) {
                            if(doShot) f_Fire(turret,boneBarrelRecoil.GetChild(0));
                            if(boneBarrelRecoil.localPosition.y>0) boneBarrelRecoil.localPosition=new Vector3(boneBarrelRecoil.localPosition.x, turret.BoneBarrelOriginalPos-turret.ST_ShotRecoil,boneBarrelRecoil.localPosition.z);
                        } else break;
                }
            } 

    }
    // Update after animation
    void LateUpdate()
    {
        if(self.Health<=0) return;
        foreach (var turret in Turrets){ // check all turrets
            
            if(turret.unit_Target!=null && turret.SocketTarget)       {

                bool canFire=false;
                //turret aim to target
                float angle=f_getAngleTarget(turret.SocketTarget.transform.position - turret.BoneTurret.position,turret.baseRotationOfTurret);
                angle=Mathf.Clamp(angle,-turret.limitTurnLeft,turret.limitTurnRight);//limit angle
                float speedTurn=(angle-turret.m_angleTurretAnim_Y)/turret.speedTurn;    
                if(speedTurn>0 && speedTurn<0.1f) speedTurn=0.1f;if(speedTurn<0 && speedTurn>-0.1f) speedTurn=-0.1f;
                turret.m_angleTurretAnim_Y+=speedTurn*100*Time.deltaTime;
                if (Mathf.Abs(Mathf.DeltaAngle(turret.m_angleTurretAnim_Y,angle))<5 ) canFire=true;//in the line of fire
                if(self.UnitType==Enum_UnitType.Tank) turret.BoneTurret.localRotation=Quaternion.Euler(-90,turret.m_angleTurretAnim_Y ,0); 

                else turret.BoneTurret.localRotation=Quaternion.Euler(turret.BoneTurret.eulerAngles.x,turret.m_angleTurretAnim_Y ,turret.BoneTurret.eulerAngles.z); 
                if(self.UnitType==Enum_UnitType.Aircraft) turret.BoneTurret.localRotation=Quaternion.Euler(0,0 ,90); 

                //barrel aim to target
                var targetRotation = Quaternion.LookRotation(turret.SocketTarget.transform.position - turret.BoneBarrelTurn.position, Vector3.forward) ; 
                float angleTurret=targetRotation.eulerAngles.x; 
                angle= Mathf.DeltaAngle(angleTurret,0);//-180,+180
                //angle=Mathf.Clamp(angle,-turret.limitTurnDown,turret.limitTurnUp);//limit angle
                speedTurn=(angle-turret.m_angleBarrelAnim_X)/turret.speedTurn;  
                if(speedTurn>0 && speedTurn<0.1f) speedTurn=0.1f;if(speedTurn<0 && speedTurn>-0.1f) speedTurn=-0.1f;
                turret.m_angleBarrelAnim_X+=speedTurn*100*Time.deltaTime;          
                if (canFire && Mathf.Abs(Mathf.DeltaAngle(turret.m_angleBarrelAnim_X,angle))<5 ) canFire=true; //in the line of fire  
                
                turret.BoneBarrelTurn.localRotation=Quaternion.Euler(turret.BoneBarrelOriginalAxis.eulerAngles.x-turret.m_angleBarrelAnim_X,turret.BoneBarrelOriginalAxis.eulerAngles.y,turret.BoneBarrelOriginalAxis.eulerAngles.z); 
                if(self.UnitType==Enum_UnitType.Aircraft) turret.BoneBarrelTurn.localRotation=Quaternion.Euler(0,0 ,90); 
                //check fire
                bool doShot=false;
                if(turret.distanceAttack>Vector3.Distance(turret.unit_Target.trans.position , transform.position)){
                    
                    if(turret.rateFire>0) turret.rateFire-=1*Time.deltaTime;
                    else if(canFire){ //Shot!!!!!
                        doShot=true;
                        turret.rateFire=turret.rateFire_State+Random.Range(-turret.rateFire_State/10,turret.rateFire_State/10);    
                        turret.ST_ShotRecoil=0.6f;
                         
                        _s_PlaySoundAt.PlayClipAt(turret.audioShot, turret.BoneTurret.position,1,1+Random.Range(-0.15f,0.15f));
                    }   

                }   
                f_BarrelRecoil(turret,doShot);

                



                if(turret.unit_Target.target_MainData.Health<=0) {
                    turret.waitReTarget=0;
                    GetComponent<SW_FindTargets>().RemoveTarget(turret.unit_Target);  
                    turret.unit_Target=null;
                }  
          
            } else turret.waitReTarget=0;

             f_BarrelRecoil(turret);    
            
            if(turret.waitReTarget>0){//wait retarget time
                turret.waitReTarget-=Time.deltaTime; 
                
            } else { //start find target
                Transform getTarget=null;
                float dist=77777;  
                
                foreach (var target in Targets) if(target.trans )
                    if(target.target_MainData.UnitType!=Enum_UnitType.Aircraft || turret.antiAir  )
                    if(dist>Vector3.Distance(transform.position,target.trans.position))
                    {

                        dist=Vector3.Distance(transform.position,target.trans.position);
                    
                        var slotForAttack=target.trans.GetComponent<SW_ForAllUnits>().Struct_ForAllUnits.slots_TagetForEnemy ;
                        if(slotForAttack.Length>0){
                            
                            List<Transform> sel = new List<Transform>();
                            foreach (var item in slotForAttack){
                                 
                                    float angleTurret=f_getAngleTarget(item.position - turret.BoneTurret.position,turret.baseRotationOfTurret);
                                    if(angleTurret<=turret.limitTurnRight && angleTurret>=-turret.limitTurnLeft){
                                        angleTurret=f_getAngleTarget(item.position - turret.BoneTurret.position,0,Vector3.right);
                                
                                        if(angleTurret<=turret.limitTurnDown && angleTurret>=-turret.limitTurnUp) sel.Add(item) ; 

                                    } 
                                
                            }
    
                            if (sel.Count>0)  getTarget=sel[Random.Range(0,sel.Count)]; 

                          
                        } else {
                            
                            float angleTurret=f_getAngleTarget(target.trans.position - turret.BoneTurret.position,turret.baseRotationOfTurret);
                            if(angleTurret<=turret.limitTurnRight && angleTurret>=-turret.limitTurnLeft) getTarget=target.trans;
                        }

                          
                        if(getTarget){
                            
                            turret.unit_Target=target;
                            turret.waitReTarget=4f;
                            if(self.UnitType==Enum_UnitType.Aircraft) turret.waitReTarget=0.5f;
                            turret.returnToAnim=true;
                            
                        }    
    

                    } 

                
                if( !getTarget && self.UnitType!=Enum_UnitType.Aircraft)  //return rotate to anim
                    if(turret.returnToAnim){
                        //return rotate turret
                    float angleAnim_Y=-Mathf.DeltaAngle(turret.BoneTurret.localEulerAngles.y,0);
                    float speedTurn=(angleAnim_Y-turret.m_angleTurretAnim_Y)/turret.speedTurn;    
                    if(speedTurn>0 && speedTurn<0.1f) speedTurn=0.1f;if(speedTurn<0 && speedTurn>-0.1f) speedTurn=-0.1f;
                    turret.m_angleTurretAnim_Y+=speedTurn*100*Time.deltaTime;
                     
                     if(self.UnitType==Enum_UnitType.Tank) turret.BoneTurret.localRotation=Quaternion.Euler(-90,turret.m_angleTurretAnim_Y ,0);
                    else turret.BoneTurret.localRotation=Quaternion.Euler(turret.BoneTurret.eulerAngles.x,turret.m_angleTurretAnim_Y ,turret.BoneTurret.eulerAngles.z); 

                        //return rotate barrel
                     
                    
                    float angleAnim_X= Mathf.DeltaAngle(turret.BoneBarrelTurn.eulerAngles.x-90,0);
                    if(self.UnitType==Enum_UnitType.Tank)  angleAnim_X=0;
                    speedTurn=(angleAnim_X-turret.m_angleBarrelAnim_X)/turret.speedTurn;  
                    if(speedTurn>0 && speedTurn<0.1f) speedTurn=0.1f;if(speedTurn<0 && speedTurn>-0.1f) speedTurn=-0.1f;
                    turret.m_angleBarrelAnim_X+=speedTurn*100*Time.deltaTime;                        

                    turret.BoneBarrelTurn.localRotation=Quaternion.Euler(turret.BoneBarrelOriginalAxis.eulerAngles.x-turret.m_angleBarrelAnim_X,turret.BoneBarrelOriginalAxis.eulerAngles.y,turret.BoneBarrelOriginalAxis.eulerAngles.z); 


                    if (Mathf.Abs(Mathf.DeltaAngle(turret.m_angleTurretAnim_Y,angleAnim_Y))<1 && Mathf.Abs(Mathf.DeltaAngle(turret.m_angleBarrelAnim_X,angleAnim_X))<1) turret.returnToAnim=false;
                }  else{

                  //  turret.m_angleTurretAnim_Y=Mathf.DeltaAngle(transform.eulerAngles.y-turret.BoneTurret.eulerAngles.y,0);
                    turret.m_angleTurretAnim_Y=-Mathf.DeltaAngle(turret.BoneTurret.localEulerAngles.y,0);

                    turret.m_angleBarrelAnim_X=Mathf.DeltaAngle(turret.BoneBarrelTurn.eulerAngles.x-90,0);
                    if(self.UnitType==Enum_UnitType.Tank)  turret.m_angleBarrelAnim_X=0;
                } 

                turret.SocketTarget=getTarget;
            
            }  
        }
       
    }


    float f_getAngleTarget(Vector3 targetDir,float  baseRotationOfTurret=0,Vector3 dir=new Vector3() ){ //find angle 

                if(dir==new Vector3(0,0,0)) dir=Vector3.up;
                var targetRotation = Quaternion.LookRotation(targetDir, dir) ; 
                float angleTurret=0;
                if(dir==Vector3.up)  angleTurret=-transform.eulerAngles.y+targetRotation.eulerAngles.y; 
                if(dir==Vector3.right)  angleTurret=-transform.eulerAngles.x+targetRotation.eulerAngles.x; 

                return Mathf.DeltaAngle(-angleTurret, baseRotationOfTurret);//-180,+180

    }

    void f_Fire(SW_Struct_Turret turret,Transform GunEnd){
           
 
       //shell    
            Quaternion ShellRot=Quaternion.Euler(GunEnd.eulerAngles.x+Random.Range(-3f,3f), GunEnd.eulerAngles.y+Random.Range(-3f,3f),GunEnd.eulerAngles.z+90+Random.Range(-3f,3f));
			var instShell =   Instantiate( shell, GunEnd.position, ShellRot) ; 
            instShell.transform.localScale= new Vector3 (turret.shellSize,turret.shellSize,turret.shellSize);
            int dmg=turret.shellDamage;
            if(turret.unit_Target.trans && turret.unit_Target.target_MainData.UnitType==Enum_UnitType.Aircraft) dmg*=10;
            instShell.GetComponent<SW_Shell>().damage=dmg;
            instShell.GetComponent<SW_Shell>().team=self.Team;

            Vector3 dir =ShellRot*Vector3.up*(turret.shellSpeed+Random.Range(-turret.shellSpeed*0.2f,turret.shellSpeed*0.2f))  ; 
			if(instShell.GetComponent<Rigidbody>()) 
				instShell.GetComponent<Rigidbody>().AddForce( dir); 
 
 

         //particle
			var instParticleShot =   Instantiate( particle_Shot, GunEnd.position,Quaternion.Euler(GunEnd.eulerAngles.x, GunEnd.eulerAngles.y,GunEnd.eulerAngles.z)) ; 
            instParticleShot.transform.localScale= new Vector3 (turret.shellSize,turret.shellSize,turret.shellSize);
            Destroy(instParticleShot,3f); 


    }
}
