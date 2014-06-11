using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[USequencerFriendlyName("Send CSharpMessenger Message")]
[USequencerEvent("Hibernum/CSharpMessenger/Send CSharpMessage")]
public class USSendCSharpMessageEvent : USEventBase 
{
	public string message = "OnSignal";
	public string parameter;

	public override void FireEvent()
	{
		if(!Application.isPlaying)
			return;

		if(parameter != null && parameter != ""){
			Messenger<string>.Invoke(message, parameter);
		}else{
			Messenger.Invoke(message);
		}
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
