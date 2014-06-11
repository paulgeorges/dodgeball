using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	public Transform currentTarget;
	public Vector3 offset;

	void Update () {
		if(currentTarget){
			transform.position = currentTarget.position + offset;
		}
	}

	private void OnTargetUpdated(GameObject target)
	{
		currentTarget = target.transform;
	}
	
	private void OnTargetRemoved(){
		currentTarget = null;
	}
}
