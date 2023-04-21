using UnityEngine;
using System.Collections;
public class SW_Props_DolphinGroupPlaySound : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip snd_Audio;
    SW_AudioSourseAdd _s_PlaySoundAt;
    void Start()
    {

        _s_PlaySoundAt = Camera.main.GetComponent<SW_AudioSourseAdd> ();
        StartCoroutine(P_Sound());
    }

    // Update is called once per frame


	private IEnumerator P_Sound()
	{
 
		yield return new WaitForSeconds(Random.Range(15f,100f));
            //play sounds
            _s_PlaySoundAt.PlayClipAt(snd_Audio, transform.position,1,1+Random.Range(-0.15f,0.15f));
            StartCoroutine(P_Sound());
         
	}  
}
