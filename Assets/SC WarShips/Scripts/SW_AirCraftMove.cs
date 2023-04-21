using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_AirCraftMove : MonoBehaviour
{
 

    float m_Speed=4;

    public Transform ListOfAirCraftPosTargets;
    public AudioClip  soundsAttack; 
 
    int indexArr=0;
    bool startFly;
    Animator _animator;
    protected float m_angleAdd;
    List <SW_Struct_Target>  Targets;
    Vector3 FixDirection;
    float m_toTurn;
    bool dropBomb;
    bool DownToWater,startDown;
    List<Vector3> arrTargetPos = new List<Vector3>();
    int timerStartWait;
    int TargetFindedAct=0;
    Transform selTargetFinded;
    public ParticleSystem particle_WaterSpray;
 
    SW_Struct_ForAllUnits  self;
    protected virtual void Start()
    {
        Targets=GetComponent<SW_FindTargets>().arrTargets;
        _animator=transform.GetComponent<Animator>();
        foreach (Transform  item  in ListOfAirCraftPosTargets )
        {
            arrTargetPos.Add(item.position);
        }
        timerStartWait= Random.Range(10,40);
        StartCoroutine(timerStart());
        self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;
     
    }

    // Update is called once per frame
      void Update()
    {
     //   if(Input.GetKeyDown("1") && self.Team==Enum_Teams.TeamBlue) startFly=true; 
     //   if(Input.GetKeyDown("2") && self.Team==Enum_Teams.TeamRed) startFly=true; 
    
        if(self.Health>0){
            if(startFly) { 
                if(Targets.Count>0 && Targets[0].trans && TargetFindedAct==0) {
                    TargetFindedAct=1;
                    Camera.main.GetComponent<SW_AudioSourseAdd> ().PlayClipAt(soundsAttack, transform.position,0.3f);
                    selTargetFinded = Targets[0].trans;
                }  
                
                    
                    // Targets[0]
        
                MainUpdate();

            }
        } else if(indexArr>0) DieUpdate();
    }
    void DieUpdate(){
         if(!startDown)Camera.main.GetComponent<SW_AudioSourseAdd> ().PlayClipAt(soundsAttack, transform.position,0.1f,0.5f);
         startDown=true;
         transform.position += transform.forward *Time.deltaTime* m_Speed;
         transform.rotation = Quaternion.Euler(transform.eulerAngles.x+0.1f,transform.eulerAngles.y,transform.eulerAngles.z);
         if(transform.position.y<0 && !DownToWater){
             DownToWater=true;
             for (int i = 0; i < 5; i++)
             {
                var instParticle  =   Instantiate( particle_WaterSpray, new Vector3(transform.position.x+Random.Range(-5,5),0,transform.position.z+Random.Range(-5,5)),Quaternion.Euler(0,0,0)) ; 
                instParticle.transform.localScale= new Vector3 (3,3,3);
                
                Destroy(instParticle.gameObject ,3);                  
             }

         }

    }
      void MainUpdate()
    {
        
        if(indexArr<5 && m_Speed<40) m_Speed+=15f*Time.deltaTime;
        if(indexArr>11 && m_Speed>4) m_Speed-=5f*Time.deltaTime;
        transform.position += transform.forward *Time.deltaTime* m_Speed;
        Vector3 trgPos=arrTargetPos[indexArr];
        if (TargetFindedAct==1) {
            if(selTargetFinded) trgPos=selTargetFinded.position; 
            if(Vector3.Distance(trgPos, transform.position)<50) {
                trgPos=new Vector3(trgPos.x,28,trgPos.z);
                if(!dropBomb){
                    dropBomb=true;
                    GetComponent<SW_Weap_Bomb>().f_Drop_Bomb();
                }    
            }
            if(Vector3.Distance(trgPos, transform.position)<10) { TargetFindedAct=2;indexArr+=2;}
        }  

        Vector3 targetDir = trgPos - transform.position;
     //    if (TargetFindedAct==1) targetDir=targetDir*0.3f;
        float singleStep = ((30-m_Speed/2) * Time.deltaTime)/10;
        

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, singleStep, 0.0f);
        float angle=f_getAngleTarget(trgPos - transform.position );
         


        transform.rotation = Quaternion.LookRotation(newDirection);
    
        
        if(Vector3.Distance(arrTargetPos[indexArr], transform.position)<10){
            indexArr++;
            f_Act(indexArr);  
  
        }
        
       
        if(indexArr<4 || indexArr> 13) angle=0;
        if(m_toTurn>angle && m_toTurn>-30) m_toTurn-=10f*Time.deltaTime;
         else  if( m_toTurn<30) m_toTurn+=10f*Time.deltaTime; 
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y ,-m_toTurn*3);//       
    }

    float f_getAngleTarget(Vector3 targetDir,float  baseRotationOfTurret=0){ //find angle 
                
                var targetRotation = Quaternion.LookRotation(targetDir, Vector3.up) ; 
                float angleTurret=-transform.eulerAngles.y+targetRotation.eulerAngles.y; 
                return Mathf.DeltaAngle(-angleTurret, baseRotationOfTurret);//-180,+180

    }

    void f_Act(int act){
         if(act==1) transform.GetComponent<AudioSource>().pitch=1;
           transform.GetComponent<AudioSource>().pitch=1;
         _animator.SetInteger("act",act);
        if(indexArr==arrTargetPos.Count){
            indexArr=0;
            m_Speed=4;
            timerStartWait=Random.Range(40,140);
            StartCoroutine(timerStart());    
            startFly=false;   
            transform.GetComponent<AudioSource>().pitch=0.49f;    
        }

    }

    float GetMinAngle(Vector3 transform_forward, Vector3 targetDir)
        {
            var angleB = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            var angleA = Mathf.Atan2(transform_forward.x, transform_forward.z) * Mathf.Rad2Deg;
            return Mathf.DeltaAngle(angleA, angleB);
        }  

 
	private IEnumerator timerStart()
	{
		yield return new WaitForSeconds(timerStartWait);
		   startFly=true;
	} 
  
}
