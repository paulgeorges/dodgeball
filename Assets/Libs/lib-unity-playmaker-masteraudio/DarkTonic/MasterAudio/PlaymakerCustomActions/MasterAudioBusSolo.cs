using UnityEngine;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory(ActionCategory.Audio)]
[Tooltip("Solo all Audio in a Bus in Master Audio")]
public class MasterAudioBusSolo : FsmStateAction {
	[Tooltip("Check this to perform action on all Buses")]
	public FsmBool allBuses;	

    [Tooltip("Name of Master Audio Bus")]
	public FsmString busName;
	
	public override void OnEnter() {
		if (!allBuses.Value && string.IsNullOrEmpty(busName.Value)) {
			Debug.LogError("You must either check 'All Buses' or enter the Bus Name");
			return;
		}

		if (allBuses.Value) {
			var busNames = MasterAudio.RuntimeBusNames;
			for (var i = 0; i < busNames.Count; i++) {
				MasterAudio.SoloBus(busNames[i]);
			}
		} else {
			MasterAudio.SoloBus(busName.Value);
		}
		
		Finish();
	}
	
	public override void Reset() {
		allBuses = new FsmBool(false);
		busName = new FsmString(string.Empty);
	}
}
