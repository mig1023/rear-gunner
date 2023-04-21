using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]


public class doEditor : MonoBehaviour
{
    public bool _start;
    public GameObject gm;
    bool old_start;
    public int h,w;
 
 
 
    void nnnnnnUpdate()
    {
        if(_start!=old_start ){
           old_start=_start;
 
           if(_start ) {
              List<GameObject> myList = new List<GameObject>();  
               foreach (Transform  child in transform){
                   //print(child.name);
                   if (child.name=="Tile(Clone)"){ 
                        myList.Add(child.gameObject);
 
                   }
               }
                for (int i = 0; i < myList.Count; i++)
                    DestroyImmediate(myList[i]);
               
               for (int i = 0; i < w; i++)
                for (int i2 = 0; i2 < h; i2++)
                if(i>0 || i2>0)
                {
                    var unit=Instantiate(gm, new Vector3(0, 0, 0), Quaternion.identity);
                    unit.transform.SetParent(transform );
                    unit.transform.localPosition=new Vector3(i*50,0,i2*50);                    
                }
 

               
           }
            ;
        }
    }
}