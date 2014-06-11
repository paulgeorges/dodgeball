using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Broadcast Message")]
[USequencerEvent("Signal/Broadcast Message")]
public class USBroadcastMessageEvent : USEventBase 
{
	public GameObject receiver = null;
	public string action = "OnSignal";
	
	public override void FireEvent()
	{
		if(!Application.isPlaying)
			return;
		
		if (receiver)
			receiver.BroadcastMessage (action);
		else
			Debug.LogWarning ("No receiver of signal \""+action+"\" on object "+receiver.name+" ("+receiver.GetType().Name+")", receiver);
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
