using UnityEngine;
using System.Collections;

public class ResetTrigger : MonoBehaviour {

	private void OnTriggerEnter (Collider other)
	{
		Player player = other.GetComponent<Player>();
		if(player){
			Messenger<Player>.Invoke(GameMessages.RESET_PLAYER, player);
		}
	}
}
