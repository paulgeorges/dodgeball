using UnityEngine;
using System.Collections;

[USequencerFriendlyName("Sleep Rigid Body")]
[USequencerEvent("Physics/Sleep Rigid Body")]
public class USSleepRigidBody : USEventBase 
{	
	public override void FireEvent()
	{	
		Rigidbody affectedBody = AffectedObject.GetComponent<Rigidbody>();
		if(!affectedBody)
		{
			Debug.Log("Attempting to Nullify a force on an object, but it has no rigid body from USSleepRigidBody::FireEvent");
			return;
		}
		
		affectedBody.Sleep();
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
		if(!AffectedObject)
			return;
		
		Rigidbody affectedBody = AffectedObject.GetComponent<Rigidbody>();
		if(!affectedBody)
			return;
		
		affectedBody.WakeUp();
	}
}
