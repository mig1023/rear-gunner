using System.Collections;
using UnityEngine;

public class SW_Bomb : MonoBehaviour
{
    public int damage=220;
    public Enum_Teams team;
    public ParticleSystem particle_Explosive;
    public ParticleSystem particle_WaterSpray;

    private void Update() {
        if(transform.position.y<0){

             //particle
                var instParticle  =   Instantiate( particle_WaterSpray, transform.position,Quaternion.Euler(0,0,0)) ; 
                instParticle.transform.localScale= new Vector3 (3,3,3);
                
                Destroy(instParticle.gameObject ,3); 
            //particle
                instParticle  =   Instantiate( particle_Explosive, transform.position,Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360))) ; 
                instParticle.transform.localScale= new Vector3 (5,5,5);
                
                Destroy(instParticle.gameObject ,particle_Explosive.main.duration); 


                Destroy(gameObject);  



        }


    
     
    }
	void OnTriggerEnter(Collider col){

        if (!col.isTrigger && col.GetComponent<SW_ForAllUnits> () ){
            var ob=col.GetComponent<SW_ForAllUnits> ();
            if (ob.Struct_ForAllUnits.Team!=team )
            {
                ob.detectHit(damage);
            //particle
                var instParticle  =   Instantiate( particle_Explosive, transform.position,Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360))) ; 
                instParticle.transform.localScale= new Vector3 (5,5,5);
                
                Destroy(instParticle.gameObject ,particle_Explosive.main.duration); 


                Destroy(gameObject);

            } 
            
        }

    }

}
