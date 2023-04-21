using UnityEngine;
[System.Serializable]
public class SW_Struct_ForAllUnits
{
    public Enum_Teams Team;
    public Enum_UnitType UnitType;
    public int HealthMax=100;
    public Transform[] slots_TagetForEnemy;
    public Transform[] slots_ForExplosionsAfterDie;    
    public ParticleSystem ParticleExplosionsAfterDie; 
    public ParticleSystem ParticleFlameAfterDie; 
    public ParticleSystem ParticleWaterSplashAfterDie; 
 
    [System.NonSerialized]
    public int Health;
    

}
