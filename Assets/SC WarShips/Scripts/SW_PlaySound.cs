
using UnityEngine;

public class SW_PlaySound : MonoBehaviour
{
    public AudioClip[] sounds; 

    void Start()
    {
        float pitch=(1.3f-(transform.localScale.x/3))+Random.Range(-0.15f,0.15f);
        if(pitch<0.7f)pitch=0.7f;
        Camera.main.GetComponent<SW_AudioSourseAdd> ().PlayClipAt(sounds[Random.Range(0,sounds.Length)], transform.position,0.5f+(transform.localScale.x/2),pitch);
    }


 
}
