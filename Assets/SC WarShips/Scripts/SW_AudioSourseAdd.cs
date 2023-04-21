
using UnityEngine;

public class SW_AudioSourseAdd: MonoBehaviour
{
 
	public AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float vol=1f, float pitch=1f,float maxDist=500f){
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		 aSource.clip = clip; // define the clip
		 aSource.volume=vol;
		 aSource.pitch=pitch;
		 aSource.spatialBlend=1;
		 aSource.rolloffMode=AudioRolloffMode.Linear;
		 aSource.dopplerLevel=0;
	     aSource.minDistance = 1;
         aSource.maxDistance = 500;	
		// set other aSource properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	} 
}
