using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Set Euler Rotation")]
[USequencerEvent("Object/Set Set Euler Rotation")]
public class USSetEulerRotation : USEventBase
{
	public bool local = true;
	public Vector3 eulerRotation;
	private Vector3 _previousEulerRotation;

	public override void FireEvent()
	{
		SetEulerRotation();
	}
	
	public override void ProcessEvent(float deltaTime)
	{
		SetEulerRotation();
	}

	private void SetEulerRotation(){
		if(AffectedObject){
			if(local){
				_previousEulerRotation = AffectedObject.transform.localEulerAngles;
				AffectedObject.transform.localEulerAngles = eulerRotation;
			}else{
				_previousEulerRotation = AffectedObject.transform.eulerAngles;
				AffectedObject.transform.eulerAngles = eulerRotation;
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
				AffectedObject.transform.localEulerAngles = _previousEulerRotation;
			}else{
				AffectedObject.transform.eulerAngles = _previousEulerRotation;
			}
		}
	}
}
