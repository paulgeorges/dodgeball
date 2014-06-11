using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Group Fade")]
[USequencerEvent("MasterAudio/Group Fade")]
public class USMasterAudioGroupFade : USEventBase {
	public bool allGroups;	
	public string soundGroupName;
	public float targetVolume;
	public float fadeTime;
	
	public override void FireEvent(){
		if (!allGroups && string.IsNullOrEmpty(soundGroupName)) {
			Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
			return;
		}

		if (allGroups) {
			var groupNames = MasterAudio.RuntimeSoundGroupNames;
			for (var i = 0; i < groupNames.Count; i++) {
				MasterAudio.FadeSoundGroupToVolume(groupNames[i], targetVolume, fadeTime);
			}
		} else {
			MasterAudio.FadeSoundGroupToVolume(soundGroupName, targetVolume, fadeTime);
		}
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
