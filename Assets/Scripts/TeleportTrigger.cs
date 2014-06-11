using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour {

	public Transform teleportPoint;

	private void OnTriggerEnter (Collider other)
	{
		PlayerController playerController = other.GetComponent<PlayerController>();
		if(playerController){
			playerController.transform.position = teleportPoint.position;
			playerController.transform.rotation = teleportPoint.rotation;

			CharacterMotor characterMotor = other.GetComponent<CharacterMotor>();
			characterMotor.movement.lockXAxis = false;
			characterMotor.movement.lockZAxis = false;
			
			if(teleportPoint.forward.x >= 1){
				characterMotor.movement.lockZAxis = true;
				characterMotor.movement.lockZAxisValue = characterMotor.transform.position.z;
			}
			
			if(teleportPoint.forward.z >= 1){
				characterMotor.movement.lockXAxis = true;
				characterMotor.movement.lockXAxisValue = characterMotor.transform.position.x;
			}
		}
	}
}
