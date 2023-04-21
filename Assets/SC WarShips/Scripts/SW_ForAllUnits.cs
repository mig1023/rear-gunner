
using UnityEngine;

public class SW_ForAllUnits : MonoBehaviour
{
 
    public SW_Struct_ForAllUnits Struct_ForAllUnits;
    Animator _animator;
    void Start()
    {
        Struct_ForAllUnits.Health=Struct_ForAllUnits.HealthMax;
        if(GetComponent<Animator>()) _animator = GetComponent<Animator>();
          else if(GetComponentInChildren<Animator>()) _animator = GetComponentInChildren<Animator>();
    }

 
 
    public void detectHit(int damage){
        if(Struct_ForAllUnits.Health<=0) return;    
        Struct_ForAllUnits.Health-=damage;
        if(Struct_ForAllUnits.Health<=0){
    

                int a=Random.Range(0,Struct_ForAllUnits.slots_ForExplosionsAfterDie.Length);
                float scale=1;
                if(Struct_ForAllUnits.UnitType==Enum_UnitType.Boat) scale=1+GetComponent<Rigidbody>().mass/10;                
                        
                             
                var instParticle  =   Instantiate( Struct_ForAllUnits.ParticleFlameAfterDie,Struct_ForAllUnits.slots_ForExplosionsAfterDie[a].position,Quaternion.Euler(0,0,0)) ; 
                instParticle.transform.localScale= new Vector3 (scale,scale,scale);   
                Destroy(instParticle.gameObject ,Struct_ForAllUnits.ParticleFlameAfterDie.main.duration);  

               if(Struct_ForAllUnits.UnitType==Enum_UnitType.Aircraft){//only for A
                    _animator.SetBool("Dead",true);
               }
               if(Struct_ForAllUnits.UnitType==Enum_UnitType.Boat || Struct_ForAllUnits.UnitType==Enum_UnitType.Tank){//only for boat and Tank
                    _animator.SetInteger("dead",Random.Range(1,3));
                    if(Struct_ForAllUnits.UnitType==Enum_UnitType.Boat){
                        instParticle  =   Instantiate( Struct_ForAllUnits.ParticleWaterSplashAfterDie,transform.position,Quaternion.Euler(0,transform.eulerAngles.y,0)) ;  
                        scale=1+GetComponent<Rigidbody>().mass/10;                
                        instParticle.transform.localScale= new Vector3 (scale,scale,scale);
                        Destroy(instParticle.gameObject ,11f);      
                    }
                }

  
                if(Struct_ForAllUnits.UnitType==Enum_UnitType.Building || Struct_ForAllUnits.UnitType==Enum_UnitType.Cannon) {//  only for Building and Cannon  
                    instParticle  =   Instantiate( Struct_ForAllUnits.ParticleExplosionsAfterDie, Struct_ForAllUnits.slots_ForExplosionsAfterDie[0].position,Quaternion.Euler(0,0,0)) ; 
                    scale=2;                
                    if(Struct_ForAllUnits.UnitType==Enum_UnitType.Building)  scale=5;                
                    instParticle.transform.localScale= new Vector3 (scale,scale,scale);
                    
                    Destroy(instParticle.gameObject ,Struct_ForAllUnits.ParticleExplosionsAfterDie.main.duration); 
                    Destroy(gameObject);        
                }
        
            

    
        }
    }
    void showParticleExplosion(int indexArray){
                if(Struct_ForAllUnits.UnitType==Enum_UnitType.Boat || Struct_ForAllUnits.UnitType==Enum_UnitType.Tank) _animator.SetInteger("dead",0);
            //particle
                var instParticle  =   Instantiate( Struct_ForAllUnits.ParticleExplosionsAfterDie, Struct_ForAllUnits.slots_ForExplosionsAfterDie[indexArray].position,Quaternion.Euler(0,0,0)) ; 
                float scale=3 ;                
                if(GetComponent<Rigidbody>())  scale+=GetComponent<Rigidbody>().mass/10;                
                instParticle.transform.localScale= new Vector3 (scale,scale,scale);
                
                Destroy(instParticle.gameObject ,Struct_ForAllUnits.ParticleExplosionsAfterDie.main.duration); 

            

    }
    public void event_die(){
        Destroy(gameObject);
    }    
}
