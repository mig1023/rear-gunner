using System.Collections;
using UnityEngine;

public class SW_Props_Albatros : SW_Props_AnimalsMove
{
 
    public AudioClip snd_Audio;


    SW_AudioSourseAdd _s_PlaySoundAt;
    float m_toTurn;
     protected override void Start()
    {

 
        MainStart();
        m_Speed = Random.Range(8.0f,10.0f);  
              
        _s_PlaySoundAt = Camera.main.GetComponent<SW_AudioSourseAdd> ();

        StartCoroutine(P_Sound()); 
        GetComponent<Animator>().Play("Idle", 0, Random.Range(0,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));    
    
    }

    // Update is called once per frame
    protected override void Update()
    {
            //turn bofy for rotate
        MainUpdate();
        if(m_toTurn>m_angleAdd)m_toTurn-=0.05f; else   m_toTurn+=0.05f; 
        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y ,-m_toTurn);//
       
    }



	private IEnumerator P_Sound()
	{
 
		yield return new WaitForSeconds(Random.Range(15f,100f));
            //play sounds
            _s_PlaySoundAt.PlayClipAt(snd_Audio, transform.position,1,1+Random.Range(-0.15f,0.15f));
            StartCoroutine(P_Sound());
         
	}   
  
}
