using System.Collections;
using UnityEngine;

public class SW_Torpedo : MonoBehaviour
{
    public int damage=80;
    public Enum_Teams team;
    bool inWater;
    public ParticleSystem particle_Explosive;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dieTime());
        rb=GetComponent<Rigidbody>();
    }
    void Update()
    { 
       
        if(transform.position.y<-1 ){
            rb.velocity=new Vector3(rb.velocity.x, -transform.position.y,rb.velocity.z);      
            if(!inWater){ 
                inWater=true;
  
               GetComponent<TrailRenderer>().enabled=true;
            }
  
        }
 
    }
	void OnTriggerEnter(Collider col){

        if (!col.isTrigger && col.GetComponent<SW_ForAllUnits> () ){
            var ob=col.GetComponent<SW_ForAllUnits> ();
            if (ob.Struct_ForAllUnits.Team!=team )
            {
                ob.detectHit(damage);
                die();

            } 
            
        }

    }
	private IEnumerator dieTime()
	{
		yield return new WaitForSeconds(5f);
            die();
	}   
    void die(){

            //particle
                var instParticle  =   Instantiate( particle_Explosive, new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.Euler(0,0,0)) ; 
                instParticle.transform.localScale= new Vector3 (4,4,4);
                Destroy(instParticle.gameObject ,particle_Explosive.main.duration);         
		        Destroy(gameObject);
    }

}
