using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Bus Fade")]
[USequencerEvent("MasterAudio/Bus Fade")]
public class USMasterAudioBusFade : USEventBase {
	public bool allBuses;	
	public string busName;
	public float targetVolume;
	public float fadeTime;
	
	public override void FireEvent(){
		if (!allBuses && string.IsNullOrEmpty(busName)) {
			Debug.LogError("You must either check 'All Buses' or enter the Bus Name");
			return;
		}
		
		if (allBuses) {
			var busNames = MasterAudio.RuntimeBusNames;
			for (var i = 0; i < busNames.Count; i++) {
				MasterAudio.FadeBusToVolume(busNames[i], targetVolume, fadeTime);
			}
		} else {
			MasterAudio.FadeBusToVolume(busName, targetVolume, fadeTime);
		}
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
