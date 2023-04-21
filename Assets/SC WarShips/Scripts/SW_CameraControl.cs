
using System.Collections.Generic;
using UnityEngine;


public class SW_CameraControl : MonoBehaviour {

	public Transform selectedUnitIcon;
	public Transform TargetMouse;
	public Transform trg;

	private Vector3 m_center;              // The position  center.
	float limitCamDist;
	private Plane plane;

	Transform selectedUnit;


	void Start () {
		plane= new Plane(Vector3.up, Vector3.zero);	
	 
	}
	
	// Update is called once per frame
	void Update () {

		
 

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float rayDistance;

		if (plane.Raycast(ray, out rayDistance))
			TargetMouse.position = ray.GetPoint(rayDistance);

 
		RaycastHit hitInfo;	
		ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));	

		if (plane.Raycast(ray, out rayDistance))
			m_center = ray.GetPoint(rayDistance);
		
		if ( Input.GetMouseButtonUp(0) && selectedUnit) {
				selectedUnit.GetComponent<SW_MoveUnit>().setPosMove(TargetMouse.position);	
				selectedUnitIcon.gameObject.SetActive(false);
				selectedUnit=null;

		}

		if ( Input.GetMouseButton(0) && !selectedUnit ) {

				transform.position = new Vector3(transform.position.x-((m_center.x-TargetMouse.position.x))* Time.deltaTime,transform.position.y,transform.position.z-((m_center.z-TargetMouse.position.z))* Time.deltaTime);		

		}
		if ( Input.GetMouseButtonUp(1) ) {
		         bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo,1000,-5,QueryTriggerInteraction.Ignore);
 
			if (hit && hitInfo.transform.GetComponent<SW_ForAllUnits> ()) { 
					var sel=hitInfo.transform.GetComponent<SW_ForAllUnits> ().Struct_ForAllUnits;
					if(sel.UnitType==Enum_UnitType.Boat || sel.UnitType==Enum_UnitType.Tank ){
						selectedUnit=hitInfo.transform;
						
						selectedUnitIcon.gameObject.SetActive(true);
						
						
					}
		}}





 		if(selectedUnit) selectedUnitIcon.position=new Vector3(selectedUnit.position.x,selectedUnit.position.y+1,selectedUnit.position.z);

	}
	  void LateUpdate() {
		

	}
    void OnGUI()
    {
		if(	Input.mouseScrollDelta.y!=0){
			limitCamDist+=Input.mouseScrollDelta.y;
			if(limitCamDist>11 ) limitCamDist=11;
			else if(limitCamDist<-5 ) limitCamDist=-5;
			else
			Camera.main.transform.position  += Camera.main.transform.forward*Input.mouseScrollDelta.y * 10;		
			
			if(limitCamDist<1)	
			Camera.main.fieldOfView=12-limitCamDist*3;	
		}
	

	} 

}
