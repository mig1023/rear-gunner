using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Props_AnimalsMove : MonoBehaviour
{
 

    public float m_Speed=4;
    public float m_SpeedRotate=2;
 
    Vector3 m_TargetPos;

    protected float m_angleAdd;
    float m_toRotate;
    protected virtual void Start()
    {
        MainStart();
    }
    protected void MainStart()
    {
        m_TargetPos=transform.position;
        m_Speed = Random.Range(m_Speed-1,m_Speed+1);  
       
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        MainUpdate();
    }
    protected void MainUpdate()
    {
        

        transform.position += transform.forward *Time.deltaTime* m_Speed;
        Vector3 targetDir = m_TargetPos - transform.position;
         
        
        if(Vector3.Distance(m_TargetPos, transform.position)<25){
            f_randomPos();  
            targetDir = m_TargetPos - transform.position;
            m_toRotate=GetMinAngle( transform.forward,targetDir);
            if(m_toRotate>0) m_angleAdd=1f;
            if(m_toRotate<0) m_angleAdd=-1f;
        }
        

        float ang=GetMinAngle( transform.forward,targetDir);
         if(Mathf.Abs(ang)>Mathf.Abs(m_toRotate/2)) {
           if(Mathf.Abs(m_angleAdd)<20) m_angleAdd=m_angleAdd*1.01f;
        }
            else if(Mathf.Abs(ang)<20)  m_angleAdd=ang*0.99f ;


        transform.localRotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y+(m_angleAdd*m_SpeedRotate*Time.deltaTime) ,transform.eulerAngles.z);//
        
    }


    void f_randomPos(){
        m_TargetPos=new Vector3(Random.Range(-200.0f,200.0f),transform.position.y,Random.Range(-245f,20));
      
    }

    float GetMinAngle(Vector3 transform_forward, Vector3 targetDir)
        {
            var angleB = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            var angleA = Mathf.Atan2(transform_forward.x, transform_forward.z) * Mathf.Rad2Deg;
            return Mathf.DeltaAngle(angleA, angleB);
        }  



  
}
