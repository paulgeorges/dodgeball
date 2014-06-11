using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Stop All Of Sound")]
[USequencerEvent("MasterAudio/Stop All Of Sound")]
public class USMasterAudioStopAllOfSound : USEventBase {
	public bool allGroups;	
	public string soundGroupName;
	
	public override void FireEvent(){
		if (!allGroups && string.IsNullOrEmpty(soundGroupName)) {
			Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
			return;
		}

		if (allGroups) {
			var groupNames = MasterAudio.RuntimeSoundGroupNames;
			for (var i = 0; i < groupNames.Count; i++) {
				MasterAudio.StopAllOfSound(groupNames[i]);
			}
		} else {
			MasterAudio.StopAllOfSound(soundGroupName);
		}
	}

	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
