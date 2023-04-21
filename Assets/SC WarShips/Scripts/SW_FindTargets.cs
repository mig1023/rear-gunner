using System.Collections.Generic;
using UnityEngine;

public class SW_FindTargets : MonoBehaviour
{
    SW_Struct_ForAllUnits  self;
    public List <SW_Struct_Target> arrTargets =new List<SW_Struct_Target>();
 
    // Start is called before the first frame update
    void Start()
    {
       self= GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;
    }

    // Update is called once per frame

	void OnTriggerEnter(Collider col){
       
        if (!col.isTrigger && col.GetComponent<SW_ForAllUnits> () )
            if (col.GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits.Team!=self.Team )
            {
               
                bool addTarget=true;
                foreach (var item in arrTargets)
                    if(col.transform==item.trans)
                        {
                            addTarget=false;
                        }
                if(addTarget) {
 
                    SW_Struct_Target  trg=new SW_Struct_Target();
                    trg.trans=col.transform;
                    trg.target_MainData=col.GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;

                    arrTargets.Add(trg);
                 
                }       

            } 
            
 

    }

	void OnTriggerExit(Collider col) {
         if (col.GetComponent<SW_ForAllUnits> () )
            if (col.GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits.Team!=self.Team )
            {
                 
                 
                foreach (var item in arrTargets)
                    if(col.transform==item.trans)
                        {
                           RemoveTarget(item);
                           break;
                        }
     

            }      
 
	}
    public void RemoveTarget(SW_Struct_Target item){
        arrTargets.Remove(item);

    }
   
}
