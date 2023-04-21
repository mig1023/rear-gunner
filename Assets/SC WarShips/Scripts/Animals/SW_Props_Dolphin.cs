using UnityEngine;

public class SW_Props_Dolphin : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_startJump;
    float interpolationRatio;
    float localPosY;


    void Start()
    {
       localPosY=transform.localPosition.y;
       GetComponent<Animator>().Play("Idle", 0, Random.Range(0,GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length));  

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_startJump){
            
            transform.localPosition=new Vector3(transform.localPosition.x,localPosY+Mathf.Lerp(0,8,Mathf.PingPong(interpolationRatio, 1.0f)),transform.localPosition.z);
            interpolationRatio+=0.02f;
            if(interpolationRatio>2) m_startJump=false;
            
        }
    }
    void e_jump(){//
        interpolationRatio=0;
        m_startJump=true; 
        
    }

}
