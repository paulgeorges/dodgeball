using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Send Event")]
[USequencerEvent("PlayMaker/Send Event")]
public class USPlayMakerSendEvent : USEventBase
{
	/// <summary>
	/// The name of the FSM Interface you want to communicate with
	/// </summary>
	public string fsmInterfaceName = "FsmScriptInterface";
	
	/// <summary>
	/// The name of the event you want to fire.
	/// </summary>
	public string eventName = "PlayMaker Event";

    public override void FireEvent()
    {
		PlayMakerFSM[] components = AffectedObject.GetComponents<PlayMakerFSM>();
		foreach(PlayMakerFSM fsm in components)
		{
			if(fsm.FsmName == fsmInterfaceName)
				fsm.Fsm.Event(eventName);
		}
    }

    public override void ProcessEvent(float deltaTime)
    {
		
    }
	
	public override void StopEvent()
	{
		UndoEvent();
	}
	
	public override void UndoEvent()
	{
		
	}
}
