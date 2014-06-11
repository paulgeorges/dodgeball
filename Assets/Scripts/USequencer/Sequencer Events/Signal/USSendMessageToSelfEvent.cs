using UnityEngine;
using System.Collections;

public enum SendMessageParamenter{
	None,
	Bool,
	String,
	Float,
	Int
}

[USequencerFriendlyName("Send Message To Self")]
[USequencerEvent("Signal/Send Message To Self")]
public class USSendMessageToSelfEvent : USEventBase 
{
	public string action = "OnSignal";
	public SendMessageParamenter sendMessageParameter;
	public bool boolValue;
	public string stringValue;
	public float floatValue;
	public int intValue;

	public override void FireEvent()
	{
		if(!Application.isPlaying)
			return;
		
		if (AffectedObject){
			switch(sendMessageParameter){
			case SendMessageParamenter.Bool:
				AffectedObject.SendMessage (action, boolValue);
				break;
			case SendMessageParamenter.String:
				AffectedObject.SendMessage (action, stringValue);
				break;
			case SendMessageParamenter.Float:
				AffectedObject.SendMessage (action, floatValue);
				break;
			case SendMessageParamenter.Int:
				AffectedObject.SendMessage (action, intValue);
				break;
			case SendMessageParamenter.None:
				AffectedObject.SendMessage (action);
				break;
			default:
				AffectedObject.SendMessage (action);
				break;
			}
		}
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		
	}
}
