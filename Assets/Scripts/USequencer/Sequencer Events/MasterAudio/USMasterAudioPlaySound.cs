using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Play Sound")]
[USequencerEvent("MasterAudio/Play Sound")]
public class USMasterAudioPlaySound : USEventBase {
	public string soundGroupName;
	public string variationName;
	public float volume = 1f;
	public float delay = 0f;
	public GameObject attachee = null;
	public bool useAffectedObject = false;
	public bool attachToGameObject = false;
	public float pitch = 1;
	
	public override void FireEvent(){
		Transform transformToUse = null;
		if(useAffectedObject && AffectedObject != null){
			transformToUse = AffectedObject.transform;
		}else if(attachee){
			transformToUse = attachee.transform;
		}

		if(transformToUse){
			MasterAudio.PlaySound3DAndForget(soundGroupName, transformToUse, attachToGameObject, volume, pitch, delay); 
		}else{
			MasterAudio.PlaySoundAndForget(soundGroupName, volume, pitch, delay); 
		}
	}

	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
