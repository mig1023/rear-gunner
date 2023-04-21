using UnityEngine;
[System.Serializable]
public class SW_Struct_Turret
{
    public Transform BoneTurret;   
    public Transform BoneBarrelTurn;   
    public float baseRotationOfTurret;    
    public int limitTurnLeft=45;
    public int limitTurnRight=45;
    public int limitTurnUp=45;
    public int limitTurnDown=20;
    public float rateFire_State=1;
    public int distanceAttack=100;
    public int shellDamage=10;

    public float shellSpeed=800;
    public float speedTurn=2;
    public float shellSize=1;
    public bool antiAir;
    public AudioClip audioShot;   
    [System.NonSerialized]
    public float rateFire;    
    [System.NonSerialized]
    public float waitReTarget;
    [System.NonSerialized]
    public SW_Struct_Target unit_Target;
    [System.NonSerialized]
    public Transform SocketTarget;
    [System.NonSerialized]
    public Quaternion St_ToRotate_Turret;

    [System.NonSerialized]
    public float ST_ShotRecoil;
    [System.NonSerialized]
    public Quaternion BoneBarrelOriginalAxis;       
    [System.NonSerialized]
    public float BoneBarrelOriginalPos;     
    [System.NonSerialized]
    public int countBarrels;     
    [System.NonSerialized]
    public float m_angleTurretAnim_Y, m_angleBarrelAnim_X;
    [System.NonSerialized]
    public bool returnToAnim;

}
