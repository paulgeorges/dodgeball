using UnityEngine;
using System.Collections;

public class HitZone : MonoBehaviour {
	private BoxCollider _boxCollider;
	private bool _active = false;

	public Player owner;

	private void Awake(){
		_boxCollider = GetComponent<BoxCollider>();

		if(!owner){
			gameObject.GetComponentInParent<Player>();
		}
	}
	
	public void SetActive(bool value){
		_active = value;
		_boxCollider.enabled = value;
	}
}
