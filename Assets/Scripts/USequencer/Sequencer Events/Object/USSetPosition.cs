using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Set Position")]
[USequencerEvent("Object/Set Position")]
public class USSetPosition : USEventBase
{
	public bool local;
	public Vector3 position;
	private Vector3 _previousPosition;

	public override void FireEvent()
	{
		SetPosition();
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		SetPosition();
	}

	private void SetPosition(){
		if(AffectedObject){
			if(local){
				_previousPosition = AffectedObject.transform.localPosition;
				AffectedObject.transform.localPosition = position;
			}else{
				_previousPosition = AffectedObject.transform.position;
				AffectedObject.transform.position = position;
			}
		}
	}

	public override void StopEvent()
	{

	}
	
	public override void UndoEvent()
	{
		if(AffectedObject){
			if(local){
				AffectedObject.transform.localPosition = _previousPosition;
			}else{
				AffectedObject.transform.position = _previousPosition;
			}
		}
	}
}
