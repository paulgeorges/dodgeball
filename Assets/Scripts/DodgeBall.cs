using UnityEngine;
using System.Collections;

public class DodgeBall : MonoBehaviour {
	private SphereCollider _sphereCollider;
	private Rigidbody _rigidBody;

	public bool isCaptured = false;
	public Player capturedBy;
	public bool isInFlight = false;

	private void Awake(){
		_rigidBody = GetComponent<Rigidbody>();
		_sphereCollider = GetComponent<SphereCollider>();
		Reset();
	}

	public void Reset(){
		_rigidBody.velocity = Vector3.zero;
		_sphereCollider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == TagNames.Player && (capturedBy == null || capturedBy.gameObject != other.gameObject)){
			if(!isInFlight && !isCaptured && _sphereCollider.isTrigger){
				capturedBy = other.gameObject.GetComponent<Player>();
				isCaptured = true;
				isInFlight = false;
				capturedBy.SendMessage(GameMessages.DODGE_BALL_CAPTURED, this);
			}
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == TagNames.Player){
			
		}
	}
}
