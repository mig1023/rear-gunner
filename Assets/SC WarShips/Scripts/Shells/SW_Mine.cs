using System.Collections;
using UnityEngine;

public class SW_Mine : MonoBehaviour
{
    public int damage=90;
    public Enum_Teams team;
    public ParticleSystem particle_Explosive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

	void OnTriggerEnter(Collider col){

        if (!col.isTrigger && col.GetComponent<SW_ForAllUnits> () ){
            var ob=col.GetComponent<SW_ForAllUnits> ();
            if (ob.Struct_ForAllUnits.Team!=team )
            {
                ob.detectHit(damage);
            //particle
                var instParticle  =   Instantiate( particle_Explosive, transform.position,Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360))) ; 
                instParticle.transform.localScale=  new Vector3 (4,4,4);
                
                Destroy(instParticle.gameObject ,particle_Explosive.main.duration); 


                Destroy(gameObject);

            } 
            
        }

    }
	private IEnumerator die()
	{
		yield return new WaitForSeconds(180f);
		  Destroy(gameObject);
	}   
}
