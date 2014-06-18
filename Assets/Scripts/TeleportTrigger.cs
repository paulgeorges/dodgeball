using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour {
	public LayerMask mask;
	public Transform teleportPoint;
	public bool active = true;

	private void OnTriggerEnter (Collider other)
	{
		if(active && teleportPoint){
			if(mask.IsLayerInLayerMask(other.gameObject.layer)){
				Transform parent = other.transform.parent;
				while(parent != null){
					if(mask.IsLayerInLayerMask(parent.gameObject.layer)){
						return;
					}

					parent = parent.parent;
				}

				TeleportTrigger otherTeleportTrigger = teleportPoint.GetComponent<TeleportTrigger>();
				if(otherTeleportTrigger){
					otherTeleportTrigger.active = false;
				}

				other.transform.position = teleportPoint.position;
			}
		}
	}

	private void OnTriggerExit(Collider other){
		active = true;
	}
}
