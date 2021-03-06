using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Match Objects Orientation")]
[USequencerEvent("Transform/Match Objects Orientation")]
public class USMatchObjectEvent : USEventBase
{
    public GameObject objectToMatch = null;
	public AnimationCurve inCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 1.0f));
	
	private Quaternion sourceRotation = Quaternion.identity;
	private Vector3 sourcePosition = Vector3.zero;

    public override void FireEvent()
    {
		if(!objectToMatch)
		{
			Debug.LogWarning("The USMatchObjectEvent event does not provice a object to match", this);
			return;
		}    
		
		sourceRotation = AffectedObject.transform.rotation;
		sourcePosition = AffectedObject.transform.position;
    }

    public override void ProcessEvent(float deltaTime)
    {
		if(!objectToMatch)
		{
			Debug.LogWarning("The USMatchObjectEvent event does not provice a object to look at", this);
			return;
		}
		
		float ratio = 1.0f;
		ratio = Mathf.Clamp(inCurve.Evaluate(deltaTime), 0.0f, 1.0f);
		
		Vector3 destinationPosition = objectToMatch.transform.position;
		Quaternion destinationRotation = objectToMatch.transform.rotation;
		
		AffectedObject.transform.rotation = Quaternion.Slerp(sourceRotation, destinationRotation, ratio);
		AffectedObject.transform.position = Vector3.Slerp(sourcePosition, destinationPosition, ratio);
    }
	
	public override void StopEvent()
	{
		UndoEvent();
	}
	
	public override void UndoEvent()
	{
		if(!AffectedObject)
			return;
		
		AffectedObject.transform.rotation = sourceRotation;
		AffectedObject.transform.position = sourcePosition;
	}
}
