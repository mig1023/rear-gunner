using System.Collections;
using UnityEngine;

public class SW_Shell : MonoBehaviour
{
    public int damage=10;
    public Enum_Teams team;
    public ParticleSystem particle_Explosive;
    public ParticleSystem particle_WaterSpray;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
        
    }
    private void Update() {
        if(transform.position.y<0){

             //particle
                var instParticle  =   Instantiate( particle_WaterSpray, transform.position,Quaternion.Euler(0,0,0)) ; 
                instParticle.transform.localScale= new Vector3 (1+damage/50,1+damage/50,1+damage/50);
                
                Destroy(instParticle.gameObject ,3); 


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
                instParticle.transform.localScale= new Vector3 (2+damage/20,2+damage/20,2+damage/20);
                
                Destroy(instParticle.gameObject ,particle_Explosive.main.duration); 


                Destroy(gameObject);

            } 
            
        }

    }
	private IEnumerator die()
	{
		yield return new WaitForSeconds(2f);
		  Destroy(gameObject);
	}   
}
